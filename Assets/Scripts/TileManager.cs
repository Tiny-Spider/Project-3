using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {
    public List<TileData> tilesData = new List<TileData>();

    public Tile GetTile(string name) {
        foreach (TileData tileData in tilesData) {
            if (tileData.name.EqualsIgnoreCase(name)) {
                return tileData.tile;
            }
        }

        return tilesData[0].tile;
    }

    public string GetName(Tile tile) {
        foreach (TileData tileData in tilesData) {
            if (tileData.tile.Equals(tile)) {
                return tileData.name;
            }
        }

        return tilesData[0].name;
    }

    [Serializable]
    public class TileData {
        public string name;
        public Tile tile;
    }
}
