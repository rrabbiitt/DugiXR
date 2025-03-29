using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public void UpdateScore()
    {
        score++;
        scoreText.text = "È¹µæÇÑ ºÒ¾¾ : " + score.ToString() + "/30";
    }

    void GameClear()
    {
        if (score == 20)
        {

        }
    }
}
