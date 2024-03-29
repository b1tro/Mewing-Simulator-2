using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMovement : MonoBehaviour
{
    public bool isCursorEmpty = true;
    public bool isRotating;

   // public Market _Market;
    public BPData _BpData;
    public RenderTile _RenderTile;
    public GridHighlight _GridHighlight;

    public UnityEngine.Tilemaps.Tilemap tilemap;
    private Tile choosenTile;

    public GameObject curBuilding = null;

    private Vector3Int cellPos;
    private Vector3 mousePos;

    public Building choosenBuilding;

    public GameObject spritePrefab;
    private Collider2D curCollider;

    public float duration;
    public float rotationSpeed = 500;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPos = tilemap.WorldToCell(mousePos);

        mousePos.z = 0;
        
        //Debug.Log(tilemap.GetTile(cellPos));

        CheckMouseOverCollider();

        TakeFromBP();

        DropBuildingOnOtherBuilding();

        if (_GridHighlight.OnBackpack(cellPos))
        {
            DropBuilding();
            
            MovingInBP();
        }
        else
        {
            TakeFromMarket();

            MovingInStorage();
        }
        
        if (Input.GetMouseButtonDown(1) && !isRotating && !isCursorEmpty)
        {
            StartCoroutine(RotationBuilding());
        }
    }

    public void CheckMouseOverCollider()
    {
        curCollider = Physics2D.OverlapPoint(mousePos);
        //OverlapPoint проверяет если мышка на коллайдере  Physics2D.OverlapBox(mousePos, new Vector2(1.75f, 1.75f), 0);

        if (curCollider == null) return;
        
        Debug.Log("YA RABOTAYU");
        
        if (!isCursorEmpty) return;
        
        Debug.Log("YA RABOTAYU");
        
        curBuilding = curCollider.gameObject;

        choosenBuilding = curBuilding.transform.parent.GetComponent<Build>().cellBuilding;
        
        //choosenBuilding = curBuilding.GetComponent<Build>().spriteBuilding;

        //Важная штука, меняет текущий геймобжэкт на тот, на который указывает мышка
    }

    private void TakeFromMarket()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        //Debug.LogError(_RenderTile.ContainsTile(cellPos));
        
        if (!_RenderTile.ContainsTile(cellPos)) return;
        //!_Market.buildingTile.ContainsKey(cellPos) 

        isCursorEmpty = false;
        choosenBuilding = _RenderTile.GetBuilding(cellPos);
        CreateSprite(new Quaternion(0, 0, 0, 0));
        //Спавнит спрайт тайла с магаза
    }

    private void MovingInStorage()
    {
        if(!curBuilding) return;
        
        if (Input.GetMouseButton(0))
        {
            if (!isCursorEmpty || (curCollider != null))
            {
                isCursorEmpty = false;
                
                curBuilding.transform.parent.GetComponent<Rigidbody2D>().MovePosition(mousePos);
            }
        }
        //Несет спрайт за курсором

        if (Input.GetMouseButtonUp(0) && !isCursorEmpty)
        {
            isCursorEmpty = true;
            
            curBuilding.transform.parent.GetComponent<Rigidbody2D>().velocity =
                (mousePos - curBuilding.transform.position).normalized * 5;
        }
        //Если выпал из курсора спрайт, то подкидывает его в сторону, куда двигался
    }
    
    private void MovingInBP()
    {
        if (!Input.GetMouseButton(0) || !curBuilding) return;
        
        isCursorEmpty = false;
        
        mousePos = tilemap.CellToWorld(cellPos);
        curBuilding.transform.parent.position = mousePos + new Vector3(0.75f, 0.75f);
        //Двигает в рюкзачке
    }

     
    private IEnumerator RotationBuilding()
    {
        if (isRotating)
        {
            yield break;
        }

        isRotating = true;

        float targetAngle = curBuilding.transform.parent.eulerAngles.z + 90f;
        float startTime = Time.time; // Time when rotation starts

        while (Time.time - startTime < duration)
        {
            float step = rotationSpeed * Time.deltaTime;
            curBuilding.transform.parent.Rotate(0, 0, step);
            yield return null;
        }
        
        curBuilding.transform.parent.eulerAngles = new Vector3(
            curBuilding.transform.parent.eulerAngles.x,
            curBuilding.transform.parent.eulerAngles.y,
            targetAngle
        );

        isRotating = false;
    }
    
    private void DropBuilding()
    {
        if (!Input.GetMouseButtonUp(0) || !curBuilding  || isCursorEmpty || _BpData.ContainsTile(cellPos)) return;
        
        isCursorEmpty = true;
            
        if(isRotating) return; 
        
        Destroy(curBuilding);
        
        
        
        _BpData.buildingsBP.Add(, choosenBuilding);
        tilemap.SetTile(cellPos, choosenBuilding.getTile());
        tilemap.SetTransformMatrix(cellPos, Matrix4x4.TRS(Vector3.zero, curBuilding.transform.rotation, Vector3.one));
        //Ставит в рюкзачек
    }
    
    private void DropBuildingOnOtherBuilding()
    {
        if (!Input.GetMouseButtonUp(0) || !curBuilding  || isCursorEmpty || !_BpData.ContainsTile(cellPos)) return;
        
        isCursorEmpty = true;
            
        if(isRotating) return; 
        
        Quaternion prevRotation = tilemap.GetTransformMatrix(cellPos).rotation;
        
        tilemap.SetTile(cellPos, choosenBuilding.getTile());
        tilemap.SetTransformMatrix(cellPos, Matrix4x4.TRS(Vector3.zero, curBuilding.transform.rotation, Vector3.one));

        Building prevBuild = _BpData.buildingsBP[choosenBuilding.getCellsPos()];


        _BpData.DeleteTile(choosenBuilding.getCellsPos(), prevBuild);
        _BpData.buildingsBP.Add(choosenBuilding.getCellsPos(), choosenBuilding);

        Destroy(curBuilding);
        
        choosenBuilding = prevBuild;
        
        CreateSprite(prevRotation);
        //Ставит в рюкзачек
    }

    private void TakeFromBP()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (!_BpData.ContainsTile(cellPos)) return;
        
        isCursorEmpty = false;

        choosenBuilding = _BpData.GetBuilding(cellPos);
        
        _BpData.DeleteTile(choosenBuilding.getCellsPos(), choosenBuilding);

        tilemap.SetTile(cellPos, _GridHighlight.GetDefaultTile());

        CreateSprite(tilemap.GetTransformMatrix(cellPos).rotation);
    }

    private void CreateSprite(Quaternion rotation)
    {
        GameObject curBuilding =Instantiate(spritePrefab, mousePos, Quaternion.identity);
        curBuilding.tag = "Tower";

            curBuilding.GetComponent<Build>().cellBuilding = choosenBuilding;
        
        foreach (var curPos in choosenBuilding.getCellsPos())
        {
            GameObject tileClone = Instantiate(new GameObject(), new Vector3((curPos.x * 1.75f), (curPos.y * 1.75f), 0), Quaternion.identity);
            tileClone.AddComponent<SpriteRenderer>().GetComponent<SpriteRenderer>().sprite = choosenBuilding.getSprite();
            tileClone.AddComponent<BoxCollider2D>();
            
            tileClone.tag = "Tower";
            
            tileClone.transform.parent = curBuilding.transform;
        }
        
    }
}