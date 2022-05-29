using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUpdater : MonoBehaviour
{
    private Text _Text = null;

    private void Awake()
    {
        _Text = GetComponent<Text>();
    }

    private void Update()
    {
        _Text.text = "Score: " + Gate.score.ToString();
    }
}
