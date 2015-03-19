using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    private Player player;
    private Rigidbody2D _rigidBody2D;
    public Camera playerCamera;

    void Awake() {
        player = GetComponent<Player>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 direction = new Vector2((int)Input.GetAxis("Horizontal"), (int)Input.GetAxis("Vertical"));
        direction.Normalize();

        if (direction.sqrMagnitude > 0.1)
        _rigidBody2D.MovePosition((Vector2)transform.position + (direction * player.speed) * Time.deltaTime);

        Vector2 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition);
        //transform.rotation = transform.LookAt()
    }
}
