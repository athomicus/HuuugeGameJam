using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridButton : Button
{
    public Vector3 WorldPosition => transform.position;

    public Vector2Int GridPosition { get; set; } = Vector2Int.zero;

    public OccupyingObject ObjectAtButton { get; set; } = null;

    public GridButton[,] Grid { get; set; } = null;

    private static OccupyingObject _selectedObject = null;

    public void ClickButton()
    {
            // If the knight at this place is dead, remove them from the button.
        if( ObjectAtButton != null && ObjectAtButton.knight.Health == 0 )
        {
            ObjectAtButton = null;
        }

        if( _selectedObject != null && _selectedObject.knight.Health == 0 )
        {
            UnhighlightAvailableButtons();

            _selectedObject = null;
        }

        // We click an empty field.
        if( _selectedObject == null && ObjectAtButton == null ) return;

        // We tried to move the knight at the same position it is in,
        // so deselect the knight and unhighlight the available buttons.
        if( _selectedObject == ObjectAtButton )
        {
            UnhighlightAvailableButtons();

            _selectedObject = null;

            return;
        }

        // We move a selected knight
        if( _selectedObject != null && ObjectAtButton == null )
        {
            UnhighlightAvailableButtons();

                // This checks if the clicked position is within the knight's movement range.
            if( ( _selectedObject.gridPosition - GridPosition ).sqrMagnitude > 2 )
            {
                _selectedObject = null;

                return;
            }
            ObjectAtButton = _selectedObject;
            var pos = ObjectAtButton.gridPosition;
            Grid[ pos.x, pos.y ].ObjectAtButton = null;
            ObjectAtButton.gridPosition = GridPosition;
            _selectedObject = null;

            // TODO: start moving the knight, playing the animation...

            ObjectAtButton.knight.StartMoving( WorldPosition );

            return;
        }

            // We select a knight
        if( _selectedObject == null )
        {
            _selectedObject = ObjectAtButton;

            HighlightAvailableButtons();

            return;
        }

        UnhighlightAvailableButtons();
        _selectedObject = null;
    }

    private void SetButtonColour( Vector2Int pos, Color colour )
    {
        Grid[ pos.x, pos.y ].image.color = colour;
    }

    private void HighlightAvailableButtons()
    {
        for( int i = GridPosition.x - 1; i <= GridPosition.x + 1; ++i )
        {
            for( int j = GridPosition.y - 1; j <= GridPosition.y + 1; ++j )
            {
                    // If the indeces are out of bounds, skip them.
                if( i < 0 || j < 0 ) continue;
                if( i >= Grid.GetLength( 0 ) || j >= Grid.GetLength( 1 ) ) continue;

                var pos = new Vector2Int( i, j );

                if( Grid[ i, j ].ObjectAtButton != null && pos != GridPosition
                         && Grid[ i, j ].ObjectAtButton.knight.Health != 0 ) continue;

                Color colour = new Color( .03f, 1, .27f, .43f );

                if( pos == GridPosition )
                {
                    colour = new Color( .03f, .88f, 1, .43f );
                }

                SetButtonColour( pos, colour );
            }
        }
    }
    private void UnhighlightAvailableButtons()
    {
        var grid_pos = _selectedObject.gridPosition;
        for( int i = grid_pos.x - 1; i <= grid_pos.x + 1; ++i )
        {
            for( int j = grid_pos.y - 1; j <= grid_pos.y + 1; ++j )
            {
                    // If the indeces are out of bounds, skip them.
                if( i < 0 || j < 0 ) continue;
                if( i >= Grid.GetLength( 0 ) || j >= Grid.GetLength( 1 ) ) continue;

                SetButtonColour( new Vector2Int( i, j ), new Color( 0, 0, 0, .43f ) );
            }
        }
    }

    public class OccupyingObject
    {
        public Vector2Int gridPosition = Vector2Int.zero;
        public Knight knight = null;
    }
}
