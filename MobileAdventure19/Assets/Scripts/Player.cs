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
        //Find components
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //call functions
        PlayerController();

        //Find gamemode script
        GameObject gamemode = GameObject.Find("Gamemode");
        Gamemode gamemodeScript = gamemode.GetComponent<Gamemode>();

        //If the game is NOT over
        if (!gamemodeScript.playerLost)
        {
            //Player Movement
            playerPos = Vector3.zero;
            playerPos.x = Input.GetAxisRaw("Horizontal");
            playerPos.y = Input.GetAxisRaw("Vertical");
            if (playerPos != Vector3.zero)
            {
                //The functionality behind the movement
                rb.MovePosition(transform.position + playerPos * playerSpeed * Time.deltaTime);
            }
        }
    }

    void PlayerController()
    {
        //Find gamemode script
        GameObject gamemode = GameObject.Find("Gamemode");
        Gamemode gamemodeScript = gamemode.GetComponent<Gamemode>();

        //If the game IS over
        if (gamemodeScript.playerLost)
        {
            {
                //If the player presses space 
                if (Input.GetKeyDown("space"))
                {
                    gamemodeScript.playerLost = false;

                    //Set blockades to false
                    gamemodeScript.eastBlockade.SetActive(false);
                    gamemodeScript.northBlockade.SetActive(false);
                    gamemodeScript.westBlockade.SetActive(false);
                    gamemodeScript.southBlockade.SetActive(false);

                    //Call functions
                    gamemodeScript.StartGame();
                }
            }
        }
    }

    //Upon colliding with object called 'collider' reset position
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Find gamemode script
        GameObject gamemode = GameObject.Find("Gamemode");
        Gamemode gamemodeScript = gamemode.GetComponent<Gamemode>();

                                                                                             //The four options for getting the answer correct
        //Going East
        if (other.gameObject.name == "Collider East" && gamemodeScript.wrongAns != 1)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(0, 5);

            //calling coroutine / functions
            StartCoroutine("SetWin");
            gamemodeScript.BuildEastBlock();
            return;
        }

        //Going North
        if (other.gameObject.name == "Collider North" && gamemodeScript.wrongAns != 2)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(0, 5);

            //calling coroutine / functions
            StartCoroutine("SetWin");
            gamemodeScript.BuildNorthBlock();
            return;
        }

        //Going West
        if (other.gameObject.name == "Collider West" && gamemodeScript.wrongAns != 3)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(0, 5);

            //calling coroutine / functions
            StartCoroutine("SetWin");
            gamemodeScript.BuildWestBlock();
            return;
        }

        //Going South
        if (other.gameObject.name == "Collider South" && gamemodeScript.wrongAns != 4)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(0, 5);

            //calling coroutine / functions
            StartCoroutine("SetWin");
            gamemodeScript.BuildSouthBlock();
            return;
        }

                                                                                             //The four options for getting the answer Wrong
        //Going East
        if (other.gameObject.name == "Collider East" && gamemodeScript.wrongAns == 1)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(0, 5);

            //calling coroutine
            StartCoroutine("SetLose");
            return;
        }

        //Going North
        if (other.gameObject.name == "Collider North" && gamemodeScript.wrongAns == 2)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(0, 5);

            //calling coroutine
            StartCoroutine("SetLose");
            return;

        }

        //Going West
        if (other.gameObject.name == "Collider West" && gamemodeScript.wrongAns == 3)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(0, 5);

            //calling coroutine
            StartCoroutine("SetLose");
            return;
        }

        //Going South
        if (other.gameObject.name == "Collider South" && gamemodeScript.wrongAns == 4)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(0, 5);

            //calling coroutine
            StartCoroutine("SetLose");
            return;
        }
    }

    IEnumerator SetWin()
    {
        //Find gamemode script
        GameObject gamemode = GameObject.Find("Gamemode");
        Gamemode gamemodeScript = gamemode.GetComponent<Gamemode>();

        //Sets screen text
        gamemodeScript.winCondition = true;
        gamemodeScript.SetRoundEnd();

        yield return new WaitForSeconds(0.25f);

        //Enemy thinks of new wrong answer
        gamemodeScript.Think();
    }

    IEnumerator SetLose()
    {
        //Find gamemode script
        GameObject gamemode = GameObject.Find("Gamemode");
        Gamemode gamemodeScript = gamemode.GetComponent<Gamemode>();

        //Sets screen text
        gamemodeScript.winCondition = false;
        gamemodeScript.SetRoundEnd();

        yield return new WaitForSeconds(0.25f);

        //Enemy thinks of new wrong answer
        gamemodeScript.Think();
    }
}
