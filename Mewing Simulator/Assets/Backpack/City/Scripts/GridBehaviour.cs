using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class GridBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("_gameManager")] [SerializeField] 
    private TileMovement tileMovement;
    
    [SerializeField]
    private Tilemap tilemap;

    public Tile buildingTile;

    public GridHighlight _GridHighlight;
    

    void Update()
    {
    }

    private void OnMouseDown()
    {
        Debug.Log("HUI");
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);

        if (!tileMovement.isCursorEmpty)
        {
           //BuildTower(cellPos);
        }
    }

    public void BuildTower(Vector3Int cellPos)
    {
        tileMovement.isCursorEmpty = true;
        tilemap.SetTile(cellPos, buildingTile);
    }
}
