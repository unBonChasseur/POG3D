using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers
using TMPro; // text mesh PRO
using System.Security.Cryptography;
using System.Collections.Specialized;

public class TxtColorBlink : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI infoText1;
    [SerializeField]
    TextMeshProUGUI infoText2;
    [SerializeField]
    TextMeshProUGUI infoText3;
    [SerializeField]
    TextMeshProUGUI infoText4;
    [SerializeField]
    TextMeshProUGUI infoText5;
    [SerializeField]
    float TempsSeconde;

    IEnumerator myCouroutine;

    void Start()
    {
        myCouroutine = InfiniteLoop(TempsSeconde);
        StartCoroutine(myCouroutine);
    }
    void update() {
        //transform.localScale = new Vector3(Random.Range(0.2f, 5.0f), 1, Random.Range(0.2f, 5.0f));
    }

    IEnumerator InfiniteLoop(float seconds)
    {
        while (true)
        {

            yield return new WaitForSeconds(seconds);
            Debug.Log("Inside infiniteLoop");
            infoText1.color = new Color(0f, 0f, 0f, Random.Range(0.5f, 0.9f));
            infoText2.color = new Color(0f, 0f, 0f, Random.Range(0.5f, 0.9f));
            infoText3.color = new Color(1f, 0f, 0.5f, Random.Range(0.5f, 0.9f));
            infoText4.color = new Color(0f, 0f, 0f, Random.Range(0.5f, 0.9f));
            infoText5.color = new Color(0f, 0f, 0f, Random.Range(0.5f, 0.9f));
            //infoText.color = new Color(1f, 1f, 1f, 0.7f);

        }
    }



}
