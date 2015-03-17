using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private Player player;


    void Awake() 
    {
        player = GetComponent<Player>();
    }

    void Update() {
        if (Input.GetKey(KeyManager.moveUp))
        {
            gameObject.transform.Translate(Vector3.forward* player.speed *Time.deltaTime);
        }

        if (Input.GetKey(KeyManager.moveDown))
        {
            gameObject.transform.Translate(Vector3.forward * -player.speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyManager.moveRight))
        {
            gameObject.transform.Translate(Vector3.right * player.speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyManager.moveLeft))
        {
            gameObject.transform.Translate(Vector3.right * -player.speed * Time.deltaTime);
        }
    }
}
