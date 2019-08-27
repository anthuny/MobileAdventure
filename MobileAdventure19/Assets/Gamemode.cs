using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    int wrongAns;
    // Start is called before the first frame update
    void Start()
    {
        Think();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Think()
    {
        wrongAns = Random.Range(1, 4);
    }
}
