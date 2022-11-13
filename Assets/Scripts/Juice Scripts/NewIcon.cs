using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewIcon : MonoBehaviour
{
    public int iconID;
    public Color backgroundColor;

    [SerializeField] private Image background, icon;

    private bool canClose = false;

    void Start()
    {

        // backgroundColor = icons.colors[icons.iconCategories[iconID]];
        // backgroundColor.a = 1f;

        background.color = backgroundColor;
        icon.sprite = Resources.Load<Sprite>("Cube/icon" + iconID.ToString());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClose)
        {
            Destroy(gameObject);
        }
    }

    public void CanClose()
    {
        canClose = true;
    }
}
