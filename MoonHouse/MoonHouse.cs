using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonHouse : MonoBehaviour
{
    public GameObject fire01Prefab;
    public GameObject fire02Prefab;
    public GameObject fire03Prefab;
    public GameObject gameoverCanvas;

    public AudioClip gameoverSound;
    private AudioSource audioSource;

    public float initialFireIntensity = 2f;
    public float decreaseRate = 1f; // 감소 속도
    public float increaseRate = 1f; // 증가 속도
    public float maxFireIntensity = 3f;
    public float timeBetweenDecrease = 5f; // 불의 세기가 감소하는 주기

    public float currentFireIntensity;

    bool isVictory = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        currentFireIntensity = initialFireIntensity;
        StartCoroutine(UpdateFireIntensity());
    }

    public void SetIsVictory(bool value)
    {
        isVictory = value;
    }

    IEnumerator UpdateFireIntensity()
    {
        while (currentFireIntensity > 0 && !isVictory)
        {
            yield return new WaitForSeconds(timeBetweenDecrease);

            DecreaseFireIntensity();
        }
        if (!isVictory)
        {
            // 불의 세기가 0이 되면 실행
            Timer timerScript = FindObjectOfType<Timer>();
            if (timerScript != null)
            {
                timerScript.enabled = false; // Timer 스크립트 비활성화
            }

            if (gameoverSound != null)
            {
                audioSource.PlayOneShot(gameoverSound);
            }

            // GameOver Canvas 활성화
            gameoverCanvas.SetActive(true);

            CanRotation canRotation = FindObjectOfType<CanRotation>();

            canRotation.GameOverDestroy();

            CanSpawner canSpawner = FindObjectOfType<CanSpawner>();
            if (canSpawner != null)
            {
                canSpawner.enabled = false;
            }
        }
        
    }

    void DecreaseFireIntensity()
    {
        currentFireIntensity = Mathf.Max(0f, currentFireIntensity - decreaseRate);

        // 불의 세기에 따라 다른 이펙트 활성화 및 비활성화
        if (currentFireIntensity == 0)
        {
            fire01Prefab.SetActive(false);
            fire02Prefab.SetActive(false);
            fire03Prefab.SetActive(false);
        }
        else if (currentFireIntensity == 1)
        {
            fire01Prefab.SetActive(true);
            fire02Prefab.SetActive(false);
            fire03Prefab.SetActive(false);
        }
        else if (currentFireIntensity == 2)
        {
            fire01Prefab.SetActive(false);
            fire02Prefab.SetActive(true);
            fire03Prefab.SetActive(false);
        }
        else if (currentFireIntensity == 3)
        {
            fire01Prefab.SetActive(false);
            fire02Prefab.SetActive(false);
            fire03Prefab.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("can"))
        {
            IncreaseFireIntensity();
        }
    }

    void IncreaseFireIntensity()
    {
        currentFireIntensity = Mathf.Min(maxFireIntensity, currentFireIntensity + increaseRate);

        // 불의 세기에 따라 다른 이펙트 활성화 및 비활성화
        if (currentFireIntensity >= 1)
        {
            fire01Prefab.SetActive(true);
        }
        else
        {
            fire01Prefab.SetActive(false);
        }

        if (currentFireIntensity >= 2)
        {
            fire02Prefab.SetActive(true);
        }
        else
        {
            fire02Prefab.SetActive(false);
        }

        if (currentFireIntensity >= 3)
        {
            fire03Prefab.SetActive(true);
        }
        else
        {
            fire03Prefab.SetActive(false);
        }
    }
}
