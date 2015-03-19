using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
    public byte[,] worldData;
    public WorldGenerator worldGenerator = new SimpleWorldGenerator();
    public TileManager tileManager;
    public string seed;

    void Start() {
        worldGenerator.GenerateWorld(this, 50, 50, seed, tileManager);
    }
}
