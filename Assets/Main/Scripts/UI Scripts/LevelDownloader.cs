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
    private string path;

    private string ID;
    private string levelName;

    public void Download()
    {
        Text[] info = GetComponentsInChildren<Text>();
        ID = info[0].text;
        levelName = info[1].text;

        path = string.Concat(Application.persistentDataPath + "/", levelName + ".txt");

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
        SceneManager.LoadScene("CustomLevel");

    }
}
