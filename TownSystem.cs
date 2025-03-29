using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownSystem : MonoBehaviour
{
    public GuideSystemManager guide;
    public GameObject Player;

    public Animator[] jegi_ani;
    public Animator[] Nakhwa_ani;
    public Animator[] Firework_ani;
    public Animator[] Tohow_ani;

    public GameObject[] Conversations;

    public TouchSceneManage NextScene;
    public CompleteGameData completeGameData;

    // Start is called before the first frame update
    void Start()
    {
        completeGameData = FindObjectOfType<CompleteGameData>();
        for (int i = 0; i < Conversations.Length; i++)
        {
            Conversations[i].SetActive(false);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.tag == "Event")
        {
            switch (collision.transform.name)
            {
                case "Jegi":
                    for (int i = 0; i < jegi_ani.Length; i++)
                        jegi_ani[i].SetTrigger("EVENT");

                    Conversations[0].SetActive(true);

                    break;
                case "Nakhwa":
                    for (int i = 0; i < Nakhwa_ani.Length; i++)
                        Nakhwa_ani[i].SetTrigger("EVENT");

                    Conversations[1].SetActive(true);

                    break;
                case "Firework":
                    for (int i = 0; i < Firework_ani.Length; i++)
                        Firework_ani[i].SetTrigger("EVENT");

                    Conversations[2].SetActive(true);

                    break;
                case "Tohow":
                    for (int i = 0; i < Tohow_ani.Length; i++)
                        Tohow_ani[i].SetTrigger("EVENT");

                    Conversations[3].SetActive(true);

                    break;
            }
            guide.pushLittle();
        }

        else if (collision.gameObject.layer == 6)
        {
            switch (collision.transform.tag)
            {
                case "Jegi":
                    NextScene.ChangeSceneRightNow("JegiGame");

                    break;
                case "Nakwha":
                    NextScene.ChangeSceneRightNow("Firework");

                    break;
                case "Fire":
                    NextScene.ChangeSceneRightNow("MoonHouse");

                    break;
                case "Toho":
                    NextScene.ChangeSceneRightNow("Tuho");

                    break;
                case "Ending":
                    NextScene.ChangeSceneRightNow("Ending");

                    break;
            }
        }

        else if (collision.gameObject.tag == "Wall")
        {
            Player.transform.position = new Vector3(0,0,0);
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform.tag == "Event")
        {
            switch (collision.transform.name)
            {
                case "Jegi":
                    for (int i = 0; i < jegi_ani.Length; i++)
                        jegi_ani[i].SetTrigger("IDLE");
                    break;
                case "Nakhwa":
                    for (int i = 0; i < Nakhwa_ani.Length; i++)
                        Nakhwa_ani[i].SetTrigger("IDLE");
                    break;
                case "Firework":
                    for (int i = 0; i < Firework_ani.Length; i++)
                        Firework_ani[i].SetTrigger("IDLE");
                    break;
                case "Tohow":
                    for (int i = 0; i < Tohow_ani.Length; i++)
                        Tohow_ani[i].SetTrigger("IDLE");
                    break;
            }
        }
    }
}
