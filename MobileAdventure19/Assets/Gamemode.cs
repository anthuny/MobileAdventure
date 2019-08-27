using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemode : MonoBehaviour
{
    public int wrongAns;
    public bool winCondition;
    public int score = 0;

    public Text scoreText;
    public Text endGameText;
    // Start is called before the first frame update
    void Start()
    {
        Think();
        score = 0;
    }

    public void Think()
    {
        //Set direction
        wrongAns = Random.Range(1, 5);
        
        //Set text to display direction
        if (wrongAns == 1)
        {
            endGameText.text = "Go Left";
        }

        if (wrongAns == 2)
        {
            endGameText.text = "Go Up";
        }

        if (wrongAns == 3)
        {
            endGameText.text = "Go Right";
        }

        if (wrongAns == 4)
        {
            endGameText.text = "Go Down";
        }
    }

    public void SetRoundEnd()
    {
        //If the player chooses incorrectly
        if (!winCondition)
        {
            endGameText.text = "Wrong!".ToString();

            //Call function
            ScoreReset();
        }
        
        //If the player chooses correctly
        else
        {
            //Call function
            ScoreUp();
        }
    }

    //Increase the score by 1
    void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
        
    }

    //Reset the score
    void ScoreReset()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
