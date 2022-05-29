using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Knight
{
    [SerializeField]
    private GameObject _ArrowPrefab = null;

    [SerializeField]
    private Transform _spawnArrowPosition;

    protected override void Attack()
    {
        // Spawn an arrow.
        Instantiate( _ArrowPrefab, _spawnArrowPosition.position, _spawnArrowPosition.rotation );
    }
}
