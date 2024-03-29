using System.Collections.Generic; using System.Xml; using UnityEngine; using UnityEditor;

public class TileEditor : MonoBehaviour { private GameObject[,] tiles = new GameObject[5, 5];

 
    private List<Vector3Int> cellPosEdit = new List<Vector3Int>();

    [SerializeField]
    public RenderTile renderTile;

    public void AddTile(int x, int y)
    {
        if (tiles[x, y] == null)
        {
            GameObject tile = new GameObject();

            tile.tag = "Tower";
            tile.transform.position = new Vector3(x - 5, y - 5, 0);

            tile.transform.parent = transform;

            tiles[x, y] = tile;

            cellPosEdit.Add(new Vector3Int(x - 5, y - 5, 0));
            Debug.Log(new Vector3Int(x - 5 , y - 5, 0));
        }
        else
        {
            DestroyImmediate(tiles[x, y]);
            tiles[x, y] = null;

            cellPosEdit.Remove(new Vector3Int(x + 5, y + 5, 0));
        }
    }

    public void SaveTiles()
    {
        renderTile = GetComponent<RenderTile>();
        renderTile.storage.Add(cellPosEdit, new Bank());
        renderTile.storage[cellPosEdit].setCellsPos(cellPosEdit);
        renderTile.RenderStart();
    }

    public void RefreshMap()
    {
        cellPosEdit.Clear();

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                tiles[x, y] = null;
            }
        }

        GameObject[] tilesToDelete = GameObject.FindGameObjectsWithTag("Tower");

        foreach (var item in tilesToDelete)
        {
            DestroyImmediate(item);
        }
    }

    public GameObject GetTile(int x, int y)
    {
        if (x >= 0 && x < 5 && y >= 0 && y < 5)
        {
            if (tiles[x, y] != null)
            {
                return tiles[x, y];
            }
        }
        return null;
    }
}