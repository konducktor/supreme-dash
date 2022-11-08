using System;
using System.IO;
using UnityEngine;

public class RandomMenuLoop : MonoBehaviour
{
    public static string[] songs = new string[] {
        "Affinity", "Ecstatic", "Feel-Alone"
    };

    AudioSource source;


    void Start()
    {
        System.Random rand = new System.Random();

        source = GetComponent<AudioSource>();

        source.clip = Resources.Load<AudioClip>(Path.Combine("MenuLoops", songs[rand.Next(0, songs.Length)]));
        source.Play();

        Resources.UnloadUnusedAssets();
    }
}
