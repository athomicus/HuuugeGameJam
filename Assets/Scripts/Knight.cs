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
    private float _AttackFrequency = 1.0f;

    private Vector3 _startPosition = Vector3.zero;

    private Vector3 _endPosition = Vector3.zero;

    private float _timeElapsed = 0f;

    private eCharacterState _knightState = eCharacterState.Attacking;

    [SerializeField]
    private float _MaxHealth = 80.0f;

    public float MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
    public float Health { get; set; }
    public void Damage( float damage )
    {
        Health -= damage;
    }

    public void StartMoving( Vector3 target_pos )
    {
        _timeElapsed = 0f;
        _startPosition = transform.position;
        _endPosition = target_pos;
        _knightState = eCharacterState.Moving;

        // TODO: Play the moving animation.
    }

    public void StartAttacking()
    {
        _timeElapsed = 0f;
        _knightState = eCharacterState.Attacking;

        // TODO: Play the attacking animation.
    }

    public void StartDying()
    {

    }

    private void Start()
    {
        Health = MaxHealth;

        _Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch( _knightState )
        {
            case eCharacterState.Attacking:

                if( _timeElapsed >= _AttackFrequency )
                {
                    Attack();
                    _timeElapsed = 0f;
                }
                break;

            case eCharacterState.Moving:
                
                transform.position = Interpolate( _startPosition, _endPosition, _timeElapsed / _MoveTime );

                if( _timeElapsed >= _MoveTime ) StartAttacking();
                break;

            case eCharacterState.Dead:
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

    private void Attack()
    {

    }

}
public enum eCharacterState { Attacking, Moving, Dead }
