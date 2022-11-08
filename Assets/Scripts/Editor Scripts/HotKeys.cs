using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeys : MonoBehaviour
{
    [SerializeField] GameObject Main, Playtest, Exit, Build, Edit, Info;

    void Update()
    {
        if (Control() && Input.GetKeyDown(KeyCode.S))
        {
            SaveLoader.SaveFile();
        }

        if (Control() && Input.GetKeyDown(KeyCode.P))
        {
            Main.SetActive(false);
            Playtest.SetActive(true);
        }

        if (Control() && Input.GetKeyDown(KeyCode.Escape))
        {
            Main.SetActive(false);
            Exit.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Build.SetActive(true);
            Edit.SetActive(false);
            Info.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Build.SetActive(false);
            Edit.SetActive(true);
            Info.SetActive(false);
        }
    }

    bool Control()
    {
        return (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.RightApple) || Input.GetKey(KeyCode.LeftApple));
    }
}
