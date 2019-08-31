using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemode : MonoBehaviour
{
    public int wrongAns;
    public int previousAns = 1;
    public bool winCondition;
    public int score = 0;
    public int highScore;
    public int resetScore = 0;

    public float timeRemaining = 10;
    public float maxTime = 10;
    public bool playerLost = true;
    public float difficulty = 1f;
    public float difficultyIncrement = 0.25f;
    public int blockEastCount;
    public int blockNorthCount;
    public int blockWestCount;
    public int blockSouthCount;
    public int individualBlockMax;
    public int blockCount;
    public int altogetherBlockMax;
    public int turnCountEast;
    public int turnCountNorth;
    public int turnCountWest;
    public int turnCountSouth;
    public int turnCountMax;

    public bool playedOnceN;
    public bool playedOnceW;
    public bool playedOnceS;
    public bool playedOnceE;

    public bool allFour;
    public bool ranEN;
    public bool ranEW;
    public bool ranES;
    public bool ranNW;
    public bool ranNS;
    public bool ranWS;
    public bool ranE;
    public bool ranN;
    public bool ranW;
    public bool ranS;

    //count of amount of times the player went to a specific path
    public int playerEastCount;
    public int playerNorthCount;
    public int playerWestCount;
    public int playerSouthCount;

    public Image timeBar;


    public Image sprintBar;

    public float sprintTime;
    public float sprintMaxTime = 1;
    public float sprintHoldBack = 1;


    public Text scoreText;
    public Text mainText;
    public Text textOffCam;
    public Text deathText;

    public Text highScoreText;
    public GameObject speechBubble;

    //Reference to each path's timer images
    public GameObject eastTimer;
    public GameObject northTimer;
    public GameObject westTimer;
    public GameObject southTimer;

    //Reference to each path's timer texts
    public Text eastTimerText;
    public Text northTimerText;
    public Text westTimerText;
    public Text southTimerText;

    //Reference to hitboxes for pathways
    public GameObject eastBlockade;
    public GameObject northBlockade;
    public GameObject westBlockade;
    public GameObject southBlockade;

    //3 turn cooldown for East path
    public GameObject eastBlock1;
    public GameObject eastBlock2;
    public GameObject eastBlock3;

    //3 turn cooldown for East path
    public GameObject northBlock1;
    public GameObject northBlock2;
    public GameObject northBlock3;

    //3 turn cooldown for West path
    public GameObject westBlock1;
    public GameObject westBlock2;
    public GameObject westBlock3;

    //3 turn cooldown for South path
    public GameObject southBlock1;
    public GameObject southBlock2;
    public GameObject southBlock3;

    private GameObject player;

    void Start()
    {
        //Find gamemode script
        GameObject audioController = GameObject.Find("AudioControllerStart");
        AudioController audioControllerScript = audioController.GetComponent<AudioController>();

        audioControllerScript.playingMM = true;

        //Call functions to set all path blockades to off
        ResetEastPath();
        ResetNorthPath();
        ResetWestPath();
        ResetSouthPath();

        //Set score to 0
        score = 0;

        mainText.text = "";
        deathText.text = "Press Space to Start";

        highScoreText.text = "Highscore " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void Update()
    {
        //If the player presses space 
        if (Input.GetKeyDown("space") && playerLost)
        {
            player = GameObject.Find("Player");
            player.transform.position = new Vector2(-0.5f, 6f);

            //Find Audio script
            GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
            AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

            //Play correct path Audio
            audioControllerMainScript.startGame.Play();

            //Call function
            StartGame();
        }

        //If player is not lost and still has sprint bar, deplete it
        if (!playerLost && Input.GetKey(KeyCode.LeftShift) && sprintTime > 0)
        {
            //time remaining decreases over time
            sprintTime -= sprintHoldBack;

            sprintBar.fillAmount = sprintTime;

            Player playerScript = player.GetComponent<Player>();
            playerScript.playerSpeed = playerScript.sprintSpeed;
        }

        if (!playerLost && sprintTime <= 0)
        {
            Player playerScript = player.GetComponent<Player>();
            playerScript.playerSpeed = playerScript.walkSpeed;
        }

        //If player presses escape, quit the game
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        //If the player presses r, reset the highscore
        if (Input.GetKeyDown("r"))
        {
            PlayerPrefs.DeleteKey("HighScore");
            highScoreText.text = "Highscore 0";
        }

        //If the time is above 0, decrease it continuesly

        if (timeRemaining >= 0 && !playerLost)
        {
            //time remaining decreases over time
            timeRemaining -= Time.deltaTime * difficulty;

            timeBar.fillAmount = timeRemaining / 10;
        }

        //If timer gets to 0, play death SFX 
        // & trigger gameover function
        if (timeRemaining <= 0)
        {
            //call function
            GameOver();
        }

        if (!playerLost)
        {
            highScoreText.text = "".ToString();
        }
    }

    private void StartGame()
    {
        //Reset score
        score = 0;
        scoreText.text = score.ToString();

        //Find gamemode script
        GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
        AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

        //Play starting music
        audioControllerMainScript.playingMML = true;

        //Set death to false
        playerLost = false;

        //Reset player's powision to middle of area
        transform.position = new Vector2(0, 5);

        //Setting values to default starting value
        timeRemaining = maxTime;
        timeBar.fillAmount = 1;

        sprintTime = sprintMaxTime;

        //Set start text to nothing
        deathText.text = "";

        //Set speech bubble to visible
        speechBubble.SetActive(true);

        //Call functions
        Think();
        ResetTurnCount();

        //Set values to 0
        turnCountEast = 0;
        turnCountNorth = 0;
        turnCountWest = 0;
        turnCountSouth = 0;

        //Set blockades to false
        eastBlockade.SetActive(false);
        northBlockade.SetActive(false);
        westBlockade.SetActive(false);
        southBlockade.SetActive(false);
    }

    void GameOver()
    {
        //Turn speech bubble off
        speechBubble.SetActive(false);

        //Set Highscore
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = highScore.ToString();
        }

        highScoreText.text = "Highscore " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        playerLost = true;
        deathText.text = "Press space to restart";

        //Set values of blockades to 0
        blockEastCount = 0;
        blockNorthCount = 0;
        blockWestCount = 0;
        blockSouthCount = 0;

        blockCount = 0;

        //Reset difficulty
        difficulty = 1;

        //Reset all paths to default state
        ResetEastPath();
        ResetNorthPath();
        ResetWestPath();
        ResetSouthPath();

        //On death, make sure the player isnt performing any animations
        Animator playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetBool("GoingLeft", false);
        playerAnimator.SetBool("GoingUp", false);
        playerAnimator.SetBool("GoingDown", false);
        playerAnimator.SetBool("GoingRight", false);
        playerAnimator.SetBool("Walking", false);
    }

    public void Think()
    {
        //If no walls are up, proceed think process with all four options.
        if (!playerLost && turnCountEast == 0 && turnCountNorth == 0 && turnCountWest == 0 && turnCountSouth == 0)
        {
            allFour = true;
            ranEN = false;
            ranEW = false;
            ranES = false;
            ranNW = false;
            ranNS = false;
            ranWS = false;
            ranE = false;
            ranN = false;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }

        //South and West walls up
        if (!playerLost && turnCountSouth > 0 && turnCountWest > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = false;
            ranES = false;
            ranNW = false;
            ranNS = false;
            ranWS = true;
            ranE = false;
            ranN = false;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }
        
        //North and South walls up
        if (!playerLost && turnCountNorth > 0 && turnCountSouth > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = false;
            ranES = false;
            ranNW = false;
            ranNS = true;
            ranWS = false;
            ranE = false;
            ranN = false;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;  
        }

        //North and West walls up
        if (!playerLost && turnCountNorth > 0 && turnCountWest > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = false;
            ranES = false;
            ranNW = true;
            ranNS = false;
            ranWS = false;
            ranE = false;
            ranN = false;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }

        //East and west walls up
        if (!playerLost && turnCountEast > 0 && turnCountWest > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = true;
            ranES = false;
            ranNW = false;
            ranNS = false;
            ranWS = false;
            ranE = false;
            ranN = false;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }

        //East and north walls up
        if (!playerLost && turnCountEast > 0 && turnCountNorth > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = true;
            ranEW = false;
            ranES = false;
            ranNW = false;
            ranNS = false;
            ranWS = false;
            ranE = false;
            ranN = false;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }

        //East and south walls up
        if (!playerLost && turnCountEast > 0 && turnCountSouth > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = false;
            ranES = true;
            ranNW = false;
            ranNS = false;
            ranWS = false;
            ranE = false;
            ranN = false;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }

        //South wall is up
        if (!playerLost && turnCountSouth > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = false;
            ranES = false;
            ranNW = false;
            ranNS = false;
            ranWS = false;
            ranE = false;
            ranN = false;
            ranW = false;
            ranS = true;

            //Call Function
            RandomController();
            return;
        }

        //West wall is up
        if (!playerLost && turnCountWest > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = false;
            ranES = false;
            ranNW = false;
            ranNS = false;
            ranWS = false;
            ranE = false;
            ranN = false;
            ranW = true;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }

        //North wall is up
        if (!playerLost && turnCountNorth > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = false;
            ranES = false;
            ranNW = false;
            ranNS = false;
            ranWS = false;
            ranE = false;
            ranN = true;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }

        //East wall is up
        if (!playerLost && turnCountEast > 0)
        {
            //Pick from those specific choices
            allFour = false;
            ranEN = false;
            ranEW = false;
            ranES = false;
            ranNW = false;
            ranNS = false;
            ranWS = false;
            ranE = true;
            ranN = false;
            ranW = false;
            ranS = false;

            //Call Function
            RandomController();
            return;
        }
    }

    void RandomController()
    {
        if (allFour)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                List<int> liarThink = new List<int>(new int[] { 1, 2, 3, 4 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            //Call function
            StartCoroutine("Choose");
            return;

        }

        if (ranWS)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 1, 2 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranNS)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 1, 3 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranNW)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 1, 4 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranEW)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 2, 4 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranEN)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 3, 4 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranES)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 2, 3 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranS)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 1, 2, 3 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranW)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                previousAns = wrongAns;
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 1, 2, 4 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranN)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 1, 3, 4 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }

        if (ranE)
        {
            //Set previous answer to equal wrong answer
            previousAns = wrongAns;

            while (wrongAns == previousAns)
            {
                //Generate thought process without a path with wall up
                List<int> liarThink = new List<int>(new int[] { 2, 3, 4 });

                //Pick from those specific choices
                wrongAns = liarThink[Random.Range(0, liarThink.Count)];
            }

            StartCoroutine("Choose");
            return;
        }
    }

    IEnumerator Choose()
    {
        if (allFour)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
                textOffCam.text = "Go Left";
            }

            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
                textOffCam.text = "Go Up";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
                textOffCam.text = "Go Right";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
                textOffCam.text = "Go Down";
            }

            yield return new WaitForSeconds(0.25f);
            allFour = false;
            yield return 0;
        }

        if (ranWS)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
                textOffCam.text = "Go Left";
            }

            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
                textOffCam.text = "Go Up";
            }

            yield return new WaitForSeconds(0.25f);
            ranWS = false;
            yield return 0;
        }

        if (ranNS)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
                textOffCam.text = "Go Left";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
                textOffCam.text = "Go Right";
            }

            yield return new WaitForSeconds(0.25f);
            ranNS = false;
            yield return 0;
        }

        if (ranNW)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
                textOffCam.text = "Go Left";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
                textOffCam.text = "Go Down";
            }

            yield return new WaitForSeconds(0.25f);

            ranNW = false;
            yield return 0;
        }

        if (ranEW)
        {
            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
                textOffCam.text = "Go Up";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
                textOffCam.text = "Go Down";
            }

            yield return new WaitForSeconds(0.25f);
            ranEW = false;
            yield return 0;
        }

        if (ranEN)
        {
            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
                textOffCam.text = "Go Right";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
                textOffCam.text = "Go Down";
            }

            yield return new WaitForSeconds(0.25f);

            ranEN = false;
            yield return 0;
        }

        if (ranES)
        {
            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
                textOffCam.text = "Go Up";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
                textOffCam.text = "Go Right";
            }

            yield return new WaitForSeconds(0.25f);
            ranES = false;
            yield return 0;
        }

        if (ranS)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
                textOffCam.text = "Go Left";
            }

            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
                textOffCam.text = "Go Up";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
                textOffCam.text = "Go Right";
            }

            yield return new WaitForSeconds(0.25f);
            ranS = false;
            yield return 0;
        }

        if (ranW)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
                textOffCam.text = "Go Left";
            }

            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
                textOffCam.text = "Go Up";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
                textOffCam.text = "Go Down";
            }

            yield return new WaitForSeconds(0.25f);
            ranW = false;
            yield return 0;
        }

        if (ranN)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
                textOffCam.text = "Go Left";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
                textOffCam.text = "Go Right";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
                textOffCam.text = "Go Down";
            }

            yield return new WaitForSeconds(0.25f);

            ranN = false;
            yield return 0;
        }

        if (ranE)
        {
            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
                textOffCam.text = "Go Up";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
                textOffCam.text = "Go Right";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
                textOffCam.text = "Go Down";
            }

            yield return new WaitForSeconds(0.25f);
            ranE = false;
            yield return 0;
        }
    }

    public void SetRoundEnd()
    {
        //If the player chooses incorrectly
        if (!winCondition)
        {
            mainText.text = "Hahaha!".ToString();

            //Call function
            GameOver();
        }
        
        //If the player chooses correctly
        else
        {
            //Call function
            StartCoroutine("ScoreUp");
        }
    }

    //When the player chooses successfully
    IEnumerator ScoreUp()
    {
        //Set sprintbar to full
        sprintTime = sprintMaxTime;
        sprintBar.fillAmount = 1f;

        //Set timebar to full
        timeRemaining = maxTime;

        //Increase difficulty
        difficulty += .25f;

        yield return new WaitForSeconds(.1f);

        //Turns to wait until specific blockade goes down if it's active
        if (turnCountEast == turnCountMax && eastBlockade.activeSelf)
        {
            //Minus 1 block count due to one blockade despawning
            blockCount--;

            eastBlockade.SetActive(false);
            turnCountEast = 0;
            blockEastCount = 0;

            //Reset variable
            playedOnceE = false;

            //Find gamemode script
            GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
            AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

            //Play block over audio
            audioControllerMainScript.blockOver.Play();

            //Call function to reset East Path blockades
            ResetEastPath();

            /*if (northBlockade.activeSelf)
            {
                northBlockade.SetActive(false);
                turnCountNorth = 0;
                blockNorthCount = 0;

                if (westBlockade.activeSelf)
                {
                    westBlockade.SetActive(false);
                    turnCountWest = 0;
                    blockWestCount = 0;

                    if (southBlockade.activeSelf)
                    {
                        southBlockade.SetActive(false);
                        turnCountSouth = 0;
                        blockSouthCount = 0;
                    }
                }
            } */
        }

        if (turnCountNorth == turnCountMax && northBlockade.activeSelf)
        {
            //Minus 1 block count due to one blockade despawning
            blockCount--;

            northBlockade.SetActive(false);
            turnCountNorth = 0;
            blockNorthCount = 0;

            //Reset variable
            playedOnceN = false;

            //Find gamemode script
            GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
            AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

            //Play block over audio

            audioControllerMainScript.blockOver.Play();
            //Call function to reset North Path blockades
            ResetNorthPath();

            /* if (eastBlockade.activeSelf)
            {
                eastBlockade.SetActive(false);
                turnCountEast = 0;
                blockEastCount = 0;

                if (westBlockade.activeSelf)
                {
                    westBlockade.SetActive(false);
                    turnCountWest = 0;
                    blockWestCount = 0;

                    if (southBlockade.activeSelf)
                    {
                        southBlockade.SetActive(false);
                        turnCountSouth = 0;
                        blockSouthCount = 0;
                    }
                }
            } */
        }

        if (turnCountWest == turnCountMax && westBlockade.activeSelf)
        {
            //Minus 1 block count due to one blockade despawning
            blockCount--;

            westBlockade.SetActive(false);
            turnCountWest = 0;
            blockWestCount = 0;

            //Reset variable
            playedOnceW = false;

            //Find gamemode script
            GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
            AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

            //Play block over audio

            audioControllerMainScript.blockOver.Play();
            //Call function to reset West Path blockades
            ResetWestPath();

            /* if (northBlockade.activeSelf)
            {
                northBlockade.SetActive(false);
                turnCountNorth = 0;
                blockNorthCount = 0;

                if (eastBlockade.activeSelf)
                {
                    eastBlockade.SetActive(false);
                    turnCountEast = 0;
                    blockEastCount = 0;

                    if (southBlockade.activeSelf)
                    {
                        southBlockade.SetActive(false);
                        turnCountSouth = 0;
                        blockSouthCount = 0;
                    }
                }
            } */
        }

        if (turnCountSouth == turnCountMax && southBlockade.activeSelf)
        {
            //Minus 1 block count due to one blockade despawning
            blockCount--;

            southBlockade.SetActive(false);
            turnCountSouth = 0;
            blockSouthCount = 0;

            //Reset variable
            playedOnceS = false;

            //Find gamemode script
            GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
            AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

            //Play block over audio
            audioControllerMainScript.blockOver.Play();

            //Call function to reset South Path blockades
            ResetSouthPath();

             /* if (northBlockade.activeSelf)
            {
                northBlockade.SetActive(false);
                turnCountNorth = 0;
                blockNorthCount = 0;

                if (westBlockade.activeSelf)
                {
                    westBlockade.SetActive(false);
                    turnCountWest = 0;
                    blockWestCount = 0;

                    if (eastBlockade.activeSelf)
                    {
                        eastBlockade.SetActive(false);
                        turnCountEast = 0;
                        blockEastCount = 0;
                    }
                }
            } */
        }
    }

    //Reset east path's blockades
    void ResetEastPath()
    {
        //Set east path's blocks to invisible
        eastBlock1.SetActive(false);
        eastBlock2.SetActive(false);
        eastBlock3.SetActive(false);

        eastTimer.SetActive(false);
    }
    //Reset North path's blockades
    void ResetNorthPath()
    {
        //Set north path's blocks to invisible
        northBlock1.SetActive(false);
        northBlock2.SetActive(false);
        northBlock3.SetActive(false);

        northTimer.SetActive(false);
    }
    //Reset West path's blockades
    void ResetWestPath()
    {
        //Set west path's blocks to invisible
        westBlock1.SetActive(false);
        westBlock2.SetActive(false);
        westBlock3.SetActive(false);

        westTimer.SetActive(false);
    }
    //Reset South path's blockades
    void ResetSouthPath()
    {
        //Set south path's blocks to invisible
        southBlock1.SetActive(false);
        southBlock2.SetActive(false);
        southBlock3.SetActive(false);

        southTimer.SetActive(false);
    }

    //Checks to see if a path has hit max entries
    public void BuildEastBlock()
    {
        //Increase Block East count
        blockEastCount++;

        //Call function, the time is so the blockade can potentially spawn first
        //Then it would add 1 to it after
        Invoke("IncreaseTurnCount", 0.05f);

        if (blockEastCount >= individualBlockMax) //If East is less then max)
        {
            if (blockCount < altogetherBlockMax)
            {
                //Increase block count
                blockCount++;

                //Spawn the block
                eastBlockade.SetActive(true);
            }
        }

        //Call function
        ScoreLimitE();
    }

    public void BuildNorthBlock()
    {
        //Increase Block north count
        blockNorthCount++;

        //Call function, the time is so the blockade can potentially spawn first
        //Then it would add 1 to it after
        Invoke("IncreaseTurnCount", 0.05f);

        if (blockNorthCount >= individualBlockMax) //If East is less then max)
        {
            if (blockCount < altogetherBlockMax)
            {
                //Increase block count
                blockCount++;

                //Spawn the block
                northBlockade.SetActive(true);
            }
        }

        //Call function
        ScoreLimitN();
    }

    public void BuildWestBlock()
    {
        //Increase Block west count
        blockWestCount++;

        //Call function, the time is so the blockade can potentially spawn first
        //Then it would add 1 to it after
        Invoke("IncreaseTurnCount", 0.05f);

        if (blockWestCount >= individualBlockMax) //If East is less then max)
        {
            if (blockCount < altogetherBlockMax)
            {
                //Increase block count
                blockCount++;

                //Spawn the block
                westBlockade.SetActive(true);
            }
        }

        //Call function
        ScoreLimitW();
    }

    public void BuildSouthBlock()
    {
        //Increase Block south count
        blockSouthCount++;

        //Call function, the time is so the blockade can potentially spawn first
        //Then it would add 1 to it after
        Invoke("IncreaseTurnCount", 0.05f);

        if (blockSouthCount >= individualBlockMax) //If East is less then max)
        {
            if (blockCount < altogetherBlockMax)
            {
                //Increase block count
                blockCount++;

                //Spawn the block
                southBlockade.SetActive(true);
            }
        }

        //Call function
        ScoreLimitS();
    }

    void IncreaseTurnCount()
    {
        //If east blockade is active, increase turncounteast count by 1
        if (eastBlockade.activeSelf)
        {
            turnCountEast++;

            //Call function to start blockade timer 
            BlockadeTimer();
        }

        //If north blockade is active, increase turncounteast count by 1
        if (northBlockade.activeSelf)
        {
            turnCountNorth++;

            //Call function to start blockade timer 
            BlockadeTimer();
        }

        //If west blockade is active, increase turncounteast count by 1
        if (westBlockade.activeSelf)
        {
            turnCountWest++;

            //Call function to start blockade timer 
            BlockadeTimer();
        }

        //If south blockade is active, increase turncounteast count by 1
        if (southBlockade.activeSelf)
        {
            turnCountSouth++;

            //Call function to start blockade timer 
            BlockadeTimer();
        }

        //Disable the images for turns left for East path
        if (blockEastCount == 1)
        {
            eastBlock1.SetActive(true);
            eastBlock2.SetActive(false);
            eastBlock3.SetActive(false);
        }

        if (blockEastCount == 2)
        {
            eastBlock2.SetActive(true);
            eastBlock1.SetActive(false);
            eastBlock3.SetActive(false);
        }

        if (blockEastCount == 3)
        {
            if (playedOnceE == false)
            {
                playedOnceE = true;

                //Find gamemode script
                GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
                AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

                //Play audio for block up
                audioControllerMainScript.blockSpawn.Play();
            }

            eastBlock3.SetActive(true);
            eastBlock1.SetActive(false);
            eastBlock2.SetActive(false);
        }

        //Disable the images for turns left for North path
        if (blockNorthCount == 1)
        {
            northBlock1.SetActive(true);
            northBlock2.SetActive(false);
            northBlock3.SetActive(false);
        }

        if (blockNorthCount == 2)
        {
            northBlock2.SetActive(true);
            northBlock1.SetActive(false);
            northBlock3.SetActive(false);
        }

        if (blockNorthCount == 3)
        {
            if (playedOnceN == false)
            {
                playedOnceN = true;

                //Find gamemode script
                GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
                AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

                //Play audio for block up
                audioControllerMainScript.blockSpawn.Play();
            }

            northBlock3.SetActive(true);
            northBlock1.SetActive(false);
            northBlock2.SetActive(false);
        }

        //Disable the images for turns left for West path
        if (blockWestCount == 1)
        {
            westBlock1.SetActive(true);
            westBlock2.SetActive(false);
            westBlock3.SetActive(false);
        }

        if (blockWestCount == 2)
        {
            westBlock2.SetActive(true);
            westBlock1.SetActive(false);
            westBlock3.SetActive(false);
        }

        if (blockWestCount == 3)
        {
            if (playedOnceW == false)
            {
                playedOnceW = true;

                //Find gamemode script
                GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
                AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

                //Play audio for block up
                audioControllerMainScript.blockSpawn.Play();
            }

            westBlock3.SetActive(true);
            westBlock1.SetActive(false);
            westBlock2.SetActive(false);
        }

        //Disable the images for turns left for South path
        if (blockSouthCount == 1)
        {
            southBlock1.SetActive(true);
            southBlock2.SetActive(false);
            southBlock3.SetActive(false);
        }

        if (blockSouthCount == 2)
        {
            southBlock2.SetActive(true);
            southBlock1.SetActive(false);
            southBlock3.SetActive(false);
        }

        if (blockSouthCount == 3)
        {
            if (playedOnceS == false)
            {
                playedOnceS = true;

                //Find gamemode script
                GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
                AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

                //Play audio for block up
                audioControllerMainScript.blockSpawn.Play();
            }

            southBlock3.SetActive(true);
            southBlock1.SetActive(false);
            southBlock2.SetActive(false);
        }

        //Call function
        //ResetTurnCount();
    }

    void BlockadeTimer()
    {
        //Trigger timer image and text according to the count
        if (turnCountEast == 1)
        {
            eastTimer.SetActive(true);
            eastTimerText.text = "3".ToString();
        }

        if (turnCountEast == 2)
        {
            eastTimerText.text = "2".ToString();
        }

        if (turnCountEast == 3)
        {
            eastTimerText.text = "1".ToString();
        }

        if (turnCountEast == 0)
        {
            eastTimer.SetActive(false);
        }

        //Trigger timer image and text according to the count
        if (turnCountNorth == 1)
        {
            northTimer.SetActive(true);
            northTimerText.text = "3".ToString();
        }

        if (turnCountNorth == 2)
        {
            northTimerText.text = "2".ToString();
        }

        if (turnCountNorth == 3)
        {
            northTimerText.text = "1".ToString();
        }

        if (turnCountNorth == 0)
        {
            northTimer.SetActive(false);
        }

        //Trigger timer image and text according to the count
        if (turnCountWest == 1)
        {
            westTimer.SetActive(true);
            westTimerText.text = "3".ToString();
        }

        if (turnCountWest == 2)
        {
            westTimerText.text = "2".ToString();
        }

        if (turnCountWest == 3)
        {
            westTimerText.text = "1".ToString();
        }

        if (turnCountWest == 0)
        {
            westTimer.SetActive(false);
        }

        //Trigger timer image and text according to the count
        if (turnCountSouth == 1)
        {
            southTimer.SetActive(true);
            southTimerText.text = "3".ToString();
        }

        if (turnCountSouth == 2)
        {
            southTimerText.text = "2".ToString();
        }

        if (turnCountSouth == 3)
        {
            southTimerText.text = "1".ToString();
        }

        if (turnCountSouth == 0)
        {
            southTimer.SetActive(false);
        }
    }

    //If the turn count for a side is 0, turn all their images off
    void ResetTurnCount()
    {
        if (blockEastCount == 0)
        {
            eastBlock1.SetActive(false);
            eastBlock2.SetActive(false);
            eastBlock3.SetActive(false);
        }

        if (blockNorthCount == 0)
        {
            northBlock1.SetActive(false);
            northBlock2.SetActive(false);
            northBlock3.SetActive(false);
        }

        if (blockWestCount == 0)
        {
            westBlock1.SetActive(false);
            westBlock2.SetActive(false);
            westBlock3.SetActive(false);
        }
  
        if (blockSouthCount == 0)
        {
            southBlock1.SetActive(false);
            southBlock2.SetActive(false);
            southBlock3.SetActive(false);
        }
    }

    public void ScoreLimitE()
    {
        playerEastCount++;
        playerNorthCount = 0;
        playerWestCount = 0;
        playerSouthCount = 0;

        if (playerEastCount == 3)
        {
            score += 3;
            scoreText.text = score.ToString();
            return;
        }

        if (playerEastCount == 2)
        {
            score += 5;
            scoreText.text = score.ToString();
            return;
        }

        if (playerEastCount == 1)
        {
            score += 10;
            scoreText.text = score.ToString();
            return;
        }
    }

    public void ScoreLimitN()
    {
        playerNorthCount++;
        playerEastCount = 0;
        playerWestCount = 0;
        playerSouthCount = 0;

        if (playerNorthCount == 3)
        {
            score += 3;
            scoreText.text = score.ToString();
            return;
        }

        if (playerNorthCount == 2)
        {
            score += 5;
            scoreText.text = score.ToString();
            return;
        }

        if (playerNorthCount == 1)
        {
            score += 10;
            scoreText.text = score.ToString();
            return;
        }
    }

    public void ScoreLimitW()
    {
        playerWestCount++;
        playerEastCount = 0;
        playerNorthCount = 0;
        playerSouthCount = 0;

        if (playerWestCount == 3)
        {
            score += 3;
            scoreText.text = score.ToString();
            return;
        }

        if (playerWestCount == 2)
        {
            score += 5;
            scoreText.text = score.ToString();
            return;
        }

        if (playerWestCount == 1)
        {
            score += 10;
            scoreText.text = score.ToString();
            return;
        }
    }

    public void ScoreLimitS()
    {
        playerSouthCount++;
        playerNorthCount = 0;
        playerWestCount = 0;
        playerEastCount = 0;

        if (playerSouthCount == 3)
        {
            score += 3;
            scoreText.text = score.ToString();
            return;
        }

        if (playerSouthCount == 2)
        {
            score += 5;
            scoreText.text = score.ToString();
            return;
        }

        if (playerSouthCount == 1)
        {
            score += 10;
            scoreText.text = score.ToString();
            return;
        }
    }
}

