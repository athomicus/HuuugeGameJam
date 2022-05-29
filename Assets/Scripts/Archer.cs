using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Knight
{
    [SerializeField]
    private GameObject _ArrowPrefab = null;

    protected override void Attack()
    {
        // Spawn an arrow.
    }
}
