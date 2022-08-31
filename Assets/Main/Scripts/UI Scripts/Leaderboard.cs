using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

class LeaderboardPosition
{
    public string[] names;
    public string[] cps;

    public LeaderboardPosition(string[] names, string[] cps)
    {
        this.names = names;
        this.cps = cps;
    }
}

public class Leaderboard : MonoBehaviour
{
    private string url;

    [SerializeField] private Transform leaderboard;
    [SerializeField] private GameObject positionObject;

    public void GetLeaderboard()
    {
        url = "data/creators.txt";
        base.StartCoroutine(Get(this.url));
    }

    private IEnumerator Get(string url)
    {
        UnityWebRequest level = UnityWebRequest.Get(ServerLogic.urlBase + url);

        yield return level.SendWebRequest();

        if (level.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(level.error);
            yield break;
        }

        string text = level.downloadHandler.text;

        if (leaderboard.childCount > 0)
        {
            for (int i = 0; i < leaderboard.childCount; i++)
            {
                Destroy(leaderboard.GetChild(i).gameObject);
            }
        }

        LeaderboardPosition leaderboardPosition = JsonUtility.FromJson<LeaderboardPosition>(text);
        for (int j = 0; j < leaderboardPosition.names.Length; j++)
        {
            Text[] componentsInChildren = Instantiate
            (
                positionObject,
                new Vector3(0f, 2.3f - (float)j / 1.5f, 0f),
                Quaternion.Euler(Vector3.zero),
                leaderboard
            ).GetComponentsInChildren<Text>();

            componentsInChildren[0].text = (j + 1).ToString() + "):";
            componentsInChildren[1].text = leaderboardPosition.names[j];
            componentsInChildren[2].text = leaderboardPosition.cps[j];
        }
        yield break;
    }
}
