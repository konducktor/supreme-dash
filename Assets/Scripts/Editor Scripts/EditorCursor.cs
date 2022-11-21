using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCursor : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    public static Vector3 objRotation, currentRotation, currentPosition;

    void Start()
    {
        objRotation = Vector3.zero;
        currentRotation = Vector3.zero;
    }

    void Update()
    {
        currentPosition = GameInput.mousePos;
        currentRotation = transform.eulerAngles;

        if (!EditorSelector.isEditing) Controlls();

        transform.position = currentPosition;
        transform.eulerAngles = currentRotation;
        objRotation = currentRotation;

        GetComponent<SpriteRenderer>().sprite = sprites[EditorLogic.objectID];
    }

    private void Controlls()
    {
        if (GameInput.Shift() && !GameInput.Rotate() && !EditorSelector.isEditing)
        {
            currentPosition.x = GlobalControl.Round(currentPosition.x, 0);
            currentPosition.y = GlobalControl.Round(currentPosition.y, 0);
        }

        if (GameInput.Rotate() && !GameInput.Shift())
        {
            currentRotation.z = GlobalControl.Round(currentRotation.z / 15f, 0) * 15f;

            currentRotation += new Vector3(0, 0, 15f) * GameInput.RotationAxis();

        }

        if (GameInput.Shift() && GameInput.Rotate())
        {
            currentRotation += new Vector3(0, 0, 0.5f) * GameInput.RotationAxis();
        }
    }
}
