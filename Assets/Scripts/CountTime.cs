using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers
using TMPro; // text mesh PRO

public class CountTime : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI TxtCountdown;
   
    IEnumerator myCouroutine;
    // Start is called before the first frame update
    void Start()
    {
        myCouroutine = Countdown(1);
        //startCoundown();
    }
    IEnumerator Countdown(int seconds)
    {
        while (seconds >= 0)
        {
            if (seconds > 0)
            {
                seconds--;
                TxtCountdown.text = seconds.ToString();
            }
            else if(seconds == 0)
            {
                TxtCountdown.text = "";
            }

            yield return new WaitForSeconds(1);
        }
    }

    void startCoundown() {
        StartCoroutine(myCouroutine);
    }

}
