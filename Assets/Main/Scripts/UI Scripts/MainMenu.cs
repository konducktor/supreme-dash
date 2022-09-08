using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputField levelName;

    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
    }
}
