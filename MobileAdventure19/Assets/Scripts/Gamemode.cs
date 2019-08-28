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

    //count of amount of times the player went to a specific path
    public int playerEastCount;
    public int playerNorthCount;
    public int playerWestCount;
    public int playerSouthCount;

    public Image timeBar;
    public Text scoreText;
    public Text mainText;

    public Text highScoreText;

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
        //Set score to 0
        score = 0;

        mainText.text = "Press space to restart";

        highScoreText.text = "Highscore " + PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    private void Update()
    {
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
        //Setting values to default starting value
        timeRemaining = maxTime;
        timeBar.fillAmount = 1;

        //Set death to false
        playerLost = false;

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
        //Set direction
        wrongAns = Random.Range(1, 5);

        //Set text to display direction
        if (!playerLost)
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
        }
    }

    public void SetRoundEnd()
    {
        //If the player chooses incorrectly
        if (!winCondition)
        {
            mainText.text = "Wrong!".ToString();

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

        yield return new WaitForSeconds(.5f);

        //Turns to wait until specific blockade goes down if it's active
        if (turnCountEast == turnCountMax && eastBlockade.activeSelf)
        {
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
        mainText.text = "Press space to restart";

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
        //Increase Block count
        blockEastCount++;

        //Call function
        IncreaseTurnCount();

        if (blockEastCount >= individualBlockMax) //If East is less then max)
        {
            eastBlockade.SetActive(true);

            //If block count goes over max, cap it at the max again
            if (blockCount > altogetherBlockMax)
            {
                blockCount = altogetherBlockMax;
            }
        }

        //Call function
        ScoreLimitE();
    }

    public void BuildNorthBlock()
    {
        //Increase Block count
        blockNorthCount++;

        //Call function
        IncreaseTurnCount();

        if (blockNorthCount >= individualBlockMax) //If East is less then max)
        {
            northBlockade.SetActive(true);

            //If block count goes over max, cap it at the max again
            if (blockCount > altogetherBlockMax)
            {
                blockCount = altogetherBlockMax;
            }
        }

        //Call function
        ScoreLimitN();
    }

    public void BuildWestBlock()
    {
        //Increase Block count
        blockWestCount++;

        //Call function
        IncreaseTurnCount();

        if (blockWestCount >= individualBlockMax)
        {
            westBlockade.SetActive(true);

            //If block count goes over max, cap it at the max again
            if (blockCount > altogetherBlockMax)
            {
                blockCount = altogetherBlockMax;
            }
        }

        //Call function
        ScoreLimitW();
    }

    public void BuildSouthBlock()
    {
        //Increase Block count
        blockSouthCount++;

        //Call function
        IncreaseTurnCount();

        if (blockSouthCount >= individualBlockMax)
        {
            southBlockade.SetActive(true);
            //If block count goes over max, cap it at the max again
            if (blockCount > altogetherBlockMax)
            {
                blockCount = altogetherBlockMax;
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

    void ResetTurnCount()
    {
        //Enable images if turncount is 0 for east path
        if (turnCountEast == 0)
        {
            thirdLifeE.SetActive(true);
            secondLifeE.SetActive(true);
            firstLifeE.SetActive(true);
        }

        if (turnCountNorth == 0)
        {
            thirdLifeN.SetActive(true);
            secondLifeN.SetActive(true);
            firstLifeN.SetActive(true);
        }

        if (turnCountWest == 0)
        {
            thirdLifeW.SetActive(true);
            secondLifeW.SetActive(true);
            firstLifeW.SetActive(true);
        }

        if (turnCountSouth == 0)
        {
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

