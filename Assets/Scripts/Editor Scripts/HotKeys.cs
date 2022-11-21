using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeys : MonoBehaviour
{
    [SerializeField] GameObject Main, Exit, Build, Edit, Info;

    private float axis, lastAxis;

    void Update()
    {
        if (Control() && Input.GetKeyDown(KeyCode.S))
        {
            SaveLoader.SaveFile();
            LevelSaved.LevelSavedAnimation();
        }

        if (Control() && Input.GetKeyDown(KeyCode.P))
        {
            EditorLogic.Playtest();
        }

        if (Control() && Input.GetKeyDown(KeyCode.Escape))
        {
            Main.SetActive(false);
            Exit.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            Build.SetActive(true);
            Edit.SetActive(false);
            Info.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            Build.SetActive(false);
            Edit.SetActive(true);
            Info.SetActive(false);
        }


        // axis = Input.GetAxis("Scroll"); // && !Input.GetButton("Shift")
        // if (axis != lastAxis)
        // {
        //     Debug.Log(((int)(axis - lastAxis) + EditorLogic.objectID) % EditorLogic.gameObjects.Length);
        //     EditorLogic.objectID = ((int)(axis - lastAxis) + EditorLogic.objectID) % EditorLogic.gameObjects.Length;
        // }
        // lastAxis = axis;
    }

    bool Control()
    {
        return (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.RightApple) || Input.GetKey(KeyCode.LeftApple));
    }
}
