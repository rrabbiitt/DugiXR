using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class GameInstruction : MonoBehaviour
{
    public LoadData loadScenedata;

    public GameObject videoObject;
    public VideoPlayer videoPlayer;
    public VideoClip[] clips;
    public GameObject instructionObject;
    public GameObject instruction;
    public Material[] instructionMaterial;

    // Start is called before the first frame update
    void Start()
    {
        loadScenedata = FindObjectOfType<LoadData>();

        if (loadScenedata.IsClear)
        {
            switch (loadScenedata.PreviousScene)
            {
                case "Firework":
                    videoPlayer.clip = clips[1];
                    break;
                case "JegiGame":
                    videoPlayer.clip = clips[2];
                    break;
                case "MoonHouse":
                    videoPlayer.clip = clips[3];
                    break;
                case "Tuho":
                    videoPlayer.clip = clips[4];
                    break;
            }
            instructionObject.SetActive(false);
            videoObject.SetActive(true);
            loadScenedata.IsClear = false;
        }
        else if (loadScenedata.NextScene == "Ending")
        {
            videoPlayer.clip = clips[0];
            instructionObject.SetActive(false);
            videoObject.SetActive(true);
        }
        else
        {
            videoPlayer.enabled = false;
            switch (loadScenedata.NextScene)
            {
                case "Firework":
                    instruction.GetComponent<MeshRenderer>().material = instructionMaterial[1];
                    break;
                case "JegiGame":
                    instruction.GetComponent<MeshRenderer>().material = instructionMaterial[2];
                    break;
                case "MoonHouse":
                    instruction.GetComponent<MeshRenderer>().material = instructionMaterial[3];
                    break;
                case "Tuho":
                    instruction.GetComponent<MeshRenderer>().material = instructionMaterial[4];
                    break;
                default:
                    instruction.GetComponent<MeshRenderer>().material = instructionMaterial[0];
                    break;
            }
        }
    }
}
