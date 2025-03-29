using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    public GameObject spawner;
    public GameObject victoryCanvas;

    public AudioClip victorySound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateScoreText();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreText();

        if (score >= 10)
        {
            DestroyAllMonsters();

            if (spawner != null)
            {
                spawner.SetActive(false);
            }

            if (victoryCanvas != null)
            {
                victoryCanvas.SetActive(true);

                if (victorySound != null)
                {
                    audioSource.PlayOneShot(victorySound);
                }
            }
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Á¡¼ö:" + score.ToString();
        }
    }

    void DestroyAllMonsters()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters)
        {
            Destroy(monster.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }

        Apple[] apples = FindObjectsOfType<Apple>();
        foreach (Apple apple in apples)
        {
            Destroy(apple.gameObject);
        }
    }
}
