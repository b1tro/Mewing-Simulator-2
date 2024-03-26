using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BPData : MonoBehaviour
{
    public Dictionary<Vector3Int, Building> buildingsBP = new Dictionary<Vector3Int, Building>();
    public Dictionary<Vector3Int, Building> buildingsStorage = new Dictionary<Vector3Int, Building>();
    
    public Tilemap tilemap;
    
    public GridHighlight _GridHighlight;
    
    public void DeleteTile(Vector3Int cellPos, Building build)
    {
        buildingsBP.Remove(cellPos, out build);
    }
}
