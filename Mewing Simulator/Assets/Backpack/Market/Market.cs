using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Market : MonoBehaviour
{
    public Dictionary<Vector3Int, Building> buildingTile = new Dictionary<Vector3Int, Building>();
    public UnityEngine.Tilemaps.Tilemap marketTilemap;
    void Start()
    {
        for (int x = 2; x < 7; x += 2)
        {
            for (int y = 0; y < 5; y += 2)
            {
                Building chosenBuilding = CreateInstanceOfRandomClass();
                Debug.Log(chosenBuilding);
                Vector3Int currentTile = new Vector3Int(-x, y, 0);
                Debug.Log(currentTile);
                marketTilemap.SetTile(currentTile, chosenBuilding.getTile());
                Debug.Log(chosenBuilding.getTile());
                buildingTile.Add(currentTile, chosenBuilding);
            }
        }
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
}