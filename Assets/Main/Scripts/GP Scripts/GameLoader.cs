using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{

    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] public GameObject finishMenu;
    [SerializeField] public Joystick joystick;

    public static string ExitScene = "Menu";

    private void Start()
    {
        finishMenu.SetActive(false);

        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            joystick.gameObject.SetActive(false);
        }

        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        SpriteRenderer BG = GameObject.Find("BG").GetComponent<SpriteRenderer>();

        AudioSource source = GetComponent<AudioSource>();

        if (EditorLogic.levelName != null)
        {
            SaveLoader.JSONToLevel(SaveLoader.Load(), cam, gameObjects, source);
        }

        EditorLogic.bgColor = cam.backgroundColor;
        BG.color = EditorLogic.bgColor;

        source.Play();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(ExitScene);
            ExitScene = "Menu";
        }
    }

}
