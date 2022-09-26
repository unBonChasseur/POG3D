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
    int Countdown;
    [SerializeField]
    TextMeshProUGUI TxtCountdown;
    [SerializeField] 
    AudioSource sonStart;

    IEnumerator myCouroutine;
    // Start is called before the first frame update
    void Start()
    {
        Countdown = 10;
        myCouroutine = InfiniteLoop(1f);
        startCoundown();
        sonStart = GetComponent<AudioSource>();
        sonStart.volume = 0.3f;
        sonStart.Play();



    }
    IEnumerator InfiniteLoop(float seconds)
    {
        while (true)
        {
            if (Countdown > 0)
            {
                Countdown = Countdown - 1;
                TxtCountdown.text = Countdown.ToString();
                sonStart = GetComponent<AudioSource>();
                sonStart.volume = 0.3f;
                sonStart.Play();
            }
            else if(Countdown == 0)
            {
                // play audio
                // stop coroutine
                sonStart = GetComponent<AudioSource>();
                sonStart.volume = 1f;
                sonStart.Play();

                TxtCountdown.text = "";
                StopCoroutine(myCouroutine);
            }

            
            yield return new WaitForSeconds(seconds);
            //Debug.Log("Inside infiniteLoop");
            //Debug.Log("Lancement dans :");
            //print(compte);
            //infoText1.color = new Color(0f, 0f, 0f, Random.Range(0.5f, 0.9f));
            
            

        }
    }
    
    int getCountdown() {
        return Countdown;
    }
    void startCoundown() {
        StartCoroutine(myCouroutine);
    }
    void resetCoundown() {
        StopCoroutine(myCouroutine);
        Countdown = 10;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
