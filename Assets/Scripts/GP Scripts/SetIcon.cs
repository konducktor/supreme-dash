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
        sr.sprite = Resources.Load<Sprite>(gameModeName + "/icon" + GlobalData.CurrentCube.ToString());
        sr.color = ColorSelector.ColorFromString(GlobalData.CurrentColor);

        for (int i = 0; i < additions.Length; i++)
        {
            additions[i].color = sr.color;
        }
    }
}
