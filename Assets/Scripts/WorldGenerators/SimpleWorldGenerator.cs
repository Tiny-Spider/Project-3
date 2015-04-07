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

        byte[,] worldData = new byte[width, height];

        Debug.Log("Generating: " + width + ", " + height);

        // Walls
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

              if (y == 0 || y == height - 1 || x == 0 || x == width - 1) {
                  worldData[x, y] = empty.id;
              }
              if (x > 0 && x < width - 1 && y > 0 && y < height - 1) {
                    if (y == 1 || y == height - 2 || x == 1 || x == width - 2) {
                        worldData[x, y] = woodWall.id;
                    }
               }

            }
        }

        //worldData[0, 0] = tree.id;
        //worldData[1, 1] = tree.id;
        //worldData[width, height] = tree.id;
        //worldData[width - 1, height - 1] = tree.id;
        //worldData[width - 2, height - 2] = tree.id;

        // Tree Gen
        int treeAmount = Random.Range((height * width) / minTrees, (height * width) / maxTrees);
        for (int i = 0; i < treeAmount; i++) {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            while (worldData[x, y] != 0) {
                x = Random.Range(0, width);
                y = Random.Range(0, height);
            }

            //Debug.Log("Spawning tree at: " + x + ", " + y);

            worldData[x, y] = tree.id;
        }

        // Pillar Gen
        int pillarAmount = Random.Range((height * width) / minPillars, (height * width) / maxPillars);
        for (int i = 0; i < pillarAmount; i++) {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            while (worldData[x, y] != 0) {
                x = Random.Range(0, width);
                y = Random.Range(0, height);
            }

            //Debug.Log("Spawning pillar at: " + x + ", " + y);

            worldData[x, y] = pillar.id;
        }

        // Rock Gen
        int rockAmount = Random.Range((height * width) / minRocks, (height * width) / maxRocks);
        for (int i = 0; i < rockAmount; i++) {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            while (worldData[x, y] != 0) {
                x = Random.Range(0, width);
                y = Random.Range(0, height);
            }

            //Debug.Log("Spawning rock at: " + x + ", " + y);

            worldData[x, y] = rock.id;
        }

        world.worldData = worldData;
        GenerateTerrain();
    }
}
