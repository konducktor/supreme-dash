using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Toggle stats;
    void Start()
    {
        stats.isOn = GlobalData.AdvancedStats;
        AudioListener.volume = (float)GlobalData.Volume;
    }
}
