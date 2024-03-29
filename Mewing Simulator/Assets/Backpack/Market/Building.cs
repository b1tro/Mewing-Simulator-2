using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Building
{
    private Tile tile;
    private List<Vector3Int> cellsPos = new List<Vector3Int>();
    private Quaternion rotation = Quaternion.identity;

    public Tile getTile()
    {
        return this.tile;
    }

    public void setTile(Tile tile)
    {
        this.tile = tile;
    }

    public Sprite getSprite()
    {
        return this.getTile().sprite;
    }

    public void setCellsPos(List<Vector3Int> cellsPos)
    {   
        this.cellsPos.Clear();
        this.cellsPos = cellsPos;
    }

    public List<Vector3Int> getCellsPos()
    {
        return this.cellsPos;
    }

    public void addCellPos(Vector3Int cellPos)
    {
        this.cellsPos.Add(cellPos);
    }

    public void removeCellPos(Vector3Int cellPos)
    {
        this.cellsPos.Remove(cellPos);
    }

    public Quaternion getRotation()
    {
        return this.rotation;
    }

    public void setRotation(Quaternion rotation)
    {
        this.rotation = rotation;
    }
}

public class House : Building
{
    public House()
    {
        this.setTile(Resources.Load<Tile>("Building1"));
    }
}
public class Bank : Building
{
    
    public Bank()
    {
        this.setTile(Resources.Load<Tile>("Building2"));
    }
}
