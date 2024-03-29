using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileEditor))] public class TilemapEditor : Editor { public override void OnInspectorGUI() { TileEditor tileEditor = (TileEditor)target;

 
        GUILayout.Label("Custom Tile Editor");

        for (int y = 0; y < 5; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < 5; x++)
            {
                Rect buttonRect = GUILayoutUtility.GetRect(30, 30);
                if (GUI.Button(buttonRect, tileEditor.GetTile(x, y) != null ? tileEditor.GetTile(x, y).name : ""))
                {
                    tileEditor.AddTile(x, y);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Save Tiles as Prefab"))
        {
            tileEditor.SaveTiles();
        }

        if (GUILayout.Button("Refresh"))
        {
            tileEditor.RefreshMap();
        }
    }
}