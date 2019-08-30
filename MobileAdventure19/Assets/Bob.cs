using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{ 
    public GameObject speechBubble;
    private Vector2 speechBubblePos;
    public float lowest;
    public float highest;
    private float t;
    public float timeToTravel;

    public bool hitTop;
    public bool hitBot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speechBubblePos = transform.position;

        if (speechBubblePos.x == 1)
        {
            hitBot = false;
            hitTop = true;
        }

        if (speechBubblePos.x == 0)
        {
            hitBot = true;
            hitTop = false;
        }

        if (hitBot)
        {
            speechBubblePos = new Vector2(0, Mathf.Lerp(lowest, highest, t));
        }

        if (hitTop)
        {
            speechBubblePos = new Vector2(0, Mathf.Lerp(highest, lowest, t));
        }

        t += timeToTravel * Time.deltaTime;
        Debug.Log(t);
    }

    void bobUp()
    {


    }
}
