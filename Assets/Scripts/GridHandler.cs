using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof( GridLayoutGroup ) )]
public class GridHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _KnightPrefab = null, _ArcherPrefab = null;

    [SerializeField]
    private GameObject _ButtonPrefab = null;

    [SerializeField]
    private Vector2Int _GridSize = Vector2Int.zero;

    private GridLayoutGroup _gridLayoutGroup = null;

    private GridButton[,] _buttonGrid = null;

    public Vector2Int GridSize => _GridSize;

    private void Awake()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();

        _gridLayoutGroup.constraintCount = _GridSize.x;

        _buttonGrid = new GridButton[ _GridSize.x, _GridSize.y ];

        SpawnButtons();

        StartCoroutine( SpawnKnights() );
    }

    private void SpawnButtons()
    {
        for( int i = 0; i < _GridSize.x; ++i )
        {
            for( int j = 0; j < _GridSize.y; ++j )
            {
                var button = Instantiate( _ButtonPrefab ).GetComponent<GridButton>();

                button.transform.SetParent( this.transform, worldPositionStays: false );

                _buttonGrid[ i, j ] = button;

                button.Grid = _buttonGrid;

                button.GridPosition = new Vector2Int( i, j );

                button.onClick.AddListener( button.ClickButton );

                button.GetComponentInChildren<Text>().text = new Vector2Int( i, j ).ToString();
            }
        }
    }

    private IEnumerator SpawnKnights()
    {
            // We wait at the end of the frame because thats when the gridlayoutgroup 
            // updates the child elements positions in the grid.
        yield return new WaitForEndOfFrame();

            // The sworded knight is spawned in the middle of the grid.
        var knight_pos = new Vector2Int( 0, _GridSize.y / 2 );

        CreateKnight( _KnightPrefab, knight_pos );
        CreateKnight( _ArcherPrefab, knight_pos + Vector2Int.up );
        CreateKnight( _ArcherPrefab, knight_pos + Vector2Int.down );

    }

    private void CreateKnight( GameObject prefab, Vector2Int grid_pos )
    {
        var obj = Instantiate( prefab, GetButtonPosition( grid_pos ), Quaternion.identity );

        _buttonGrid[ grid_pos.x, grid_pos.y ].ObjectAtButton = new GridButton.OccupyingObject
        {
            gridPosition = grid_pos,
            knight = obj.GetComponent<Knight>()
        };
    }

    public Vector3 GetButtonPosition( Vector2Int pos ) => _buttonGrid[ pos.x, pos.y ].WorldPosition;
}
