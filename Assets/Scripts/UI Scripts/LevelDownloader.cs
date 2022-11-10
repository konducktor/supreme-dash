using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LevelDownloader : MonoBehaviour
{
    private string url;
    private string[] keys;
    private string[] values;

    private string ID;
    private string levelName;

    public void GetProfile(Text name)
    {
        GameObject.Find("Main Camera").GetComponent<ServerLogic>().GetProfile(name.text);
    }

    public void Download()
    {
        Text[] info = GetComponentsInChildren<Text>();
        ID = info[1].text;
        levelName = info[0].text;

        url = "https://beloeozero.ru/ppdsh/levels/" + ID + ".txt";

        StartCoroutine(Get(url));
    }

    IEnumerator Get(string url)
    {
        UnityWebRequest level = UnityWebRequest.Get(url);

        yield return level.SendWebRequest();

        if (level.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(level.error);
            yield break;
        }

        EditorLogic.levelData = level.downloadHandler.text;
        GameLoader.currentID = Convert.ToInt32(ID);
        SceneTransition.ChangeScene("CustomLevel");
    }
}
