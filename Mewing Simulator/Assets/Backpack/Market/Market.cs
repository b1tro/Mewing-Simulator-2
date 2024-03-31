using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class Market : MonoBehaviour
{
    public Dictionary<List<Vector3Int>, Building> buildingTile = new Dictionary<List<Vector3Int>, Building>();
    public Tilemap marketTilemap;
    void Start()
    {
        MakeBuild();
        MakeBuild();
        
    }

    private void MakeBuild(){
        Building chosenBuilding = CreateInstanceOfRandomClass();

        for (int x = 0; x  < Random.Range(1, 3) ; x++)
        {
            for (int y = 0; y < Random.Range(1, 3); y++)
            {
                if(y == Random.Range(1, 3) && x == Random.Range(1, 3)) continue;
                
                Vector3Int currentTile = new Vector3Int(-x, y, 0);

                marketTilemap.SetTile(currentTile, chosenBuilding.getTile());
                chosenBuilding.addCellPos(currentTile);
                
            }
        }
        
        buildingTile.Add(chosenBuilding.getCellsPos(), chosenBuilding);
    }

    private Building CreateInstanceOfRandomClass()
    {
        int randomNumber = UnityEngine.Random.Range(0, 2);
        switch (randomNumber)
        {
            case 0:
                return new House();
            case 1:
                return new Bank();
            default: 
                return null;
        }
        
    }

    public bool ContainsCellPos(Vector3Int cellPos)
    {
        if (buildingTile == null) return false;
        
        foreach (var building in buildingTile)
        {
            foreach (var Pos in building.Key)
            {
                if (Pos == cellPos)
                    return true;
            }
        }
        return false;
    }
    
    public Building GetBuilding(Vector3Int cellPos)
    {
        if (buildingTile == null) return null;
        
        foreach (var building in buildingTile)
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
    
    public void RenderTiles()
    {
        // foreach (var building in buildingTile)
        // {
        //     foreach (var Pos in building.Key)
        //     {
        //         //Debug.Log("YA POSTAVIL:" + Pos + " " +   building.Value.getTile());
        //         
        //         marketTilemap.SetTile(Pos, building.Value.getTile());
        //     }
        // }
    }

    public void DeleteTiles(Building building)
    {
        foreach (var build in buildingTile)
        {
            if (build.Value == building)
            {
                foreach (var Pos in build.Key)
                {
                    marketTilemap.SetTile(Pos, null);
                }

            }
        }
        
        buildingTile.Remove(building.getCellsPos());
    }
    
    public List<Vector3Int> GetCellsPos(Building building)
    {
        if (buildingTile == null) return null;
        
        foreach (var build in buildingTile)
        {
            if (build.Value == building)
            {
                return build.Key;
            }
        }
        return null;
    }
}
