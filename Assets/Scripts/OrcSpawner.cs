using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcSpawner : MonoBehaviour
{
    [SerializeField]
    private GridHandler _GridHandler = null;

    [SerializeField]
    private GameObject _OrcPrefab = null;

    [SerializeField]
    private float _SpawnFrequency = 2.0f;

    private float _timeElapsed = 0.0f;

    private void Update()
    {
        _timeElapsed += Time.deltaTime;

        if( _timeElapsed < _SpawnFrequency ) return;

        _timeElapsed = 0.0f;

        SpawnOrc();

    }

    private void SpawnOrc()
    {
        var offset = new Vector2Int( 1, 1 + Random.Range( 0, _GridHandler.GridSize.y ) );

        var orc_pos = _GridHandler.GetButtonPosition( _GridHandler.GridSize - offset );

        Instantiate( _OrcPrefab, orc_pos, Quaternion.identity );
    }
}
