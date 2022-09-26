using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ScorePog : MonoBehaviour
{

    [SerializeField]
    int ScoreP1;
    [SerializeField]
    int ScoreP2;
    // Start is called before the first frame update
    void Start()
    {
        ScoreP1 = 0;
        ScoreP2 = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (ScoreP1 > 10) {
            //Debug.Log("P1 WIN");

        }else if (ScoreP1 > 10)
        {
            //  Debug.Log("P2 WIN");
        }
    }
    void P1Plus() {
        ScoreP1 = ScoreP1 + 1;
    }
    void P2Plus()
    {
        ScoreP1 = ScoreP1 + 1;
    }
    int ScoreDEP1() {
        return ScoreP1;
    }
    int ScoreDEP2()
    {
        return ScoreP2;
    }
    void ResetScore() {
        ScoreP1 = 0;
        ScoreP2 = 0;
    }
}
