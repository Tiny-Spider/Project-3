using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
    public byte[,] worldData;
    public WorldGenerator worldGenerator = new SimpleWorldGenerator();
    public TileManager tileManager;
    public string seed;
    public int width, height;

    void Awake() {
        worldGenerator.GenerateWorld(this, width, height, seed, tileManager);
    }

    public void GenerateRandom() {
        Transform worldHolder = GameObject.Find("World Holder").transform;

        foreach (Transform child in worldHolder) {
            Destroy(child.gameObject);
        }

        worldGenerator.GenerateWorld(this, width, height, tileManager);
    }
}
