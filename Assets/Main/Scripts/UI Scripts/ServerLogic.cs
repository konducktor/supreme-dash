using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class ServerLogic : MonoBehaviour
{
    private class SearchData
    {
        public string[] names, ids, authors, ratings;
    }

    public class Profile
    {
        public string user, color;
        public int icon, gems, rank, doublers, triplers;
    }

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
        currentAccount.text = "Current: " + GlobalData.Login;
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
            GlobalData.Login,
            GlobalData.Password,
            name.text, SaveLoader.LoadFile(name.text)
        };

        StartCoroutine(Send(url, keys, values));
    }

    public void RefreshIcon()
    {
        url = "refreshicon.php";
        keys = new string[] { "login", "password", "icon", "color" };
        values = new string[] {
            GlobalData.Login,
            GlobalData.Password,
            GlobalData.CurrentCube.ToString(),
            GlobalData.CurrentColor
        };

        StartCoroutine(Send(url, keys, values));
    }

    public void GetProfile(string userName)
    {
        url = "getprofile.php";
        keys = new string[] { "username" };
        values = new string[] {
            userName
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

    public void Search(string category, string searchText)
    {
        searchInput.text = searchText;
        Search(category);
    }

    public void Search(string category)
    {
        string search = searchInput.text;
        if (search != lastSearch)
        {
            searchPage = 0;
        }

        lastCategory = category;
        lastSearch = search;
        url = "levelsearch.php";

        if (search == "")
        {
            keys = new string[] { "category", "rating", "page" };
            values = new string[] { category, rating, Convert.ToString(searchPage) };
        }
        else
        {
            keys = new string[] { "search", "category", "page", "rating" };
            values = new string[] { search, category, Convert.ToString(this.searchPage), rating };
        }

        StartCoroutine(Send(url, keys, values));
    }


    private string lastProductID;
    public void Shop(string productID)
    {
        url = "shop.php";
        keys = new string[] { "login", "password", "productID" };
        values = new string[] {
            GlobalData.Login,
            GlobalData.Password,
            productID
        };

        lastProductID = productID;
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

                GlobalData.Login = values[0];
                GlobalData.Password = values[1];
                currentAccount.text = "Done! Current: " + values[0];

                GlobalData.SaveLocal();
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

                GlobalData.Login = values[0];
                GlobalData.Password = values[1];
                currentAccount.text = "Done! Current: " + values[0];

                GlobalData.SaveLocal();
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

                    GameObject level = Instantiate(
                        searchElement,
                        searching
                    );

                    if (Int32.Parse(searchData.ratings[k]) == 1)
                    {
                        level.GetComponentInChildren<Image>().color = ColorSelector.ColorFromString("00FF24");
                    }

                    Text[] info = level.GetComponentsInChildren<Text>();

                    info[1].text = searchData.ids[k];
                    info[0].text = searchData.names[k];
                    info[3].text = searchData.authors[k];
                }
                break;
            case "getprofile.php":
                Profile prof = JsonUtility.FromJson<Profile>(result);

                GameObject obj = Instantiate(Resources.Load<GameObject>("Objects/UserProfile"), GameObject.Find("Canvas").transform);
                obj.GetComponent<UserProfile>().SetProfile(prof.user, prof.icon, prof.color, prof.gems, prof.rank);

                break;
            case "shop.php":
                if (Int32.Parse(result) == 1)
                {
                    break;
                }

                if (lastProductID == "0" && Int32.Parse(result) == 0)
                {
                    GlobalData.Doublers += 200;
                }

                if (lastProductID == "1" && Int32.Parse(result) == 0)
                {
                    GlobalData.Triplers += 150;
                }

                if (lastProductID == "2" && Int32.Parse(result) < 0)
                {
                    GlobalData.UnlockedCubes[Int32.Parse(result) * -1] = true;
                }

                GlobalData.SaveLocal();
                GameObject.Find("ShopStats").GetComponent<ShopStats>().RefreshStats();
                break;
        }
    }
}
