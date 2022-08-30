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

    private string urlBase = "https://beloeozero.ru/ppdsh/";
    private string url;
    private string[] keys;
    private string[] values;

    [SerializeField] private InputField login;
    [SerializeField] private InputField password;
    [SerializeField] private Text currentAccount;

    [SerializeField] private InputField levelName;
    [SerializeField] private Text publishResult;

    [SerializeField] private InputField searchInput;
    [SerializeField] private GameObject searchElement;
    [SerializeField] private Transform searching;

    void Start()
    {
        currentAccount.text = "Current: " + PlayerPrefs.GetString("Login", "");

        if (EditorLogic.levelName != null)
        {
            searchInput.text = EditorLogic.levelName;
        }
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

    public void SendLevel()
    {
        EditorLogic.levelName = levelName.text;
        string path = string.Concat(Application.persistentDataPath + "/", EditorLogic.levelName.ToLower() + ".txt");
        string data = File.ReadAllText(path);

        url = "levelsend.php";
        keys = new string[] { "login", "password", "levelName", "levelData" };
        values = new string[] {
            PlayerPrefs.GetString("Login"),
            PlayerPrefs.GetString("Password"),
            EditorLogic.levelName, data
        };

        StartCoroutine(Send(url, keys, values));
    }

    public void Search(string category)
    {
        url = "levelsearch.php";

        if (searchInput.text == "")
        {
            keys = new string[] { "category", "rating" };
            values = new string[] { category, rating };
        }
        else
        {
            keys = new string[] { "search", "category", "rating" };
            values = new string[] { searchInput.text, category, rating };
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

                SearchData searchResults = JsonUtility.FromJson<SearchData>(result);
                for (int i = 0; i < searchResults.names.Length; i++)
                {

                    GameObject searchResult = Instantiate(
                        searchElement,
                        new Vector3(2f, -2.2f + (searchResults.names.Length - i - 1) * 1.6f, 0),
                        Quaternion.Euler(Vector3.zero),
                        searching
                    );

                    Text[] info = searchResult.GetComponentsInChildren<Text>();
                    info[0].text = searchResults.ids[i];
                    info[1].text = searchResults.names[i];
                    info[2].text = searchResults.authors[i];
                }
                break;
        }
    }

}
