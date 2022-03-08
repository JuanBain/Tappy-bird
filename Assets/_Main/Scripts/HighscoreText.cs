using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Text))]
public class HighscoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text highscore; 
    void OnEnable()
    {
      
        highscore = GetComponent<TMP_Text>();
        highscore.text = "Highscore: "+PlayerPrefs.GetInt("HighScore").ToString();
        //highscore.text
    }

}
