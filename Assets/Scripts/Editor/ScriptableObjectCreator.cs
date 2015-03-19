using UnityEngine;
using UnityEditor;
using System.Collections;

public class ScriptableObjectCreator {
    [MenuItem("Assets/Create/Tile")]
    public static void CreateTile() {
        TileData asset = ScriptableObject.CreateInstance<TileData>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Tiles/Tile.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
