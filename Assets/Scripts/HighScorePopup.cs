using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScorePopup : MonoBehaviour
{

    public TextMeshProUGUI scoresLabel;


    private void OnEnable()
    {
        string[] scores = PlayerPrefs.GetString("HighScores", "").Split(",");


        string result = "";

        for (int i=0; i<scores.Length; i++)
        {
            result += (i + 1) + ". " + scores[i] + "\n";   // \m은 줄바꿈 기능
        }

        scoresLabel.text = result;
        

    }

    public void ClosePressed()
    {
        gameObject.SetActive(false);
    }


}
