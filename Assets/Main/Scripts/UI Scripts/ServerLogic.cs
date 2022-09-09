using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class SearchData
{
    public string[] names;
    public string[] ids;
    public string[] authors;

    public SearchData(string[] names, string[] ids, string[] authors)
    {
        this.names = names;
        this.ids = ids;
        this.ids = authors;
    }
}

public class ServerLogic : MonoBehaviour
{
    public static string rating = "1";

    public static string urlBase = "https://beloeozero.ru/ppdsh/";
    private string url;
    private string[] keys;
    private string[] values;

    [SerializeField] private InputField login;
    [SerializeField] private InputField password;
    [SerializeField] private Text currentAccount;

    [SerializeField] private Text publishResult;

    [SerializeField] private InputField searchInput;
    [SerializeField] private GameObject searchElement;
    [SerializeField] private Transform searching;

    private string lastCategory = "ID";
    private string lastSearch;
    private int searchPage;

    void Start()
    {
        rating = "1";
        currentAccount.text = "Current: " + PlayerPrefs.GetString("Login", "");
        searchPage = 0;
    }

    public void Resents()
    {
        Search("ID");
    }

    public void Featured(bool value)
    {
        if (value)
        {
            rating = "1";
            return;
        }
        rating = "0";
    }

    public void Register()
    {
        url = "register.php";
        keys = new string[] { "login", "password" };
        values = new string[] { login.text, password.text };

        StartCoroutine(Send(url, keys, values));
    }

    public void Login()
    {
        url = "login.php";
        keys = new string[] { "login", "password" };
        values = new string[] { login.text, password.text };

        StartCoroutine(Send(url, keys, values));
    }

    public void SendLevel(InputField name)
    {
        url = "levelsend.php";
        keys = new string[] { "login", "password", "levelName", "levelData" };
        values = new string[] {
            PlayerPrefs.GetString("Login"),
            PlayerPrefs.GetString("Password"),
            name.text, SaveLoader.LoadFile(name.text)
        };

        StartCoroutine(Send(url, keys, values));
    }

    public void NextPage()
    {
        searchPage += 5;
        Search(lastCategory);
    }

    public void PreviousPage()
    {
        if (searchPage >= 5)
        {
            searchPage -= 5;
            Search(lastCategory);
        }
    }

    public void Search(string category)
    {
        if (searchInput.text != lastSearch)
        {
            searchPage = 0;
        }

        lastCategory = category;
        lastSearch = searchInput.text;
        url = "levelsearch.php";

        if (searchInput.text == "")
        {
            keys = new string[] { "category", "rating", "page" };
            values = new string[] { category, rating, Convert.ToString(searchPage) };
        }
        else
        {
            keys = new string[] { "search", "category", "page", "rating" };
            values = new string[] { searchInput.text, category, Convert.ToString(this.searchPage), rating };
        }

        StartCoroutine(Send(url, keys, values));
    }


    IEnumerator Send(string url, string[] keys, string[] values)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        for (int i = 0; i < keys.Length; i++)
        {
            form.Add(new MultipartFormDataSection(keys[i], values[i]));
        }

        UnityWebRequest www = UnityWebRequest.Post(urlBase + url, form);

        yield return www.SendWebRequest();


        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(www.error);
            yield break;
        }

        string result = www.downloadHandler.text;

        switch (url)
        {
            case "register.php":
                if (result == "2")
                {
                    currentAccount.text = "Enter everyting, please";
                    break;
                }

                if (result == "1")
                {
                    currentAccount.text = "This name is already taken";
                    break;
                }

                PlayerPrefs.SetString("Login", values[0]);
                PlayerPrefs.SetString("Password", values[1]);
                currentAccount.text = "Done! Current: " + values[0];
                break;
            case "login.php":
                if (result == "2")
                {
                    currentAccount.text = "Enter everyting, please";
                    break;
                }

                if (result == "1")
                {
                    currentAccount.text = "There is no such account";
                    break;
                }

                PlayerPrefs.SetString("Login", values[0]);
                PlayerPrefs.SetString("Password", values[1]);
                currentAccount.text = "Done! Current: " + values[0];
                break;
            case "levelsend.php":
                if (result == "3")
                {
                    publishResult.text = "Login/register to upload";
                    break;
                }

                if (result == "1")
                {
                    publishResult.text = "Your level is updated!";
                    break;
                }

                publishResult.text = "Your level is published!";
                break;
            case "levelsearch.php":
                if (searching.childCount > 0)
                {
                    for (int i = 0; i < searching.childCount; i++) Destroy(searching.GetChild(i).gameObject);
                }

                SearchData searchData = JsonUtility.FromJson<SearchData>(result);
                for (int k = 0; k < searchData.names.Length; k++)
                {

                    Text[] info = Instantiate(
                        searchElement,
                        new Vector3(2f, -2.2f + (float)k * 1.6f, 0f),
                        Quaternion.Euler(Vector3.zero),
                        searching
                    ).GetComponentsInChildren<Text>();

                    info[0].text = searchData.ids[k];
                    info[1].text = searchData.names[k];
                    info[2].text = searchData.authors[k];
                }
                break;
        }
    }

}
