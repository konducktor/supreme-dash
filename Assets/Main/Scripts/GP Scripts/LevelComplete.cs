using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LevelComplete : MonoBehaviour
{
    private class Result
    {
        public int gems;
        public int rank;
    }

    [SerializeField] private RankBar rank;
    [SerializeField] private Text gems;

    void OnEnable()
    {
        string[] keys = new string[] { "login", "password", "id" };
        string[] values = new string[] { GlobalData.Login, GlobalData.Password, GameLoader.currentID.ToString() };

        StartCoroutine(LevelCompleting(keys, values));
    }

    IEnumerator LevelCompleting(string[] keys, string[] values)
    {
        string url = ServerLogic.urlBase + "levelcomplete.php";

        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        for (int i = 0; i < keys.Length; i++)
        {
            form.Add(new MultipartFormDataSection(keys[i], values[i]));
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();


        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(www.error);
            yield break;
        }

        string result = www.downloadHandler.text;
        Debug.Log(result);

        if (result == "1")
        {
            rank.gameObject.SetActive(false);
            gems.gameObject.SetActive(false);
        }

        Result res = JsonUtility.FromJson<Result>(result);

        rank.Rank = res.rank;
        gems.text = "+" + res.gems;
    }
}
