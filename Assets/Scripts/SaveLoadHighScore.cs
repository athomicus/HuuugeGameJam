using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadHighScore : MonoBehaviour
 {

 public int highScore = 0;
 public Text text;
 string highScoreKey = "HighScore";

     void Start()
     {
         //Get the highScore from player prefs if it is there, 0 otherwise.
         highScore = PlayerPrefs.GetInt(highScoreKey,0);
        if( text == null ) return;
         text.text = "HighScore: " + highScore.ToString(); 
     }
public void SetPlayerHighscores(int scorePoints)
{
    if( scorePoints > highScore)
    {
             PlayerPrefs.SetInt(highScoreKey, scorePoints );
             PlayerPrefs.Save();
    }
}
 }
