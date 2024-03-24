using UnityEngine;
using UnityEngine.Tilemaps;


public class Building
{
    private Tile tile;

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
