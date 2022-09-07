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
        DirectoryInfo files = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] savedLevels = files.GetFiles().OrderBy(f => f.CreationTime).ToArray();
        Array.Reverse(savedLevels);

        foreach (FileInfo name in savedLevels)
        {

            GameObject newLevel = Instantiate(levelElement, new Vector3(0f, 0f, 0f), Quaternion.Euler(Vector3.zero));
            newLevel.GetComponent<RectTransform>().SetParent(gameObject.transform, false);

            newLevel.GetComponentInChildren<InputField>().text = Path.GetFileNameWithoutExtension(name.Name);
        }
    }
}
