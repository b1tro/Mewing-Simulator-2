using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderTile : MonoBehaviour
{
    [SerializeField] public GameObject tilePrefab;
    [SerializeField] public Tilemap tilemap;

    public Dictionary<List<Vector3Int>, Building> storage = new Dictionary<List<Vector3Int>, Building>();

    public void RenderStart()
    {
        foreach (var building in storage)
        {
            if (storage.ContainsKey(building.Key))
            {
                Render(building.Value);
            }
        }
    }

    private void Render(Building building)
    {
        foreach (var cellPos in building.getCellsPos())
        {
            tilemap.SetTile(cellPos,  building.getTile());
        }
    }

    public bool ContainsTile(Vector3Int cellPos)
    {
        if (storage == null)
        {
            
            return false;
        }
        
        
        
        foreach (var building in storage)
        {
            foreach (var b in building.Key)
            {
                Debug.Log(b);
            }
           
            
            Debug.Log(cellPos);
            
            foreach (var Pos in building.Key)
            {
                Debug.Log(Pos + " == " + cellPos);
                if (Pos == cellPos)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Building GetBuilding(Vector3Int cellPos)
    {
        if (storage == null) return null;
        
        foreach (var building in storage)
        {
            foreach (var Pos in building.Key)
            {
                if (Pos == cellPos)
                {
                    return building.Value;
                }
            }
        }

        return null;
    }
}
