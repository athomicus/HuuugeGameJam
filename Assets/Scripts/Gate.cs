using System;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _MaxHealth = .8f;

    [SerializeField]
    private float _ElevationOffset = .4f;

    [SerializeField]
    private float _ElevationTime = 1.0f;

    [SerializeField]
    private AudioClip _AudioClip = null;

    public static event Action OnLose;

    public float MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
    public float Health { get; set; }

    private float _startElevation = 0.0f;
    private float _endElevation = 0.0f;
    private float _timeElapsed = 0.0f;

    public static int score = 0;

    private void IncreaseScore() => score += 2;

    private void Awake()
    {
        Orc.OnDeath += RaiseTheBarrier;
        Orc.OnDeath += IncreaseScore;

            // The gate has 2 hit points initially.
        _MaxHealth = 2 * _ElevationOffset;

        _startElevation = _endElevation = transform.position.y;
    }

    private void OnDestroy()
    {
        Orc.OnDeath -= RaiseTheBarrier;
        Orc.OnDeath -= IncreaseScore;
    }

    private void Update()
    {
        float new_y = Mathf.SmoothStep( _startElevation, _endElevation, _timeElapsed / _ElevationTime );

        _timeElapsed += Time.deltaTime;

        var pos = transform.position;
        pos.y = new_y;
        transform.position = pos;
    }

    public void Damage( float damage )
    {
        _ElevationOffset *= -1;

        RaiseTheBarrier();

        _ElevationOffset *= -1;

        if( Health > 0 ) return;

        Health = 0.0f;

        print( "END OF THE GAME" );

        // End the game

        OnLose?.Invoke();
        
        var hi_score = FindObjectOfType<SaveLoadHighScore>();
        hi_score.SetPlayerHighscores( score );

        print( score );
    }

    public void RaiseTheBarrier()
    {
        var clipPosition = Camera.main.transform.position;
        if( _AudioClip != null )
        {
            AudioSource.PlayClipAtPoint( _AudioClip, clipPosition );
        }

        _timeElapsed = 0.0f;
        _startElevation = transform.position.y;
        _endElevation = _startElevation + _ElevationOffset;
        _MaxHealth += 1.01f * _ElevationOffset;
        Health = MaxHealth;
    }
}
