using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    private Vector3 playerPos;
    private Rigidbody2D rb;

    public float playerSpeed = .2f;

    public Animator animator;

    private void Start()
    {
        //Find components
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Find gamemode script
        GameObject gamemode = GameObject.Find("Gamemode");
        Gamemode gamemodeScript = gamemode.GetComponent<Gamemode>();

        //If the game is NOT over
        if (!gamemodeScript.playerLost)
        {
            //When walking left, play left animation
            if (!Input.GetKey("a"))
            {
                animator.SetBool("GoingLeft", false);
            }

            if (Input.GetKey("a"))
            {
                animator.SetBool("GoingLeft", true);
            }

            //When walking left, play right animation
            if (!Input.GetKey("d"))
            {
                animator.SetBool("GoingRight", false);
            }

            if (Input.GetKey("d"))
            {
                animator.SetBool("GoingRight", true);
            }

            if (Input.GetKey("w"))
            {
                animator.SetBool("GoingUp", true);
            }

            if (!Input.GetKey("w"))
            {
                animator.SetBool("GoingUp", false);
            }

            if (Input.GetKey("s"))
            {
                animator.SetBool("GoingDown", true);
            }

            if (!Input.GetKey("s"))
            {
                animator.SetBool("GoingDown", false);
            }

            //Player Movement
            playerPos = Vector3.zero;
            playerPos.x = Input.GetAxisRaw("Horizontal");
            playerPos.y = Input.GetAxisRaw("Vertical");

            if (playerPos != Vector3.zero)
            {
                //The functionality behind the movement
                rb.MovePosition(transform.position + playerPos * playerSpeed * Time.deltaTime);

                //Trigger walking animation
                animator.SetBool("Walking", true);
            }

            else
            {
                //Trigger idle animation
                animator.SetBool("Walking", false);
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
            transform.position = new Vector2(-0.5f, 6f);

            //calling coroutine / functions
            StartCoroutine("SetWin");
            gamemodeScript.BuildEastBlock();
            return;
        }

        //Going North
        if (other.gameObject.name == "Collider North" && gamemodeScript.wrongAns != 2)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(-0.5f, 6f);

            //calling coroutine / functions
            StartCoroutine("SetWin");
            gamemodeScript.BuildNorthBlock();
            return;
        }

        //Going West
        if (other.gameObject.name == "Collider West" && gamemodeScript.wrongAns != 3)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(-0.5f, 6f);

            //calling coroutine / functions
            StartCoroutine("SetWin");
            gamemodeScript.BuildWestBlock();
            return;
        }

        //Going South
        if (other.gameObject.name == "Collider South" && gamemodeScript.wrongAns != 4)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(-0.5f, 6f);

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
            transform.position = new Vector2(-0.5f, 6f);

            //calling coroutine
            SetLose();
            return;
        }

        //Going North
        if (other.gameObject.name == "Collider North" && gamemodeScript.wrongAns == 2)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(-0.5f, 6f);

            //calling coroutine
            SetLose();
            return;

        }

        //Going West
        if (other.gameObject.name == "Collider West" && gamemodeScript.wrongAns == 3)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(-0.5f, 6f);

            //calling coroutine
            SetLose();
            return;
        }

        //Going South
        if (other.gameObject.name == "Collider South" && gamemodeScript.wrongAns == 4)
        {
            //Reset player's powision to middle of area
            transform.position = new Vector2(-0.5f, 6f);

            //calling coroutine
            SetLose();
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

        //Find Audio script
        GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
        AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

        //Play correct path Audio
        audioControllerMainScript.pathCorrect.pitch = Random.Range(.9f,1.1f);
        audioControllerMainScript.pathCorrect.Play();

        yield return new WaitForSeconds(0.1f);

        //Enemy thinks of new wrong answer
        gamemodeScript.Think();
    }

    void SetLose()
    {
        //Find gamemode script
        GameObject gamemode = GameObject.Find("Gamemode");
        Gamemode gamemodeScript = gamemode.GetComponent<Gamemode>();

        //Sets screen text
        gamemodeScript.winCondition = false;
        gamemodeScript.SetRoundEnd();

        //Find Audio script
        GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
        AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

        //Play correct path Audio
        audioControllerMainScript.pathWrong.pitch = Random.Range(.9f, 1.1f);
        audioControllerMainScript.pathWrong.Play();
    }
}
