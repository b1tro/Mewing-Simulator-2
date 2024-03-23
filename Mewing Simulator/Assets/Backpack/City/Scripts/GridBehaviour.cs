using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBehaviour : MonoBehaviour
{
    public bool isCursorEmpty;
    
    [SerializeField]
    private Tilemap tilemap;

    public Tile buildingTile;

    public GridHighlight _GridHighlight;
    

    void FixedUpdate()
    {
        _GridHighlight.Highlight();
    }

    private void OnMouseDown()
    {
        Debug.Log("HUI");
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);

        if (!isCursorEmpty)
        {
            BuildTower(cellPos);
        }
    }

    public void BuildTower(Vector3Int cellPos)
    {
        tilemap.SetTile(cellPos, buildingTile);
    }
}
