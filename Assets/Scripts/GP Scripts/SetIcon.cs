using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIcon : MonoBehaviour
{
    [SerializeField] private string gameModeName;
    [SerializeField] private SpriteRenderer[] additions;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        string path = gameModeName + "/icon";
        switch (gameModeName)
        {
            case "Cube":
                path += GlobalData.CurrentCube.ToString();
                break;
            case "Ball":
                path += GlobalData.CurrentBall.ToString();
                break;
        }

        sr.sprite = Resources.Load<Sprite>(path);
        sr.color = ColorSelector.ColorFromString(GlobalData.CurrentColor);

        for (int i = 0; i < additions.Length; i++)
        {
            additions[i].color = sr.color;
        }
    }
}
