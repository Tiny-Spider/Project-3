using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleWorldGenerator : WorldGenerator {
    private TileData pillar;
    private TileData rock;
    private TileData tree;

    private void LoadTiles() {
        pillar =    LoadTile("pillar", true);
        rock =      LoadTile("rock", true);
        tree =      LoadTile("tree", true);
    }

    public override void Generate(int height, int width) {
        LoadTiles();

        byte[,] worldData = new byte[height, width];
        HashSet<Vector2> usedPositions = new HashSet<Vector2>();
        
        // Tree Gen

        int treeAmount = Random.Range((height * width) / 40, (height * width) / 10);
        for (int i = 0; i < treeAmount; i++) {
            Vector2 position = new Vector2(Random.Range(0, width), Random.Range(0, height));

            while (usedPositions.Contains(position)) {
                position = new Vector2(Random.Range(0, width), Random.Range(0, height));
            }

            //if (world.worldData)

            Debug.Log("Spawning tree at: " + position.ToString());

            usedPositions.Add(position);
            SpawnTile(tree.tile, position);

            worldData[(int) position.x, (int) position.y] = tree.id;
        }

        // Pillar Gen

        int pillarAmount = Random.Range((height * width) / 60, (height * width) / 40);
        for (int i = 0; i < pillarAmount; i++) {
            Vector2 position = new Vector2(Random.Range(0, width), Random.Range(0, height));

            while (usedPositions.Contains(position)) {
                position = new Vector2(Random.Range(0, width), Random.Range(0, height));
            }

            Debug.Log("Spawning pillar at: " + position.ToString());

            usedPositions.Add(position);
            SpawnTile(pillar.tile, position);

            worldData[(int)position.x, (int)position.y] = pillar.id;
        }

        // Rock Gen

        int rockAmount = Random.Range((height * width) / 50, (height * width) / 30);
        for (int i = 0; i < rockAmount; i++) {
            Vector2 position = new Vector2(Random.Range(0, width), Random.Range(0, height));

            while (usedPositions.Contains(position)) {
                position = new Vector2(Random.Range(0, width), Random.Range(0, height));
            }

            Debug.Log("Spawning rock at: " + position.ToString());

            usedPositions.Add(position);
            SpawnTile(rock.tile, position);

            worldData[(int)position.x, (int)position.y] = rock.id;
        }

        world.worldData = worldData;
    }

    //public bool NextTo(byte[,] worldData, Vector2 position, byte checkFor) {
    //    if (position.x > 0 && position.y > 0)

    //}
}
