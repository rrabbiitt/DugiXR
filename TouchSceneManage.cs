using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchSceneManage : MonoBehaviour
{
    public LoadData loadData;

    [SerializeField]
    private bool isWantChage;
    [SerializeField]
    private bool isCharged;

    public TextMeshProUGUI CountText;
    public GameObject Heat;
    public int WindCount;

    private Vector3 startPosition;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        loadData = FindObjectOfType<LoadData>();
        isCharged = false;
        WindCount = 0;
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isWantChage)
        {
            if (isCharged)
            {
                SceneManager.LoadScene("Loading");
            }
        }
        else
        {
            isCharged = false;
        }

        if (SceneManager.GetActiveScene().name == "Firework")
        {
            if (WindCount >= 10)
            {
                Heat.SetActive(false);
            }
        }
    }

    public void ChangeScene(string scenename)
    {
        isWantChage = true;
        loadData.NextScene = scenename;
        StartCoroutine(TwoLoad());
    }
    public void ChangeSceneRightNow(string scenename)
    {
        isWantChage = true;
        loadData.NextScene = scenename;
        isCharged = true;
    }

    public void StopChange()
    {
        isWantChage = false;
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
    }

    public void Ending(string scenename)
    {
        if (SceneManager.GetActiveScene().name == "Firework")
        {
            if (WindCount >= 10)
            {
                loadData.NextScene = scenename;
                loadData.IsClear = true;
                loadData.PreviousScene = "Firework";
                isWantChage = true;
                isCharged = true;
            }
        }
    }

    IEnumerator TwoLoad()
    {
        yield return new WaitForSeconds(2);
        isCharged = true;
        yield return new WaitForEndOfFrame();
    }

    public void WindCountUP()
    {
        if (SceneManager.GetActiveScene().name == "Firework")
        {
            WindCount += 1;
            CountText.text = WindCount.ToString() + " / 10";
        }
    }
}
