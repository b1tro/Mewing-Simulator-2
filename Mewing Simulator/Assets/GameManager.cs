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

    private GameObject curBuilding = null;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);
        
        mousePos.z = 0;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (_Market.buildingTile.ContainsKey(cellPos))
            {
                Building choosenBuilding = _Market.buildingTile[cellPos];
                Debug.Log(choosenBuilding.getSprite());
                
                curBuilding = new GameObject();

                curBuilding.transform.position = mousePos;
                curBuilding.AddComponent<SpriteRenderer>().GetComponent<SpriteRenderer>().sprite = choosenBuilding.getSprite();
                curBuilding.AddComponent<Rigidbody2D>().GetComponent<Rigidbody2D>().gravityScale = 0.1f;

                //Instantiate(obj, mousePos, new Quaternion());

            }
        }

        if (Input.GetMouseButton(0) && curBuilding)
        {
            Debug.Log("SS");
            curBuilding.transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0) && curBuilding)
        {
            Destroy(curBuilding);
        }
    }
    
    
}
