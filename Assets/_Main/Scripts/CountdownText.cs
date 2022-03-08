using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Text))]
public class CountdownText : MonoBehaviour
{
    public delegate void CountdownFinished();
    public static event CountdownFinished OnCountDownFinished;

    TMP_Text countdown;
    private void OnEnable()
    {
        countdown = GetComponent<TMP_Text>();
        countdown.text = "3";
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        int count = 3;
        for (int i = 0; i < count; i++)
        {
            countdown.text = (count - i).ToString();
            yield return new WaitForSeconds(1);
        }
        OnCountDownFinished();
    }

}
