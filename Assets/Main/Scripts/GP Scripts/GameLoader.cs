using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{

    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] public GameObject finishMenu;
    [SerializeField] public GameObject pausePanel;
    [SerializeField] public GameObject pauseButton;
    [SerializeField] public Joystick joystick;

    public static string ExitScene = "Menu";

    private void Awake()
    {
        Time.timeScale = 1f;
        finishMenu.SetActive(false);

        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        SpriteRenderer BG = GameObject.Find("BG").GetComponent<SpriteRenderer>();

        AudioSource source = GetComponent<AudioSource>();

        if (EditorLogic.levelData == null) EditorLogic.levelData = SaveLoader.LoadFile();
        SaveLoader.JSONToLevel(EditorLogic.levelData, cam, gameObjects, source);

        EditorLogic.bgColor = cam.backgroundColor;
        BG.color = EditorLogic.bgColor;

        source.Play();
    }

    public float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchPause();
    }

    public void SetPaused(bool paused)
    {
        pausePanel.SetActive(paused);
        pauseButton.SetActive(!paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    public void SwitchPause() => SetPaused(!pausePanel.activeSelf);

    public void Continue() => SetPaused(false);

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(ExitScene);
        ExitScene = "Menu";
    }
}
