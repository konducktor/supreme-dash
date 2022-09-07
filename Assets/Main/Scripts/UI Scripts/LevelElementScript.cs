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
        File.Delete(Path.Combine(Application.persistentDataPath, nameText.text) + ".txt");
        File.Move(Path.Combine(Application.persistentDataPath, currentName) + ".txt", Path.Combine(Application.persistentDataPath, nameText.text) + ".txt");

        currentName = nameText.text;
    }
}
