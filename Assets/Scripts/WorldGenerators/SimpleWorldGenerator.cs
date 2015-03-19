using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleWorldGenerator : WorldGenerator {
    private TileData pillar;
    private TileData rock;
    private TileData tree;
    private TileData empty;
    private TileData woodWall;

    // Minimal is minimal tree/blocks and maximal is maximal tree/blocks
    int minTrees = 200, maxTrees = 100;
    int minPillars = 300, maxPillars = 200;
    int minRocks = 150, maxRocks = 200;

    private void LoadTiles() {
        pillar =    LoadTile("pillar", true);
        rock =      LoadTile("rock", true);
        tree =      LoadTile("tree", true);
        empty =     LoadTile("empty", true);
        woodWall =  LoadTile("wallWood", true);
    }

    public override void Generate(int height, int width) {
        LoadTiles();

        byte[,] worldData = new byte[height, width];
        HashSet<Vector2> usedPositions = new HashSet<Vector2>();

        // Walls

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                if (y == 1 || y == height - 1 || x == 1 || x == width - 1) {
                    worldData[x, y] = woodWall.id;

                    usedPositions.Add(new Vector2(x, y));
                }
                if (y == 0 || y == height || x == 0 || x == width) {
                    worldData[x, y] = empty.id;

                    usedPositions.Add(new Vector2(x, y));
                }
            }
        }


        // Tree Gen

        int treeAmount = Random.Range((height * width) / minTrees, (height * width) / maxTrees);
        for (int i = 0; i < treeAmount; i++) {
            Vector2 position = new Vector2(Random.Range(0, width), Random.Range(0, height));

            while (usedPositions.Contains(position)) {
                position = new Vector2(Random.Range(0, width), Random.Range(0, height));
            }

            //if (world.worldData)

            Debug.Log("Spawning tree at: " + position.ToString());

            usedPositions.Add(position);

            worldData[(int) position.x, (int) position.y] = tree.id;
        }

        // Pillar Gen

        int pillarAmount = Random.Range((height * width) / minPillars, (height * width) / maxPillars);
        for (int i = 0; i < pillarAmount; i++) {
            Vector2 position = new Vector2(Random.Range(0, width), Random.Range(0, height));

            while (usedPositions.Contains(position)) {
                position = new Vector2(Random.Range(0, width), Random.Range(0, height));
            }

            Debug.Log("Spawning pillar at: " + position.ToString());

            usedPositions.Add(position);

            worldData[(int)position.x, (int)position.y] = pillar.id;
        }

        // Rock Gen

        int rockAmount = Random.Range((height * width) / minRocks, (height * width) / maxRocks);
        for (int i = 0; i < rockAmount; i++) {
            Vector2 position = new Vector2(Random.Range(0, width), Random.Range(0, height));

            while (usedPositions.Contains(position)) {
                position = new Vector2(Random.Range(0, width), Random.Range(0, height));
            }

            Debug.Log("Spawning rock at: " + position.ToString());

            usedPositions.Add(position);

            worldData[(int)position.x, (int)position.y] = rock.id;
        }

        world.worldData = worldData;
        GenerateTerrain();
    }
}
