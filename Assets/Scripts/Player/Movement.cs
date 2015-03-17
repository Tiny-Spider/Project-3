using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Player player;
    private Rigidbody2D _rigidBody2D;


    void Awake() 
    {
        player = GetComponent<Player>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        direction.Normalize();
        _rigidBody2D.MovePosition((Vector2)transform.position + direction * player.speed * Time.deltaTime);
    }
}
