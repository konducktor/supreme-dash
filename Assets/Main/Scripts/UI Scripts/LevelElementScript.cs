using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelElementScript : MonoBehaviour
{
    private InputField nameText;
    private string currentName;

    void Start()
    {
        nameText = GetComponentInChildren<InputField>();
        currentName = nameText.text;
    }

    public void SetName()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, nameText.text.ToLower()) + ".txt");
        File.Move(Path.Combine(Application.persistentDataPath, currentName.ToLower()) + ".txt", Path.Combine(Application.persistentDataPath, nameText.text.ToLower()) + ".txt");

        currentName = nameText.text;
    }
}
