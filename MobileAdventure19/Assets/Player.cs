using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    private Vector3 playerPos;
    private Rigidbody2D rb;

    public float playerSpeed = .2f;


    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        
    }
    void FixedUpdate()
    {
        playerPos = Vector3.zero;
        playerPos.x = Input.GetAxisRaw("Horizontal");
        playerPos.y = Input.GetAxisRaw("Vertical"); 
        if (playerPos != Vector3.zero)
        {
            PlayerMovement();
        }
    }

    void PlayerMovement()
    {
        rb.MovePosition(transform.position + playerPos * playerSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Collider")
        {
            transform.position = new Vector2(0, 5);
            
        }

    }
}
