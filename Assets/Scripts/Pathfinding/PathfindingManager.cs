using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathfindingManager : MonoBehaviour {
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathfindingManager instance;
    Pathfinding pathfinding;

    bool isProcessingPath;

    void Awake() {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callBack) {
        PathRequest pathRequest = new PathRequest(pathStart, pathEnd, callBack);

        instance.pathRequestQueue.Enqueue(pathRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext() {
        if (!isProcessingPath && pathRequestQueue.Count > 0) {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        } 
    }

    public void FinishedProcessingPath(Vector3[] path, bool succes) {
        currentPathRequest.callBack(path, succes);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callBack;

        public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callBack) {
            this.pathStart = pathStart;
            this.pathEnd = pathEnd;
            this.callBack = callBack;
        }
    }
}
