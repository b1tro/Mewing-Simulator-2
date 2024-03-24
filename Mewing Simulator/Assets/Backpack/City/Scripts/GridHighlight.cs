using System; using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridHighlight : MonoBehaviour {
    
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField] 
    private Tile defaultTile; 
    [SerializeField]
    private Tile highlightedTile;
    
    public Color highlightColor;

    private Vector3Int previousCellPos;

    private void Start()
    {
        previousCellPos = new Vector3Int(2, 2, -1);
    }

    public void Highlight()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);

       //Debug.Log(cellPos);
        
        if (tilemap.GetTile(cellPos) != defaultTile && tilemap.GetTile(cellPos) != highlightedTile)
        {
            tilemap.SetTile(previousCellPos, defaultTile);
            return;
        }

        if (tilemap.GetTile(previousCellPos) == highlightedTile)
        {
            tilemap.SetTile(previousCellPos, defaultTile);
        }

        tilemap.SetTile(cellPos, highlightedTile);
        tilemap.SetTileFlags(cellPos, TileFlags.None);
        tilemap.SetColor(cellPos, highlightColor);
    
        previousCellPos = cellPos;
    }
}