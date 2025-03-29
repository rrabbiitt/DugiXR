using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearManager : MonoBehaviour
{
    /*
    public GameObject clearSoundPrefab;
    public GameObject wave1Text;
    public GameObject clearText;
    public GameObject wave2Text;
    public GameObject wave2Script;

    private void Update()
    {
        // WAVE1 스크립트가 비활성화되어 있고, 목숨이 0이 아니고, 소환되어있는 모든 몬스터 프리팹들이 파괴된 경우
        if (!IsWave1Active() && AreMonstersDestroyed() && AreLivesRemaining())
        {
            StartCoroutine(GameClearRoutine());
        }
    }

    bool IsWave1Active()
    {
        Wave1 wave1Script = FindObjectOfType<Wave1>();
        return wave1Script != null && wave1Script.enabled;
    }

    bool AreMonstersDestroyed()
    {
        return GameObject.FindWithTag("Monster") == null;
    }

    bool AreLivesRemaining()
    {
        Player player = FindObjectOfType<Player>();
        return player != null && player.GetLives() > 0;
    }

    IEnumerator GameClearRoutine()
    {
        // 클리어 소리 재생
        if (clearSoundPrefab != null)
        {
            Instantiate(clearSoundPrefab);
        }

        // CLEAR_TEXT 활성화
        clearText.SetActive(true);

        // 3초 대기
        yield return new WaitForSeconds(3f);

        // WAVE1_TEXT, CLEAR_TEXT 비활성화
        wave1Text.SetActive(false);
        clearText.SetActive(false);

        // WAVE2_TEXT, WAVE2 스크립트 활성화
        wave2Text.SetActive(true);
        if (wave2Script != null)
        {
            wave2Script.SetActive(true);
        }
    } */
}
