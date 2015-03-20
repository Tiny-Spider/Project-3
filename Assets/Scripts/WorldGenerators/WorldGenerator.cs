using UnityEngine;
using System.Collections;

public abstract class WorldGenerator {
    private TileManager tileManager;
    protected World world;

    public void GenerateWorld(World world, int height, int width, string seed, TileManager tileManager) {
        Random.seed = seed.GetHashCode();

        GenerateWorld(world, height, width, tileManager);
    }

    public void GenerateWorld(World world, int height, int width, TileManager tileManager) {
        this.world = world;
        this.tileManager = tileManager;

        Generate(height, width);
    }

    public abstract void Generate(int height, int width);

    protected void GenerateTerrain() {
        byte[,] worldData = world.worldData;

        for (int x = 0; x < worldData.GetLength(0); x++) {
            for (int y = 0; y < worldData.GetLength(1); y++) {
                Tile tile = tileManager.GetTile(worldData[x, y]).tile;

                if (tile != null) {
                    Tile spawnedTile = GameObject.Instantiate(tile);
                    spawnedTile.transform.position = new Vector2(x, y);
                    spawnedTile.gameObject.name = x + ", " + y;
                }
            }
        }
    }

    protected TileData LoadTile(string tileName, bool important) {
        TileData tile = tileManager.GetTile(tileName);

        if (tile == null && important) {
            throw new UnassignedReferenceException("World Gen: Important tile could not be found! Tile: " + tileName);
        }

        return tile;
    }
}
