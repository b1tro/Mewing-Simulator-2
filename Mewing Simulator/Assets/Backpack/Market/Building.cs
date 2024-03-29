using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Building
{
    [SerializeField]
    public Tile tile;
    [SerializeField]
    public List<Vector3Int> cellsPos;

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

        this.cellsPos = cellsPos;
    }

    public List<Vector3Int> getCellsPos()
    {
        return this.cellsPos;
    }
}

[System.Serializable]
public class House : Building
{
    public House()
    {
        this.setTile(Resources.Load<Tile>("Building1"));
    }
}

[System.Serializable]
public class Bank : Building
{
    public Bank()
    {
        this.setTile(Resources.Load<Tile>("Building2"));
    }
}
