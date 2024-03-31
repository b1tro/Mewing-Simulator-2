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

    private List<Vector3Int> previousCellPos = new List<Vector3Int>();

    private void Start()
    {
        //Time.timeScale = 0.0001f;

    }

    private void OnEnable()
    {
        _TileMovement.DropBuild += Highlight;
    }
    
    private void OnDisable()
    {
        Debug.Log("VURUBIL");
        _TileMovement.DropBuild -= Highlight;
    }

    private void Highlight(List<Vector3Int> cellPos, bool isCursorEmpty, Vector3Int mousePos)
    {
        if (previousCellPos != null || isCursorEmpty)
        {
            ClearGrid();
        }
        
        //Debug.Log(tilemap.GetTile(cellPos));

        if (!isCursorEmpty && (mousePos.x >= 1 && mousePos.x <= 8 && mousePos.y >= -4 && mousePos.y <= 3))
        {
            foreach (Vector3Int Pos in cellPos)
            {
                // if (tilemap.GetTile(previousCellPos) == highlightedTile)
                //     tilemap.SetTile(previousCellPos, null);
                // return;


                // if (tilemap.GetTile(previousCellPos) == highlightedTile)
                // {
                //     tilemap.SetTile(previousCellPos, null);
                // }

                if (!(Pos.x >= 1 && Pos.x <= 8 && Pos.y >= -4 && Pos.y <= 3))
                {
                    break;
                }

                tilemap.SetTile(Pos, highlightedTile);
                    tilemap.SetTileFlags(Pos, TileFlags.None);
                    tilemap.SetColor(Pos, highlightColor);
                
            }
        }

        if (previousCellPos != null) previousCellPos.Clear();
        previousCellPos = cellPos;
    }

    public void ClearGrid()
    {
        for(int x = 1; x <= 8; x++)
        {
            for (int y = -4; y <= 3; y++)
            {
                if(tilemap.GetTile(new Vector3Int(x, y)) == highlightedTile)
                    tilemap.SetTile(new Vector3Int(x, y), null);
            }
      
        }
    }

    public Tile GetDefaultTile()
    {
        return defaultTile;
    }

    public bool OnBackpack(List<Vector3Int> cellPos)
    {
        bool isAllInBP = true;
        
        foreach (Vector3Int Pos in cellPos)
        {
            if(!(Pos.x >= 1 && Pos.x <= 8 && Pos.y >= -4 && Pos.y <= 3))
            {
                isAllInBP = false;
            }
        }

        return isAllInBP;
    }
}