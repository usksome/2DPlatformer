using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultPopup : MonoBehaviour
{

    public TextMeshProUGUI titleLabel;
    public TextMeshProUGUI scoreLabel;
    //public GameObject highScoreLabel; ���� ���̽��ھ� �� �ڵ�


    public GameObject highScorePopup;

    private void OnEnable()
    {
        Time.timeScale = 0;


        if (GameManager.Instance.isCleared)
        {

            SaveHighScore();



            
            // ���� ������ ���� ���� ���Ͽ� High score ��Ÿ���� �ڵ�
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

        string currentScoreString = score.ToString("#.###"); // �Ҽ��� ���ڸ����� ����

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
                float savedScore = float.Parse(scoreList[i]);   //  Parse�� string�� float������ �ٲ��ִ� ��, i��° ���ھ� ����Ʈ�� ����ִ� ���ڿ��� plot������ �ٲ��ش�.

                if (savedScore < score)
                {
                    scoreList.Insert(i, currentScoreString);
                    break;   // ������ �������Ƿ� for�� ������ ���´�.
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
