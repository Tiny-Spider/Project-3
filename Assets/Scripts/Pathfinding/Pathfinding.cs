using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {

    PathfindingManager pathfindingManager;
    Grid grid;

    void Awake() {
        grid = GetComponent<Grid>();
        pathfindingManager = GetComponent<PathfindingManager>();
    }

    public void StartFindPath(Vector3 startPosition, Vector3 targetPosition) {
        StartCoroutine(FindPath(startPosition, targetPosition));
    }

    IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition) {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        Vector3[] wayPoints = new Vector3[0];
        bool pathSucces = false;

        Node startNode = grid.NodeFromWorldPoint(startPosition);
        Node targetNode = grid.NodeFromWorldPoint(targetPosition);

        if (startNode.walkable && targetNode.walkable) {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode) {
                    stopWatch.Stop();
                    UnityEngine.Debug.Log("Path found in " + stopWatch.ElapsedMilliseconds + "ms.");

                    pathSucces = true;
                    break;
                }



                foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
                    if (/*!neighbour.walkable ||*/ closedSet.Contains(neighbour)) {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {

                        // Calculate corner
                        if (neighbour.gridX != currentNode.gridX && neighbour.gridY != currentNode.gridY) {
                            UnityEngine.Debug.Log("Checking node: " + neighbour.worldPosition.ToString() + " | " + currentNode.worldPosition.ToString());

                            int gridX = neighbour.gridX + (neighbour.gridX > currentNode.gridX ? -1 : 1);
                            int gridY = neighbour.gridY + (neighbour.gridY > currentNode.gridY ? -1 : 1);

                            Node nodeA = grid.GetNode(gridX, currentNode.gridY);
                            Node nodeB = grid.GetNode(currentNode.gridX, gridY);

                            if (!nodeA.walkable || !nodeB.walkable) {
                                continue;
                            }
                        }

                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        }
                        else {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }

        yield return null;

        if (pathSucces) {
            wayPoints = RetracePath(startNode, targetNode);
        }

        pathfindingManager.FinishedProcessingPath(wayPoints, pathSucces);
    }

    Vector3[] RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Vector3[] waypoints = SimplifyPath(path); // new Vector3[path.Count];

        //
        //for( int i =0 ; i < path.Count; i++) {
        //    waypoints[i] = path[i].worldPosition;
        //}
        //

        Array.Reverse(waypoints);

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        waypoints.Add(path[0].worldPosition);

        for (int i = 1; i < path.Count; i++) {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);

            if (directionNew != directionOld) {
                waypoints.Add(path[i].worldPosition);
            }

            directionOld = directionNew;
        }


        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB) {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY) {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
