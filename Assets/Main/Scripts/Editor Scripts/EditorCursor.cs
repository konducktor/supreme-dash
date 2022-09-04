using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCursor : MonoBehaviour
{
    [SerializeField] private Camera editorCamera;
    [SerializeField] private Sprite[] sprites;

    public static Vector3 mousePos;

    public static Vector3 objRotation;
    public static Vector3 currentRotation;

    void Start()
    {
        objRotation = Vector3.zero;
        currentRotation = Vector3.zero;
    }

    void Update()
    {

        mousePos = editorCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = Round(mousePos.x, 4);
        mousePos.y = Round(mousePos.y, 4);
        mousePos.z = 0;

        currentRotation = transform.eulerAngles;

        if (!EditorSelector.isEditing) Controlls();

        transform.eulerAngles = currentRotation;
        objRotation = currentRotation;

        GetComponent<SpriteRenderer>().sprite = sprites[EditorLogic.objectID];
        transform.position = mousePos;
    }

    private void Controlls()
    {
        if (GameInput.Shift() && !GameInput.Rotate() && !EditorSelector.isEditing)
        {
            mousePos.x = Round(mousePos.x, 0);
            mousePos.y = Round(mousePos.y, 0);
        }

        if (GameInput.Rotate() && !GameInput.Shift())
        {
            currentRotation.z = Round(currentRotation.z / 15f, 0) * 15f;

            currentRotation += new Vector3(0, 0, 15f) * GameInput.RotationAxis();

        }

        if (GameInput.Shift() && GameInput.Rotate())
        {
            currentRotation += new Vector3(0, 0, 0.5f) * GameInput.RotationAxis();
        }
    }

    public static float Round(float num, int divide)
    {
        return decimal.ToSingle(Math.Round((decimal)num, divide));
    }
}
