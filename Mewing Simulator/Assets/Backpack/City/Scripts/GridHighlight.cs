using System; using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridHighlight : MonoBehaviour
{
    public TileMovement _TileMovement;
    
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
        //Time.timeScale = 0.0001f;
        
        previousCellPos = new Vector3Int(2, 2, -1);
    }

    public void Highlight()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);

       Debug.Log(tilemap.GetTile(cellPos));
        
        if (tilemap.GetTile(cellPos) != defaultTile && tilemap.GetTile(cellPos) != highlightedTile)
        {
            if (tilemap.GetTile(previousCellPos) == highlightedTile)
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

    public Tile GetDefaultTile()
    {
        return defaultTile;
    }

    public bool OnBackpack(Vector3Int cellPos) => tilemap.GetTile(cellPos) == defaultTile || tilemap.GetTile(cellPos) == highlightedTile;
}