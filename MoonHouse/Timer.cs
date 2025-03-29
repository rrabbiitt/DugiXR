using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public LoadData loadData;
    public CompleteGameData completeGameData;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    private float remainingTime = 120f;

    public GameObject victoryCanvas;
    public GameObject gameoverCanvas;
    public GameObject time_score;
    public int score = 0;

    public FiremanSpawner spawner;

    public AudioClip passSound;
    public AudioClip gameoverSound;
    private AudioSource audioSource;

    private bool isDone;
    
    private void Start()
    {
        completeGameData = FindObjectOfType<CompleteGameData>();
        loadData = FindObjectOfType<LoadData>();

        audioSource = gameObject.AddComponent<AudioSource>();

        isDone = false;
    }

    void Update()
    {
        if (score == 10)
        {
            Fireman_L[] firemans_L = FindObjectsOfType<Fireman_L>();
            foreach (Fireman_L fireman_L in firemans_L)
            {
                Destroy(fireman_L.gameObject);
            }

            Fireman_R[] firemans_R = FindObjectsOfType<Fireman_R>();
            foreach (Fireman_R fireman_R in firemans_R)
            {
                Destroy(fireman_R.gameObject);
            }

            spawner.enabled = false;
            time_score.SetActive(false);
            victoryCanvas.SetActive(true);

            CanRotation canRotation = FindObjectOfType<CanRotation>();
            if (canRotation != null)
            {
                canRotation.GameOverDestroy();
            }

            CanController canController = FindObjectOfType<CanController>();
            if (canController != null)
            {
                canController.enabled = false;
            }

            completeGameData.IsMoonHouseClear = true;
            loadData.NextScene = "Town";
            loadData.IsClear = true;
            loadData.PreviousScene = "MoonHouse";

            audioSource.PlayOneShot(passSound);
            score = 11;
        }

        else if (score < 10)
        {
            // 시간이 남아있으면 감소
            if (remainingTime > 0f)
            {
                remainingTime -= Time.deltaTime;

                // 시간을 정수로 변환하여 텍스트에 표시
                int seconds = Mathf.RoundToInt(remainingTime);
                timerText.text = "남은 시간 : " + seconds.ToString() + "초";
            }
            else // 시간이 없으면 게임오버
            {
                if (!isDone)
                {
                    Fireman_L[] firemans_L = FindObjectsOfType<Fireman_L>();
                    foreach (Fireman_L fireman_L in firemans_L)
                    {
                        Destroy(fireman_L.gameObject);
                    }

                    Fireman_R[] firemans_R = FindObjectsOfType<Fireman_R>();
                    foreach (Fireman_R fireman_R in firemans_R)
                    {
                        Destroy(fireman_R.gameObject);
                    }

                    spawner.enabled = false;
                    time_score.SetActive(false);
                    gameoverCanvas.SetActive(true);

                    CanRotation canRotation = FindObjectOfType<CanRotation>();
                    if (canRotation != null)
                    {
                        canRotation.GameOverDestroy();
                    }

                    CanController canController = FindObjectOfType<CanController>();
                    if (canController != null)
                    {
                        canController.enabled = false;
                    }
                    loadData.NextScene = "Town";

                    audioSource.PlayOneShot(gameoverSound);

                    isDone = true;
                }
            }
        }
        
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "획득한 불씨 : " + score + "/10";
    }
}
