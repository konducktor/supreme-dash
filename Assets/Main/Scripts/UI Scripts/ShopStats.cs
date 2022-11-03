using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShopStats : MonoBehaviour
{
    [SerializeField] private Text gems, doublers, triplers;

    void OnEnable()
    {
        RefreshStats();
    }

    IEnumerator Send(string url, string[] keys, string[] values)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        for (int i = 0; i < keys.Length; i++)
        {
            form.Add(new MultipartFormDataSection(keys[i], values[i]));
        }

        UnityWebRequest www = UnityWebRequest.Post(ServerLogic.urlBase + url, form);

        yield return www.SendWebRequest();


        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(www.error);
            yield break;
        }

        string result = www.downloadHandler.text;


        ServerLogic.Profile prof = JsonUtility.FromJson<ServerLogic.Profile>(result);

        gems.text = prof.gems.ToString();
        doublers.text = prof.doublers.ToString();
        triplers.text = prof.triplers.ToString();
    }

    public void RefreshStats()
    {
        doublers.text = GlobalData.Doublers.ToString();
        triplers.text = GlobalData.Triplers.ToString();

        var url = "getprofile.php";
        var keys = new string[] { "username" };
        var values = new string[] {
            GlobalData.Login
        };

        StartCoroutine(Send(url, keys, values));
    }
}
