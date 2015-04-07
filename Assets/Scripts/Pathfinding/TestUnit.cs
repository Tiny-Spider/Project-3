using UnityEngine;
using System.Collections;

public class TestUnit : MonoBehaviour {

    public Transform target;
    public float speed = 5;
    Vector3[] path;
    int targetIndex;

    void Start() {
        StartCoroutine(FindPath());
    }
    
    IEnumerator FindPath() {
        //while (true) {
            yield return new WaitForSeconds(1F);
            PathfindingManager.RequestPath(transform.position, target.position, OnPathFound);
        //}
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesful) {
        if (pathSuccesful) {
            StopCoroutine(FollowPath());

            path = newPath;
            targetIndex = 0;

            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        Debug.Log("FOLLOW PATH");
        Debug.Log("TARGET: " + path[path.Length - 1].ToString());


        while (true) {
            if (transform.position == currentWaypoint) {
                targetIndex++;

                if (targetIndex >= path.Length) {
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDrawGizmos() {
        if (path != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(path[path.Length - 1], Vector3.one * 1.1F);

            Gizmos.color = Color.black;
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
