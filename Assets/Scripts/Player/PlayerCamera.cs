using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class PlayerCamera : MonoBehaviour {

    public Transform playerPosition;
    public float cameraDistance = -10;
    public float lerpSpeed;
    public float yOffset;
	
	// Update is called once per frame
    void Update() {
        Vector3 targetPosition = playerPosition.position;
        targetPosition.z += +cameraDistance;
        targetPosition.y += yOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);


    }
}
