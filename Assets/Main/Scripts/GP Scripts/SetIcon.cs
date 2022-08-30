using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIcon : MonoBehaviour
{
    [SerializeField] private string gameModeName;
    [SerializeField] private Sprite[] icons;
    [SerializeField] private SpriteRenderer[] additions;

    private SpriteRenderer sr;
    private int curIcon;
    private string curColor;


    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (icons.Length != 0)
        {
            curIcon = PlayerPrefs.GetInt(gameModeName, 0);
            sr.sprite = icons[curIcon];
        }

        curColor = PlayerPrefs.GetString("Color", "FFFFFF");
        sr.color = ColorSelector.ColorFromString(curColor);

        for (int i = 0; i < additions.Length; i++)
        {
            additions[i].color = sr.color;
        }
    }
}
