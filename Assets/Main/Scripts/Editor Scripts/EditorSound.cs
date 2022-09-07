using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class EditorSound : MonoBehaviour
{
    [SerializeField] private Text text;
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
        ChangeSong(0);
    }

    public void ChangeSong(int changeBy)
    {
        songID += changeBy;
        text.text = songs[songID];

        source.Stop();
        source.clip = Resources.Load<AudioClip>(Path.Combine("Sounds/", songs[songID]));
        source.Play();

        Resources.UnloadUnusedAssets();
    }
}
