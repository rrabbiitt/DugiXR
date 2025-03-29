using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int startingLives = 3;  // �÷��̾��� �ʱ� ���
    private int currentLives;       // ���� ���

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

    public Image screenFlashImage;  // ȭ�� �÷��ÿ� ���� Image ������Ʈ�� ���� ����
    public Color flashColor = new Color(1f, 0f, 0f, 0.8f);  // ������ �� 50% ������ ����
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
        // ���� ����� �ִ� ������� ���� ��쿡�� ����
        if (currentLives < startingLives)
        {
            currentLives++;

            // ����Ʈ���� ��Ȱ��ȭ�� ��� ������Ʈ�� ã�� Ȱ��ȭ
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

        // �÷��̾ ����� ��� ������ ���� ���� �Ǵ� �ٸ� ó���� ������ �� �ֽ��ϴ�.
        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            //Debug.Log("Lives Remaining: " + currentLives);

            // ���� ����� �ش��ϴ� ������Ʈ�� ��Ȱ��ȭ
            hearts[currentLives].SetActive(false);
        }
    }

    IEnumerator FlashScreen()
    {
        if (screenFlashImage != null)
        {
            // �÷��� ���� ����
            screenFlashImage.color = flashColor;

            // ������ �Ⱓ ���� ���
            yield return new WaitForSeconds(flashDuration);

            // ������ �����ϰ� �缳��
            screenFlashImage.color = Color.clear;
        }
    }

    void GameOver()
    {
        heart1.SetActive(false);

        wave1Script.enabled = false;
        wave2Script.enabled = false;
        wave3Script.enabled = false;

        // �����Ǿ��ִ� ���͵��� ���� �ı�
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

        // ���� ���� UI�� Ȱ��ȭ
        gameoverCanvas.SetActive(true);        

        if (gameoverSound != null)
        {
            audioSource.PlayOneShot(gameoverSound);
        }
    }
}
