using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCandleSystem : MonoBehaviour
{
    public TouchSceneManage sceneManage;
    public float speed = 3.0f;
    public bool isTouch;
    public GameObject LinkedFire;
    public GameObject[] LinkedEmber;

    // Start is called before the first frame update
    void Start()
    {
        isTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouch)
        {
            if (LinkedFire != null)
            {
                LinkedFire.SetActive(true);
            }
            for (int i = 0; i < LinkedEmber.Length; i++)
            {
                if (LinkedEmber[i] != null)
                {
                    LinkedEmber[i].SetActive(true);
                }
            }
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime, Space.World);

            if (transform.position.y > 20)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void GetWindCandle()
    {
        isTouch = true;
        sceneManage.WindCountUP();
    }
}
