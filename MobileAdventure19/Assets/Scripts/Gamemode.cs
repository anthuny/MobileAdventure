using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemode : MonoBehaviour
{
    public int wrongAns;
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
    public Text scoreText;
    public Text mainText;
    public Text deathText;

    public Text highScoreText;
    public GameObject speechBubble;

    //3 turn cooldown for east path
    public GameObject firstLifeE;
    public GameObject secondLifeE;
    public GameObject thirdLifeE;
    public GameObject fourthLifeE;

    //3 turn cooldown for north path
    public GameObject firstLifeN;
    public GameObject secondLifeN;
    public GameObject thirdLifeN;
    public GameObject fourthLifeN;

    //3 turn cooldown for west path
    public GameObject firstLifeW;
    public GameObject secondLifeW;
    public GameObject thirdLifeW;
    public GameObject fourthLifeW;

    //3 turn cooldown for south path
    public GameObject firstLifeS;
    public GameObject secondLifeS;
    public GameObject thirdLifeS;
    public GameObject fourthLifeS;

    public GameObject eastBlockade;
    public GameObject northBlockade;
    public GameObject westBlockade;
    public GameObject southBlockade;

    void Start()
    {
        //Set speech bubble to invisible
        speechBubble.SetActive(false);

        //Set score to 0
        score = 0;

        mainText.text = "";
        deathText.text = "Press Space to Start";

        highScoreText.text = "Highscore " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void Update()
    {
        //if (Input.GetKeyDown("t"))
        { 


        }
        //If the player presses space 
        if (Input.GetKeyDown("space") && playerLost)
        {
            //Call function
            StartGame();
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

        if (timeRemaining <= 0)
        {
            GameOver();
        }

        if (!playerLost)
        {
            highScoreText.text = "".ToString();
        }
    }

    private void StartGame()
    {
        //Set death to false
        playerLost = false;

        //Reset player's powision to middle of area
        transform.position = new Vector2(0, 5);

        //Setting values to default starting value
        timeRemaining = maxTime;
        timeBar.fillAmount = 1;

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
            List<int> liarThink = new List<int>(new int[] { 1, 2, 3, 4 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranWS)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 1, 2 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranNS)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 1, 3 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranNW)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 1, 4 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranEW)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 2, 4 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranEN)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 3, 4 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranES)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 2, 3 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranS)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 1, 2, 3 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];
        
            Choose();
            return;
        }

        if (ranW)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 1, 2, 4 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranN)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 1, 3, 4 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }

        if (ranE)
        {
            //Generate thought process without a path with wall up
            List<int> liarThink = new List<int>(new int[] { 2, 3, 4 });

            //Pick from those specific choices
            wrongAns = liarThink[Random.Range(0, liarThink.Count)];

            Choose();
            return;
        }
    }

    void Choose()
    {
        if (allFour)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
            }

            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
            }

            allFour = false;
            return;
        }

        if (ranWS)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
            }

            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
            }

            ranWS = false;
            return;
        }

        if (ranNS)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
            }

            ranNS = false;
            return;
        }

        if (ranNW)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
            }

            ranNW = false;
            return;
        }

        if (ranEW)
        {
            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
            }

            ranEW = false;
            return;
        }

        if (ranEN)
        {
            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
            }

            ranEN = false;
            return;
        }

        if (ranES)
        {
            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
            }

            ranES = false;
            return;
        }

        if (ranS)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
            }

            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
            }

            return;
        }

        if (ranW)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
            }

            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
            }

            ranS = false;
            return;
        }

        if (ranN)
        {
            if (wrongAns == 1)
            {
                mainText.text = "Go Left";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
            }

            ranN = false;
            return;
        }

        if (ranE)
        {
            if (wrongAns == 2)
            {
                mainText.text = "Go Up";
            }

            if (wrongAns == 3)
            {
                mainText.text = "Go Right";
            }

            if (wrongAns == 4)
            {
                mainText.text = "Go Down";
            }

            ranE = false;
            return;
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

            if (northBlockade.activeSelf)
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
            }
        }

        if (turnCountNorth == turnCountMax && northBlockade.activeSelf)
        {
            //Minus 1 block count due to one blockade despawning
            blockCount--;

            northBlockade.SetActive(false);
            turnCountNorth = 0;
            blockNorthCount = 0;

            if (eastBlockade.activeSelf)
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
            }
        }

        if (turnCountWest == turnCountMax && westBlockade.activeSelf)
        {
            //Minus 1 block count due to one blockade despawning
            blockCount--;

            westBlockade.SetActive(false);
            turnCountWest = 0;
            blockWestCount = 0;
            if (northBlockade.activeSelf)
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
            }
        }

        if (turnCountSouth == turnCountMax && southBlockade.activeSelf)
        {
            //Minus 1 block count due to one blockade despawning
            blockCount--;

            southBlockade.SetActive(false);
            turnCountSouth = 0;
            blockSouthCount = 0;
            if (northBlockade.activeSelf)
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
            }
        }
    }

    void GameOver()
    {
        //Set Highscore
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = highScore.ToString();
        }

        highScoreText.text = "Highscore " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        //Reset Score
        score = 0;
        scoreText.text = score.ToString();

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
    }

    //Checks to see if a path has hit max entries
    public void BuildEastBlock()
    {
        //Increase Block East count
        blockEastCount++;

        //Call function
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

        //Call function
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

        //Call function
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

        //Call function
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
        }

        //If north blockade is active, increase turncounteast count by 1
        if (northBlockade.activeSelf)
        {
            turnCountNorth++;
        }

        //If west blockade is active, increase turncounteast count by 1
        if (westBlockade.activeSelf)
        {
            turnCountWest++;
        }

        //If south blockade is active, increase turncounteast count by 1
        if (southBlockade.activeSelf)
        {
            turnCountSouth++;
        }

        //Enable images if turncount is 0 for west path
        if (turnCountWest == 0)
        {
            fourthLifeW.SetActive(true);
            thirdLifeW.SetActive(true);
            secondLifeW.SetActive(true);
            firstLifeW.SetActive(true);
        }

        //Enable images if turncount is 0 for east path
        if (turnCountEast == 0)
        {
            fourthLifeE.SetActive(true);
            thirdLifeE.SetActive(true);
            secondLifeE.SetActive(true);
            firstLifeE.SetActive(true);
        }

        //Enable images if turncount is 0 for south path
        if (turnCountSouth == 0)
        {
            fourthLifeS.SetActive(true);
            thirdLifeS.SetActive(true);
            secondLifeS.SetActive(true);
            firstLifeS.SetActive(true);
        }

        //Enable images if turncount is 0 for north path
        if (turnCountNorth == 0)
        {
            fourthLifeN.SetActive(true);
            thirdLifeN.SetActive(true);
            secondLifeN.SetActive(true);
            firstLifeN.SetActive(true);
        }


        //Disable the images for turns left for East path
        if (turnCountEast == 1)
        {
            fourthLifeE.SetActive(false);
        }

        if (turnCountEast == 2)
        {
            thirdLifeE.SetActive(false);
        }

        if (turnCountEast == 3)
        {
            secondLifeE.SetActive(false);
        }

        if (turnCountEast == 4)
        {
            firstLifeE.SetActive(false);
        }

        //Disable the images for turns left for North path
        if (turnCountNorth == 1)
        {
            fourthLifeN.SetActive(false);
        }

        if (turnCountNorth == 2)
        {
            thirdLifeN.SetActive(false);
        }

        if (turnCountNorth == 3)
        {
            secondLifeN.SetActive(false);
        }

        if (turnCountNorth == 4)
        {
            firstLifeN.SetActive(false);
        }

        //Disable the images for turns left for West path
        if (turnCountWest == 1)
        {
            fourthLifeW.SetActive(false);
        }

        if (turnCountWest == 2)
        {
            thirdLifeW.SetActive(false);
        }

        if (turnCountWest == 3)
        {
            secondLifeW.SetActive(false);
        }

        if (turnCountWest == 4)
        {
            firstLifeW.SetActive(false);
        }

        //Disable the images for turns left for South path
        if (turnCountSouth == 1)
        {
            fourthLifeS.SetActive(false);
        }

        if (turnCountSouth == 2)
        {
            thirdLifeS.SetActive(false);
        }

        if (turnCountSouth == 3)
        {
            secondLifeS.SetActive(false);
        }

        if (turnCountSouth == 4)
        {
            firstLifeS.SetActive(false);

        }

        //Call function
        ResetTurnCount();
    }

    //If the turn count for a side is 0, turn all their images on
    void ResetTurnCount()
    {
        if (turnCountEast == 0)
        {
            fourthLifeE.SetActive(true);
            thirdLifeE.SetActive(true);
            secondLifeE.SetActive(true);
            firstLifeE.SetActive(true);
        }

        if (turnCountNorth == 0)
        {
            fourthLifeN.SetActive(true);
            thirdLifeN.SetActive(true);
            secondLifeN.SetActive(true);
            firstLifeN.SetActive(true);
        }

        if (turnCountWest == 0)
        {
            fourthLifeW.SetActive(true);
            thirdLifeW.SetActive(true);
            secondLifeW.SetActive(true);
            firstLifeW.SetActive(true);
        }
  
        if (turnCountSouth == 0)
        {
            fourthLifeS.SetActive(true);
            thirdLifeS.SetActive(true);
            secondLifeS.SetActive(true);
            firstLifeS.SetActive(true);
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

