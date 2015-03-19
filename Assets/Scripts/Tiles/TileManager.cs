using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {
    private List<TileData> tilesData = new List<TileData>();

    void Awake() {
        tilesData = new List<TileData>(Resources.LoadAll<TileData>("Tiles"));

        if (tilesData.Count == 0) {
            Debug.LogError("No tiles loaded! Please add a \"Tiles\" folder in a \"Resources\" folder with tiles.");
        }
    }

    public TileData GetTile(string name) {
        foreach (TileData tileData in tilesData) {
            if (tileData.techName.EqualsIgnoreCase(name)) {
                return tileData;
            }
        }

        return tilesData[0];
    }

    public string GetName(Tile tile) {
        foreach (TileData tileData in tilesData) {
            if (tileData.tile.Equals(tile)) {
                return tileData.name;
            }
        }

        return tilesData[0].name;
    }
}
