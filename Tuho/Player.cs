using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int startingLives = 3;  // 플레이어의 초기 목숨
    private int currentLives;       // 현재 목숨

    public GameObject gameoverCanvas;
    public GameObject wave1Text;
    public GameObject wave2Text;
    public GameObject wave3Text;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public Wave1 wave1Script;
    public Wave2 wave2Script;
    public Wave3 wave3Script;

    public AudioClip gameoverSound;
    private AudioSource audioSource;

    public Image screenFlashImage;  // 화면 플래시에 사용될 Image 컴포넌트에 대한 참조
    public Color flashColor = new Color(1f, 0f, 0f, 0.8f);  // 빨간색 및 50% 투명도의 색상
    public float flashDuration = 0.2f;

    private List<GameObject> hearts;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        currentLives = startingLives;
        //spawner.SetActive(true);
        gameoverCanvas.SetActive(false);

        hearts = new List<GameObject> { heart1, heart2, heart3 };
    }

    public void IncreaseLives()
    {
        // 현재 목숨이 최대 목숨보다 작은 경우에만 실행
        if (currentLives < startingLives)
        {
            currentLives++;

            // 리스트에서 비활성화된 목숨 오브젝트를 찾아 활성화
            for (int i = currentLives - 1; i < startingLives; i++)
            {
                if (!hearts[i].activeSelf)
                {
                    hearts[i].SetActive(true);
                    break;
                }
            }
        }
    }

    public void DecreaseLives()
    {
        currentLives--;

        StartCoroutine(FlashScreen());

        // 플레이어가 목숨을 모두 잃으면 게임 오버 또는 다른 처리를 수행할 수 있습니다.
        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            //Debug.Log("Lives Remaining: " + currentLives);

            // 현재 목숨에 해당하는 오브젝트를 비활성화
            hearts[currentLives].SetActive(false);
        }
    }

    IEnumerator FlashScreen()
    {
        if (screenFlashImage != null)
        {
            // 플래시 색상 설정
            screenFlashImage.color = flashColor;

            // 지정된 기간 동안 대기
            yield return new WaitForSeconds(flashDuration);

            // 색상을 투명하게 재설정
            screenFlashImage.color = Color.clear;
        }
    }

    void GameOver()
    {
        heart1.SetActive(false);

        wave1Script.enabled = false;
        wave2Script.enabled = false;
        wave3Script.enabled = false;

        // 생성되어있는 몬스터들을 전부 파괴
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

        // 게임 오버 UI를 활성화
        gameoverCanvas.SetActive(true);        

        if (gameoverSound != null)
        {
            audioSource.PlayOneShot(gameoverSound);
        }
    }
}
