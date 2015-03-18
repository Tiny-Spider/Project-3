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

    protected void SpawnTile(Tile tile, Vector2 position) {
        SpawnTile(tile, (int)position.x, (int)position.y);
    }

    protected void SpawnTile(Tile tile, int x, int y) {
        Tile spawnedTile = GameObject.Instantiate(tile);
        spawnedTile.transform.position = new Vector2(x, y);
        spawnedTile.gameObject.name = x + ", " + y;
    }

    public abstract void Generate(int height, int width);

    protected TileData LoadTile(string tileName, bool important) {
        TileData tile = tileManager.GetTile(tileName);

        //if (tile == null)
        //    throw new NotSupportedException();

        return tile;
    }
}
