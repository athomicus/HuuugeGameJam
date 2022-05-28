using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField]
    private float _MoveTime = 5.0f;

    [SerializeField]
    private float _AttackFrequency = 1.0f;

    private Vector3 _startPosition = Vector3.zero;

    private Vector3 _endPosition = Vector3.zero;

    private float _timeElapsed = 0f;

    private eKnightState _knightState = eKnightState.Standing;

    public void StartMoving( Vector3 target_pos )
    {
        _timeElapsed = 0f;
        _startPosition = transform.position;
        _endPosition = target_pos;
        _knightState = eKnightState.Dashing;

        // TODO: Play the moving animation.
    }
    public void StartStanding()
    {
        _timeElapsed = 0f;
        _knightState = eKnightState.Standing;

        // TODO: Play the standing animation.
    }

    private void Update()
    {
        switch( _knightState )
        {
            case eKnightState.Standing:

                if( _timeElapsed >= _AttackFrequency )
                {
                    Attack();
                    _timeElapsed = 0f;
                }

                break;
            case eKnightState.Dashing:

                
                transform.position = Interpolate( _startPosition, _endPosition, _timeElapsed / _MoveTime );

                if( _timeElapsed >= _MoveTime ) StartStanding();
                break;
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

    private enum eKnightState { Standing, Dashing }
}
