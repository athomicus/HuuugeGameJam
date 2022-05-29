using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Animator _Animator = null;

    [SerializeField]
    private float _MoveTime = 5.0f;

    [SerializeField]
    private float _Damage = 10.0f;

    [SerializeField]
    private float _AttackDistance = 2.0f, _AttackRadius = 1.0f;

    private Vector3 _startPosition = Vector3.zero;

    private Vector3 _endPosition = Vector3.zero;

    private float _timeElapsed = 0f;

    private eCharacterState _knightState = eCharacterState.Attacking;

    private static readonly Collider[] _colliderBuffer = new Collider[ 10 ];

    [SerializeField]
    private float _MaxHealth = 80.0f;

    private Vector3 EndPoint => transform.position - Vector3.forward * ( _AttackDistance - _AttackRadius );

    public float MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
    public float Health { get; set; }
    public void Damage( float damage )
    {
        Health -= damage;
        Health = Mathf.Min( Health, MaxHealth );

        if( Health > 0 ) return;

        Health = 0.0f;

        StartDying();
    }

    public void StartMoving( Vector3 target_pos )
    {
        _timeElapsed = 0f;
        _startPosition = transform.position;
        _endPosition = target_pos;
        _knightState = eCharacterState.Moving;

        _Animator.Play( "move" );
    }

    public void StartAttacking()
    {
        _timeElapsed = 0f;
        _knightState = eCharacterState.Attacking;

        _Animator.Play( "attack" );
    }

    public void StartDying()
    {
        _knightState = eCharacterState.Dead;

            // Disable the collider.
        GetComponent<CapsuleCollider>().enabled = false;

        _Animator.Play( "death" );
    }

    private void Awake()
    {
        Health = MaxHealth;

        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch( _knightState )
        {
            case eCharacterState.Attacking:

                // ?

                break;

            case eCharacterState.Moving:
                
                transform.position = Interpolate( _startPosition, _endPosition, _timeElapsed / _MoveTime );

                if( _timeElapsed >= _MoveTime ) StartAttacking();
                break;

            case eCharacterState.Dead:

                // ?

                return;
        }

        _timeElapsed += Time.deltaTime;
    }

    private Vector3 Interpolate( Vector3 start, Vector3 end, float t )
    {
        Vector3 vec = new Vector3()
        {
            x = Mathf.SmoothStep( start.x, end.x, t ),
            y = Mathf.SmoothStep( start.y, end.y, t ),
            z = Mathf.SmoothStep( start.z, end.z, t )
        };

        return vec;
    }

    protected virtual void Attack()
    {
        int n = Physics.OverlapCapsuleNonAlloc( transform.position, EndPoint, _AttackRadius,
                    _colliderBuffer );

        for( int i = 0; i < n; ++i )
        {
            var col = _colliderBuffer[ i ];

                // Don't hit other knights
            if( col.GetComponent<Knight>() != null ) continue;

            col.GetComponent<Orc>()?.Damage( _Damage );
        }
    }

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

}
public enum eCharacterState { Attacking, Moving, Dead }
