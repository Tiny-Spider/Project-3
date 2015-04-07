using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {
    private static List<TileData> tilesData;
    private static TileData[] tilesDataLookup;

    void Awake() {
        tilesData = new List<TileData>(Resources.LoadAll<TileData>("Tiles"));

        if (tilesData.Count == 0) {
            Debug.LogError("No tiles loaded! Please add a \"Tiles\" folder in a \"Resources\" folder with tiles.");
            return;
        }

        tilesDataLookup = new TileData[byte.MaxValue + 1];

        foreach (TileData tileData in tilesData) {
            tilesDataLookup[tileData.id] = tileData;
        }
    }

    public static TileData GetTileData(string name) {
        if (tilesData == null) {
            Debug.LogError("TileManager is not instantiated!");
            Debug.Break();
        }

        foreach (TileData tileData in tilesData) {
            if (tileData.techName.EqualsIgnoreCase(name)) {
                return tileData;
            }
        }

        return tilesData[0];
    }

    public static TileData GetTileData(byte id) {
        if (tilesDataLookup == null) {
            Debug.LogError("TileManager is not instantiated!");
            Debug.Break();
        }

        return tilesDataLookup[id];
    }

    public static string GetTileName(Tile tile) {
        if (tilesData == null) {
            Debug.LogError("TileManager is not instantiated!");
            Debug.Break();
        }

        foreach (TileData tileData in tilesData) {
            if (tileData.tile.Equals(tile)) {
                return tileData.name;
            }
        }

        return tilesData[0].name;
    }
}
