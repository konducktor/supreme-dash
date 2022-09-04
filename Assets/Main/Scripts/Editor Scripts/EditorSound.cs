using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class EditorSound : MonoBehaviour
{
    [SerializeField] private InputField songInput;
    private AudioSource source;

    public static int songID;

    public static string[] songs = new string[] {
        "Peaceful",
        "Chromedry",
        "Inquiry",
        "Coherent",
        "Endemic",
        "Presence",
        "Absence",
        "Ascension",
        "Inveterate",
        "Enquiry",
        "Eternity",
        "Mental Disorder"
    };

    void Awake()
    {
        songID = 0;
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        source.Play();
    }

    public void Song()
    {
        int.TryParse(songInput.text, out int result);

        source.Stop();
        source.clip = Resources.Load<AudioClip>(Path.Combine("Sounds/", songs[result]));
        source.Play();

        songID = result;
        Resources.UnloadUnusedAssets();
    }
}
