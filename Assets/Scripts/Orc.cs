using System;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour, IDamageable
{
    public float damage = 10.0f;

    public static event Action OnDeath;

    [SerializeField]
    private float _MovingSpeed = 2.0f;

    [SerializeField]
    private float _AttackDistance = 2.0f;

    [SerializeField]
    private float _AttackRadius = 1.0f;

    [SerializeField]
    private Animator _Animator = null;

        // The collider buffer, its length is the maximal collision count.
    private static readonly Collider[] _colliderBuffer = new Collider[ 10 ];

    private eCharacterState _characterState = eCharacterState.Moving;

    private Vector3 EndPoint => transform.position + Vector3.forward * ( _AttackDistance - _AttackRadius );

    private void OnDrawGizmos()
    {
        var colour = Color.magenta;
        colour.a = .5f;

        Gizmos.color = colour;

        Gizmos.DrawSphere( transform.position, _AttackRadius );        
        Gizmos.DrawSphere( EndPoint, _AttackRadius );

        Gizmos.color = Color.red;
        var offset = Vector3.up * _AttackRadius;
        Gizmos.DrawLine( transform.position + offset, EndPoint + offset );
        offset = Vector3.left * _AttackRadius;
        Gizmos.DrawLine( transform.position + offset, EndPoint + offset );
        offset = Vector3.right * _AttackRadius;
        Gizmos.DrawLine( transform.position + offset, EndPoint + offset );
        offset = Vector3.down * _AttackRadius;
        Gizmos.DrawLine( transform.position + offset, EndPoint + offset );
    }

    [SerializeField]
    private float _MaxHealth = 100.0f;

    public float MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
    public float Health { get; set; }
    public void Damage( float damage )
    {
        Health -= damage;

        Health = Mathf.Min( Health, MaxHealth );

        if( Health > 0 ) return;

        Health = 0;

        Die();
    }

    private void Awake()
    {
        Health = MaxHealth;

        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch( _characterState )
        {
            case eCharacterState.Moving:

                MoveAndLookForTarget();

                break;

            case eCharacterState.Attacking:

                // ?

                break;

            case eCharacterState.Dead:

                // ?

                break;
        }
    }

    private void MoveAndLookForTarget()
    {
        transform.Translate( _MovingSpeed * Time.deltaTime * Vector3.forward );

        int n = Physics.OverlapCapsuleNonAlloc( transform.position, EndPoint, _AttackRadius, _colliderBuffer );

        for( int i = 0; i < n; ++i )
        {
            var col = _colliderBuffer[ i ];

                // Don't collide with self.
            if( col.gameObject == this.gameObject ) continue;

            if( col.GetComponent<IDamageable>() == null ) continue;

            // Don't attack other orcs.
            if( col.GetComponent<Orc>() != null ) continue;

            StartAttacking();

            return;
        }
    }

    private void StartAttacking()
    {
        _characterState = eCharacterState.Attacking;
        // Play the animation.

        _Animator.Play( "fight" );
    }

    private void StartMoving()
    {
        _characterState = eCharacterState.Moving;

        _Animator.Play( "WalkingOrc" );
    }

    private void Die()
    {
        OnDeath?.Invoke();

        _characterState = eCharacterState.Dead;

        GetComponent<CapsuleCollider>().enabled = false;

        _Animator.Play( "death" );
    }

    private void Disappear()
    {
        _characterState = eCharacterState.Dead;

        _Animator.Play( "disappear" );
    }

    private void Destroy() => Destroy( this.gameObject );

    private void Attack()
    {
        int n = Physics.OverlapCapsuleNonAlloc( transform.position, EndPoint, _AttackRadius, _colliderBuffer );

        int hit_knights = 0;
        int hit_objects = 0;

        for( int i = 0; i < n; ++i )
        {
            var col = _colliderBuffer[ i ];

                // Don't collide with self.
            if( col.gameObject == this.gameObject ) continue;

                // Don't attack other orcs.
            if( col.GetComponent<Orc>() != null ) continue;

            col.GetComponent<IDamageable>()?.Damage( damage );

            ++hit_objects;

            hit_knights += col.GetComponent<Knight>() != null ? 1 : 0;
        }

            // Nothing is standing in front of the orc, keep moving.
        if( hit_knights == 0 && hit_objects == 0 )
        {
            StartMoving();
            return;
        }

            // Hit the gate.
        if( hit_knights != hit_objects )
        {
            Disappear();
            return;
        }
    }
}
