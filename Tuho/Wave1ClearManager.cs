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
        // WAVE1 ��ũ��Ʈ�� ��Ȱ��ȭ�Ǿ� �ְ�, ����� 0�� �ƴϰ�, ��ȯ�Ǿ��ִ� ��� ���� �����յ��� �ı��� ���
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
        // Ŭ���� �Ҹ� ���
        if (clearSoundPrefab != null)
        {
            Instantiate(clearSoundPrefab);
        }

        // CLEAR_TEXT Ȱ��ȭ
        clearText.SetActive(true);

        // 3�� ���
        yield return new WaitForSeconds(3f);

        // WAVE1_TEXT, CLEAR_TEXT ��Ȱ��ȭ
        wave1Text.SetActive(false);
        clearText.SetActive(false);

        // WAVE2_TEXT, WAVE2 ��ũ��Ʈ Ȱ��ȭ
        wave2Text.SetActive(true);
        if (wave2Script != null)
        {
            wave2Script.SetActive(true);
        }
    } */
}
