using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BPData : MonoBehaviour
{
    public Dictionary<List<Vector3Int>, Building> buildingsBP = new Dictionary<List<Vector3Int>, Building>();
    public Dictionary<Vector3Int, Building> buildingsStorage = new Dictionary<Vector3Int, Building>();
    
    public UnityEngine.Tilemaps.Tilemap tilemap;
    
    public GridHighlight _GridHighlight;
    
    public void DeleteTile(List<Vector3Int> cellPos, Building build)
    {
        buildingsBP.Remove(cellPos, out build);
    }
    
    public bool ContainsTile(Vector3Int cellPos)
    {
        if (buildingsBP == null)
        {
            
            return false;
        }

        foreach (var building in buildingsBP)
        {
            foreach (var Pos in building.Key)
            {
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
        if (buildingsBP == null) return null;
        
        foreach (var building in buildingsBP)
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