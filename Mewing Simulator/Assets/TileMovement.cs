using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMovement : MonoBehaviour
{
    public bool isCursorEmpty;
    
    public Tilemap tilemap;
    public Market _Market;
    private Tile choosenTile;

    public GameObject curBuilding = null;

    private Vector3Int cellPos;
    private Vector3 mousePos;

    public GridHighlight _GridHighlight;
    public Building choosenBuilding;

    public bool isDropping = false;



    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPos = tilemap.WorldToCell(mousePos);
        
        mousePos.z = 0;
        
        DragBuilding();
        MoveInBackPackBuilding();
        
        if (Input.GetMouseButtonUp(0) && curBuilding && _GridHighlight.OnBackpack(cellPos))
        {
            DropBuilding();
        }

        if (Input.GetMouseButtonUp(0) && !_GridHighlight.OnBackpack(cellPos))
        {
            Destroy(curBuilding);
        }
    }

    private void DragBuilding()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_Market.buildingTile.ContainsKey(cellPos) && !curBuilding)
            {
                choosenBuilding = _Market.buildingTile[cellPos];
                Debug.Log(choosenBuilding.getSprite());
                
                curBuilding = new GameObject();

                curBuilding.transform.position = mousePos;
                curBuilding.AddComponent<SpriteRenderer>().GetComponent<SpriteRenderer>().sprite = choosenBuilding.getSprite();
                curBuilding.AddComponent<Rigidbody2D>().GetComponent<Rigidbody2D>().gravityScale = 0.1f;

                //Instantiate(obj, mousePos, new Quaternion());

            }
        }

        if (Input.GetMouseButton(0) && curBuilding && !_GridHighlight.OnBackpack(cellPos))
        {
            Debug.Log("SS");
            curBuilding.transform.position = mousePos;
        }
    }

    private void MoveInBackPackBuilding()
    {
        if (Input.GetMouseButton(0) && _GridHighlight.OnBackpack(cellPos) && curBuilding)
        {
            mousePos = tilemap.CellToWorld(cellPos);
            curBuilding.transform.position = mousePos + new Vector3(0.875f, 0.875f);
        }
    }

    private void DropBuilding()
    {
        Destroy(curBuilding);
        tilemap.SetTile(cellPos,choosenBuilding.getTile());
    }
}
