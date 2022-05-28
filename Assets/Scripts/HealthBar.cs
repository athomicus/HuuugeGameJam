using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform _HealthPivot = null;

    private IDamageable _idamageable = null;

    private void Awake()
    {
        _idamageable = GetComponentInParent<IDamageable>();

        if( _HealthPivot != null ) return;

        _HealthPivot = transform.GetChild( 0 );
    }

    private void Update()
    {
        _HealthPivot.localScale = new Vector3( _idamageable.Health / _idamageable.MaxHealth, 1, 1 );

        // Always look straight at the camera.
        transform.forward = Camera.main.transform.forward;
    }
}
