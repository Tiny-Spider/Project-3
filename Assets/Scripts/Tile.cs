using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public bool hasDirections = false;

    private Direction currentDirection = Direction.North;

    public GameObject northGameObject;
    public GameObject eastGameObject;
    public GameObject southGameObject;
    public GameObject westGameObject;

    public void SetDirection(params Direction[] directions) {
        if (!hasDirections)
            return;

        foreach (Direction direction in directions) {
            switch (direction) {
                case Direction.North:
                    northGameObject.SetActive(true);
                    break;
                case Direction.East:
                    eastGameObject.SetActive(true);
                    break;
                case Direction.South:
                    southGameObject.SetActive(true);
                    break;
                case Direction.West:
                    westGameObject.SetActive(true);
                    break;
            }
        }
    }

    public void ClearDirections() {
        northGameObject.SetActive(false);
        eastGameObject.SetActive(false);
        southGameObject.SetActive(false);
        westGameObject.SetActive(false);
    }

    public void Rotate(Direction direction) {
        transform.rotation = Quaternion.AngleAxis((int) direction, Vector3.up);
    }
}
