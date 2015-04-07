using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
    public bool displayGridGizmos = false;

    Node[,] grid;
    World world;

    int gridSizeX, gridSizeY;

    void Start() {
        world = Component.FindObjectOfType<World>();

        gridSizeX = world.worldData.GetLength(0);
        gridSizeY = world.worldData.GetLength(1);

        CreateGrid();
    }

    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                byte tile = world.worldData[x, y];
                TileData tileData = TileManager.GetTileData(tile);
                int movementPenalty = tileData ? tileData.movementPenalty : 0;

                grid[x, y] = new Node(tile == 0, new Vector2(x, y), x, y, movementPenalty);
            }
        }
    }

    public Node GetNode(int x, int y) {
        x = Mathf.Clamp(x, 0, grid.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, grid.GetLength(1) - 1);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition) {
        int x = Mathf.RoundToInt(worldPosition.x);
        int y = Mathf.RoundToInt(worldPosition.y);

        x = Mathf.Clamp(x, 0, grid.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, grid.GetLength(1) - 1);

        return grid[x, y];
    }

    void OnDrawGizmos() {
        if (grid != null && displayGridGizmos) {
            foreach (Node node in grid) {
                Gizmos.color = node.walkable ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * 0.9F);
            }
        }
    }
}
