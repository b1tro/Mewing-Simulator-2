using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMovement : MonoBehaviour
{
    public bool isCursorEmpty = true;
    public bool isRotating;

    public Market _Market;
    public BPData _BpData;
    public GridHighlight _GridHighlight;

    public Tilemap tilemap;
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
        Debug.Log(cellPos);
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPos = tilemap.WorldToCell(mousePos);

        mousePos.z = 0;

        CheckMouseOverCollider();

        TakeFromBP();

        DropBuildingOnOtherBuilding();
        

        if (choosenBuilding != null && _GridHighlight.OnBackpack(PosUpdater(cellPos, choosenBuilding.getCellsPos())))
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

    private void CheckMouseOverCollider()
    {
        curCollider = Physics2D.OverlapPoint(mousePos);
        //OverlapPoint проверяет если мышка на коллайдере  Physics2D.OverlapBox(mousePos, new Vector2(1.75f, 1.75f), 0);

        if (curCollider == null) return;

        if (!curCollider.CompareTag("Tower") || !isCursorEmpty) return;
        
        curBuilding = curCollider.gameObject;
        choosenBuilding = curBuilding.transform.parent.GetComponent<Build>().prefabBuilding;
        //Важная штука, меняет текущий геймобжэкт на тот, на который указывает мышка
    }
    
    private void TakeFromMarket()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (!_Market.ContainsCellPos(cellPos)) return;
        
        choosenBuilding = _Market.GetBuilding(cellPos);

        CreateSprite(new Quaternion(0, 0, 0, 0), _Market.GetCellsPos(choosenBuilding));
        
        _Market.DeleteTiles(choosenBuilding);
        
        _Market.RenderTiles();
        
       
        //Спавнит спрайт тайла с магаза
    }

    private void MovingInStorage()
    {
        if(!curBuilding) return;
        
        if (Input.GetMouseButton(0))
        {
            if (!isCursorEmpty || (curCollider != null && curCollider.CompareTag("Tower")))
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
        curBuilding.transform.parent.transform.position = mousePos + new Vector3(0.75f, 0.75f);
        //Двигает в рюкзачке
    }

     
    private IEnumerator RotationBuilding()
    {
        if (isRotating)
        {
            yield break;
        }
        
        choosenBuilding.setCellsPos(RotateCords(choosenBuilding.getCellsPos()));
        
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
        if (!Input.GetMouseButtonUp(0) || !curBuilding  || isCursorEmpty || _BpData.ContainsCellPos(cellPos)) return;
        
        isCursorEmpty = true;
            
        if(isRotating) return;
        
        //choosenBuilding.setCellsPos(PosUpdater(cellPos, choosenBuilding.getCellsPos()));
        
        choosenBuilding.setRotation(curBuilding.transform.parent.rotation);
        
        _BpData.buildingsBP.Add(PosUpdater(cellPos, choosenBuilding.getCellsPos()), choosenBuilding);
        
        _BpData.RenderTiles();
        
        ClearChildren();
        //Ставит в рюкзачек
    }
    
    private void DropBuildingOnOtherBuilding()
    {
        // if (!Input.GetMouseButtonUp(0) || !curBuilding  || isCursorEmpty || !_BpData.buildingsBP.ContainsKey(cellPos)) return;
        //
        // isCursorEmpty = true;
        //     
        // if(isRotating) return; 
        //
        // Quaternion prevRotation = tilemap.GetTransformMatrix(cellPos).rotation;
        //
        // tilemap.SetTile(cellPos, choosenBuilding.getTile());
        // tilemap.SetTransformMatrix(cellPos, Matrix4x4.TRS(Vector3.zero, curBuilding.transform.parent.rotation, Vector3.one));
        //
        // Building prevBuild = _BpData.buildingsBP[cellPos];
        //
        // _BpData.DeleteTile(cellPos, prevBuild);
        // _BpData.buildingsBP.Add(cellPos, choosenBuilding);
        //
        // Destroy(curBuilding);
        //
        // choosenBuilding = prevBuild;
        //
        // CreateSprite(prevRotation);
        // //Ставит в рюкзачек
    }

    private void TakeFromBP()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (!_BpData.ContainsCellPos(cellPos)) return;

        choosenBuilding = _BpData.GetBuilding(cellPos);
        
        CreateSprite(choosenBuilding.getRotation(), _BpData.GetCellsPos(choosenBuilding));
        
        _BpData.DeleteTiles(choosenBuilding);
        
    }

    private void CreateSprite(Quaternion rotation, List<Vector3Int> storagePos)
    {
        // curBuilding = Instantiate(spritePrefab, mousePos, rotation);
        // curBuilding.GetComponent<Build>().prefabBuilding = choosenBuilding;
        //
        // curBuilding.GetComponent<SpriteRenderer>().sprite = choosenBuilding.getSprite();
        
        GameObject curBuilding = Instantiate(spritePrefab, mousePos, rotation);
        curBuilding.tag = "Tower";

        curBuilding.GetComponent<Build>().prefabBuilding = choosenBuilding;
        
        foreach (var curPos in storagePos) {
            
            Vector3Int relativePosition = new Vector3Int(curPos.x - cellPos.x, curPos.y - cellPos.y, 0);

            GameObject newTile = new GameObject();
                
            GameObject tileClone = Instantiate(newTile, curBuilding.transform.position + new Vector3((relativePosition.x * 1.75f), 
                (relativePosition.y * 1.75f), 0), rotation);
            
            Destroy(newTile);
            
            tileClone.AddComponent<SpriteRenderer>().GetComponent<SpriteRenderer>().sprite = choosenBuilding.getSprite();

            tileClone.AddComponent<BoxCollider2D>();

            tileClone.tag = "Tower";

            tileClone.transform.parent = curBuilding.transform;
        }
    }
    
    public List<Vector3Int> PosUpdater(Vector3Int currentPosition, List<Vector3Int> positions)
    {
        List<Vector3Int> updatedPositions = new List<Vector3Int>();

        foreach (Vector3Int position in positions)
        {
            // Вычисляем разницу между текущей позицией и позицией из списка
            Vector3Int difference = currentPosition + position;

            // Добавляем измененную позицию в список
            updatedPositions.Add(difference);
            
        }

        return updatedPositions;
    }
    
    public void ClearChildren()
    {
        Transform parent = curBuilding.transform.parent;
        
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[curBuilding.transform.parent.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in curBuilding.transform.parent)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            Destroy(child.gameObject);
        }

        Destroy(parent.gameObject);
    }

    public List<Vector3Int> RotateCords(List<Vector3Int> tileCoordinates)
    {
        List<Vector3Int> rotatedCoordinates = new List<Vector3Int>();

        foreach (Vector3Int coords in tileCoordinates)
        {
            rotatedCoordinates.Add(new Vector3Int(-coords.y, coords.x, coords.z));
        }

        return rotatedCoordinates;
    }
    
    
}