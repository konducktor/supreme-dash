using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelElement;

    void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);

        DirectoryInfo files = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] savedLevels = files.GetFiles().OrderBy(f => f.CreationTime).ToArray();
        Array.Reverse(savedLevels);

        foreach (FileInfo name in savedLevels)
        {

            if (name.Extension != ".txt") continue;

            GameObject newLevel = Instantiate(levelElement, new Vector3(0f, 0f, 0f), Quaternion.Euler(Vector3.zero));
            newLevel.GetComponent<RectTransform>().SetParent(gameObject.transform, false);

            newLevel.GetComponentInChildren<InputField>().text = Path.GetFileNameWithoutExtension(name.Name);
        }
    }

    public void Open()
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
        {
            FileName = Application.persistentDataPath,
            UseShellExecute = true,
            Verb = "open"
        });
    }
}
