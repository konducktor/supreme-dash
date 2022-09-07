using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour //НИКОГДА В ЖИЗНИ ТАК НЕ ДЕЛАЙТЕ
{

    public void ChangeScene(string sceneName)
    {
        SelectLevel.level = PlayerPrefs.GetInt("Level", 0);
        SceneManager.LoadScene(sceneName);
    }


    public void SetLevel(InputField levelName)
    {
        EditorLogic.levelName = levelName.text;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Publish(InputField levelName)
    {
        GameObject.Find("Main Camera").GetComponent<ServerLogic>().SendLevel(levelName);
    }
}
