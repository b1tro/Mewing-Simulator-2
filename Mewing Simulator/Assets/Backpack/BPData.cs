using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BPData : MonoBehaviour
{
    public Dictionary<List<Vector3Int>, Building> buildingsBP = new Dictionary<List<Vector3Int>, Building>();
    public Dictionary<Vector3Int, Building> buildingsStorage = new Dictionary<Vector3Int, Building>();

    public Tilemap tilemap;

    public GridHighlight _GridHighlight;
    
    public bool ContainsCellPos(Vector3Int cellPos)
    {
        if (buildingsBP == null) return false;
        
        foreach (var building in buildingsBP)
        {
            foreach (var Pos in building.Key)
            {
                if (Pos == cellPos)
                    return true;
            }
        }
        return false;
    }

    public void RenderTiles()
    {
        if (buildingsBP == null) return;
        
        foreach (var building in buildingsBP)
        {
            foreach (var Pos in building.Key)
            {
                Debug.Log("YA POSTAVIL:" + Pos + " " +   building.Value.getTile());
                
                tilemap.SetTile(Pos, building.Value.getTile());
                
                //Debug.Log(building.Value.getRotation() + "POVOROT ++++++++++++");
                tilemap.SetTransformMatrix(Pos, Matrix4x4.TRS(Vector3.zero, building.Value.getRotation(), Vector3.one));
            }
        }
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
    
    public List<Vector3Int> GetCellsPos(Building building)
    {
        if (buildingsBP == null) return null;
        
        foreach (var build in buildingsBP)
        {
            if (build.Value == building)
            {
                return build.Key;
            }
        }
        return null;
    }
    
    public void DeleteTiles(Building building)
    {
        if (buildingsBP == null) return;
        
        foreach (var build in buildingsBP)
        {
            if (build.Value == building)
            {
                foreach (var Pos in build.Key)
                {
                    tilemap.SetTile(Pos, _GridHighlight.GetDefaultTile());
                }
                buildingsBP.Remove(build.Key, out building);
                
                return;
            }
        }
    }
    
    
    
}
