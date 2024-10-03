using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultPopup : MonoBehaviour
{

    public TextMeshProUGUI titleLabel;
    public TextMeshProUGUI scoreLabel;
    //public GameObject highScoreLabel; 이전 하이스코어 비교 코드


    public GameObject highScorePopup;

    private void OnEnable()
    {
        Time.timeScale = 0;


        if (GameManager.Instance.isCleared)
        {

            SaveHighScore();



            
            // 이전 점수와 현재 점수 비교하여 High score 나타내는 코드
            //float highScore = PlayerPrefs.GetFloat("HighScore", 0);

            //if(highScore < GameManager.Instance.timeLimit)
            //{
            //    highScoreLabel.SetActive(true);
            //    PlayerPrefs.SetFloat("HighScore", GameManager.Instance.timeLimit);
            //    PlayerPrefs.Save();

            //}

            //else
            //{
            //    highScoreLabel.SetActive(false);
            //}



            


            titleLabel.text = "Cleared!";
            scoreLabel.text = GameManager.Instance.timeLimit.ToString("#.##");
        }

        else 
        {
            titleLabel.text = "Game Over!";
            scoreLabel.text = "";
        }


    }


    void SaveHighScore()
    {
        float score = GameManager.Instance.timeLimit;

        string currentScoreString = score.ToString("#.###"); // 소수점 세자리까지 저장

        string savedScoreString = PlayerPrefs.GetString("HighScores", "");


        if (savedScoreString == "")
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }

        else
        {

            string[] scoreArray = savedScoreString.Split(",");
            List<string> scoreList = new List<string>(scoreArray);

            for (int i=0; i<scoreList.Count; i++)
            {
                float savedScore = float.Parse(scoreList[i]);   //  Parse는 string을 float형으로 바꿔주는 것, i번째 스코어 리스트에 들어있는 문자열을 plot형으로 바꿔준다.

                if (savedScore < score)
                {
                    scoreList.Insert(i, currentScoreString);
                    break;   // 삽입이 끝났으므로 for문 밖으로 나온다.
                }
            }

            if(scoreArray.Length == scoreList.Count)
            {
                scoreList.Add(currentScoreString);
            }


            if (scoreList.Count > 10)
            {
                scoreList.RemoveAt(0);
            }


            string result = string.Join(",", scoreList);
            PlayerPrefs.SetString("HighScores", result);

        }

       
        PlayerPrefs.Save();
    }


    public void PlayAgainPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }


    public void HighScorePressed()
    {
        highScorePopup.SetActive(true);
    }

}
