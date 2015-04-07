using UnityEngine;
using System.Collections;

public abstract class WorldGenerator {
    private TileManager tileManager;
    private Transform worldHolder;

    protected World world;

    public void GenerateWorld(World world, int height, int width, string seed, TileManager tileManager) {
        Random.seed = seed.GetHashCode();

        GenerateWorld(world, height, width, tileManager);
    }

    public void GenerateWorld(World world, int height, int width, TileManager tileManager) {
        this.world = world;
        this.tileManager = tileManager;
        worldHolder = GameObject.Find("World Holder").transform;

        Generate(height, width);
    }

    public abstract void Generate(int height, int width);

    protected void GenerateTerrain() {
        byte[,] worldData = world.worldData;

        for (int x = 0; x < worldData.GetLength(0); x++) {
            for (int y = 0; y < worldData.GetLength(1); y++) {
                TileData tile = TileManager.GetTileData(worldData[x, y]);

                if (tile != null && tile.tile != null) {
                    Tile spawnedTile = GameObject.Instantiate(tile.tile);
                    spawnedTile.transform.position = new Vector2(x, y);
                    spawnedTile.transform.SetParent(worldHolder);
                    spawnedTile.gameObject.name = new Vector2(x, y).ToString();

                    if (spawnedTile.hasDirections) {
                        // West
                        if (x > 0 && worldData[x - 1, y] == tile.id) {
                            spawnedTile.SetDirection(Direction.West);
                        }

                        // East
                        if (x < worldData.GetLength(0) - 1 && worldData[x + 1, y] == tile.id) {
                            spawnedTile.SetDirection(Direction.East);
                        }

                        // South
                        if (y > 0 && worldData[x, y - 1] == tile.id) {
                            spawnedTile.SetDirection(Direction.South);
                        }

                        // North
                        if (y < worldData.GetLength(1) - 1 && worldData[x, y + 1] == tile.id) {
                            spawnedTile.SetDirection(Direction.North);
                        }
                    }
                }
            }
        }
    }

    protected TileData LoadTile(string tileName, bool important) {
        TileData tile = TileManager.GetTileData(tileName);

        if (tile == null && important) {
            throw new UnassignedReferenceException("World Gen: Important tile could not be found! Tile: " + tileName);
        }

        return tile;
    }
}
