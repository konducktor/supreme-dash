using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonL : MonoBehaviour //НИКОГДА В ЖИЗНИ ТАК НЕ ДЕЛАЙТЕ
{
    [SerializeField] private string sceneName;
    [SerializeField] private InputField levelName;

    public void ChangeScene()
    {
        SelectLevel.level = PlayerPrefs.GetInt("Level", 0);
        SceneManager.LoadScene(sceneName);
    }


    public void SetLevel()
    {
        EditorLogic.levelName = levelName.text;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
