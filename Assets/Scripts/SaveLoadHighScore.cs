using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadHighScore : MonoBehaviour
 {

 public int score = 0;
 public int highScore = 0;
 public Text text;
 string highScoreKey = "HighScore";

     void Start()
     {
         //Get the highScore from player prefs if it is there, 0 otherwise.
         highScore = PlayerPrefs.GetInt(highScoreKey,0);   
         text.text = "HighScore: " + score.ToString(); 
     }
public void SetPlayerHighscores(int scorePoints)
{
    if(score>highScore)
    {
             PlayerPrefs.SetInt(highScoreKey, score);
             PlayerPrefs.Save();
    }
}
 }
