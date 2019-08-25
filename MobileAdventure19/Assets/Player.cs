using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    Vector2 playerPos;
    public float playerSpeed = .2f;

    private void Start()
    {
        playerPos = transform.position;
        
    }
    void FixedUpdate()
    {
        //Call function(s) every frame
        PlayerMovement();

        transform.position = playerPos;
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerPos.x -= playerSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerPos.x += playerSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            playerPos.y += playerSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerPos.y -= playerSpeed;
        }
    }
}
