using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private Slider volumeSlider;
    void Start()
    {
        volumeSlider = GetComponent<Slider>();

        volumeSlider.value = (float)GlobalData.Volume;

        AudioListener.volume = volumeSlider.value;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        GlobalData.Volume = volumeSlider.value;
    }
}
