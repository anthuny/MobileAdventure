using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemode : MonoBehaviour
{
    public int wrongAns;
    public bool winCondition;
    public int score = 0;
    public float timeRemaining = 10;
    public float maxTime = 10;
    public bool playerLost = false;
    public float difficulty = 1f;
    public float difficultyIncrement = 0.25f;
    public int blockEastCount;
    public int blockNorthCount;
    public int blockWestCount;
    public int blockSouthCount;
    public int individualBlockMax;
    public int blockCount;
    public int altogetherBlockMax;
    public int turnCount;
    public int turnCountMax;

    public Image timeBar;
    public Text scoreText;
    public Text mainText;

    public GameObject firstLife;
    public GameObject secondLife;
    public GameObject thirdLife;

    public GameObject eastBlockade;
    public GameObject northBlockade;
    public GameObject westBlockade;
    public GameObject southBlockade;


    // Start is called before the first frame update
    void Start()
    {
        //Set score to 0
        score = 0;

        //Call function
        StartGame();
    }

    public void StartGame()
    {
        //Setting values to default starting value
        timeRemaining = maxTime;
        timeBar.fillAmount = 1;
        Think();
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
            ScoreUp();
        }
    }

    //When the player chooses successfully
    void ScoreUp()
    {
        //Increase the score
        score++;
        scoreText.text = score.ToString();

        //Set timebar to full
        timeRemaining = maxTime;

        //Increase difficulty
        difficulty += .25f;

        //Turns to wait until specific blockade goes down if it's active
        if (turnCount >= turnCountMax && eastBlockade.activeSelf)
        {
            eastBlockade.SetActive(false);
            turnCount = 0;
            blockEastCount = 0;

            if (northBlockade.activeSelf)
            {
                northBlockade.SetActive(false);
                turnCount = 0;
                blockNorthCount = 0;

                if (westBlockade.activeSelf)
                {
                    westBlockade.SetActive(false);
                    turnCount = 0;
                    blockWestCount = 0;

                    if (southBlockade.activeSelf)
                    {
                        southBlockade.SetActive(false);
                        turnCount = 0;
                        blockSouthCount = 0;
                    }
                }
            }
        }

        if (turnCount >= turnCountMax && northBlockade.activeSelf)
        {
            northBlockade.SetActive(false);
            turnCount = 0;
            blockNorthCount = 0;

            if (eastBlockade.activeSelf)
            {
                eastBlockade.SetActive(false);
                turnCount = 0;
                blockEastCount = 0;

                if (westBlockade.activeSelf)
                {
                    westBlockade.SetActive(false);
                    turnCount = 0;
                    blockWestCount = 0;

                    if (southBlockade.activeSelf)
                    {
                        southBlockade.SetActive(false);
                        turnCount = 0;
                        blockSouthCount = 0;
                    }
                }
            }
        }

        if (turnCount >= turnCountMax && westBlockade.activeSelf)
        {
            westBlockade.SetActive(false);
            turnCount = 0;
            blockWestCount = 0;
            if (northBlockade.activeSelf)
            {
                northBlockade.SetActive(false);
                turnCount = 0;
                blockNorthCount = 0;

                if (eastBlockade.activeSelf)
                {
                    eastBlockade.SetActive(false);
                    turnCount = 0;
                    blockEastCount = 0;

                    if (southBlockade.activeSelf)
                    {
                        southBlockade.SetActive(false);
                        turnCount = 0;
                        blockSouthCount = 0;
                    }
                }
            }
        }

        if (turnCount >= turnCountMax && southBlockade.activeSelf)
        {
            southBlockade.SetActive(false);
            turnCount = 0;
            blockSouthCount = 0;
            if (northBlockade.activeSelf)
            {
                northBlockade.SetActive(false);
                turnCount = 0;
                blockNorthCount = 0;

                if (westBlockade.activeSelf)
                {
                    westBlockade.SetActive(false);
                    turnCount = 0;
                    blockWestCount = 0;

                    if (eastBlockade.activeSelf)
                    {
                        eastBlockade.SetActive(false);
                        turnCount = 0;
                        blockEastCount = 0;
                    }
                }
            }
        }
    }

    void GameOver()
    {
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
    }

    private void Update()
    {
        //If the time remaining is above 0
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
    }

    void IncreaseTurnCount()
    {
        if (eastBlockade.activeSelf || northBlockade.activeSelf || westBlockade.activeSelf || southBlockade.activeSelf)
        {
            turnCount++;
        }

        if (turnCount == 1)
        {
            thirdLife.SetActive(false);
        }

        if (turnCount == 2)
        {
            secondLife.SetActive(false);
        }

        if (turnCount == 3)
        {
            firstLife.SetActive(false);
        }

    }
}
