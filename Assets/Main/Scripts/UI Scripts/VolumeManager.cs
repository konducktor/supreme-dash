using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private Slider volumeSlider;
    void Start()
    {
        volumeSlider = GetComponent<Slider>();

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);

        AudioListener.volume = volumeSlider.value;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}
