using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public bool isCursorEmpty;
    public Tilemap tilemap;

    public Market _Market;
    
    private Tile choosenTile;
    
    
    
    private void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);

        if (tilemap.GetTile(cellPos))
        {
            foreach (var VARIABLE in COLLECTION)
            {
                
            }
        }

    }
}
