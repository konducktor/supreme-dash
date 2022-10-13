using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AudioListener.volume = (float)GlobalData.Volume;
    }
}
