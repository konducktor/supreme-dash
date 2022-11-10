using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour //НИКОГДА В ЖИЗНИ ТАК НЕ ДЕЛАЙТЕ
{

    public void ChangeScene(string sceneName)
    {
        SceneTransition.ChangeScene(sceneName);
    }


    public void SetLevel(InputField levelName)
    {
        EditorLogic.levelData = SaveLoader.LoadFile(levelName.text);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Publish(InputField levelName)
    {
        GameObject.Find("Main Camera").GetComponent<ServerLogic>().SendLevel(levelName);
    }

    public void Save()
    {
        GlobalData.SaveLocal();
    }

    public void GetPlayerProfile(GameObject register)
    {
        if (GlobalData.Login == null)
        {
            register.SetActive(true);
            return;
        }

        GameObject.Find("Main Camera").GetComponent<ServerLogic>().GetProfile(GlobalData.Login);
    }

    public void ToggleStats(bool v)
    {
        GlobalData.AdvancedStats = v;
    }
}
