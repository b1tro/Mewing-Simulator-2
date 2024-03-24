using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBehaviour : MonoBehaviour
{
    [SerializeField] 
    private GameManager _gameManager;
    
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

        if (!_gameManager.isCursorEmpty)
        {
           // BuildTower(cellPos);
        }
    }

    public void BuildTower(Vector3Int cellPos)
    {
        _gameManager.isCursorEmpty = true;
        tilemap.SetTile(cellPos, buildingTile);
    }
}
