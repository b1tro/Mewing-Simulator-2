using UnityEngine;
using UnityEngine.Tilemaps;


public class Building
{
    public Tile tile;

    public Tile getTile()
    {
        return this.tile;
    }
}

public class House : Building
{
    public House()
    {
        tile = Resources.Load<Tile>("Building1");
        Debug.Log(tile);
    }
}
public class Bank : Building
{
    
    public Bank()
    {
        tile = Resources.Load<Tile>("Building2");
        Debug.Log(tile);
    }
}
