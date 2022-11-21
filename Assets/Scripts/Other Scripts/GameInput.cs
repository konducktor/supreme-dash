using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    private static bool isSnap = false;
    private static bool isDelete = false;

    private static bool isRotating = false;
    private static float rotationAxis = 0;
    private static float scrollAxis = 0;

    private static int maxTouches = 0;

    private static Joystick joystick;
    private static float lastDist;
    private static int isSelecting;
    private Camera editorCamera;

    private void Start()
    {
        isSnap = false;
        isDelete = false;

        if (GameObject.Find("Main").GetComponent<GameLoader>())
        {
            joystick = GameObject.Find("Main").GetComponent<GameLoader>().joystick;
        }

        editorCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


    public static Vector3 mousePos;
    public static Vector3 unsnapMousePos;
    private void Update()
    {
        unsnapMousePos = editorCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = editorCamera.ScreenToWorldPoint(Input.mousePosition);

        mousePos.x = GlobalControl.Round(mousePos.x, 4);
        mousePos.y = GlobalControl.Round(mousePos.y, 4);

        mousePos.z = 0;
        unsnapMousePos.z = 0;
    }


    //local
    private static Vector3 GlobalPosition(Vector2 pos)
    {
        Vector3 output = Camera.main.ScreenToWorldPoint(pos);
        output.z = 0f;

        if (isSnap)
        {
            output = GlobalControl.RoundVector(output, 0);
        }
        return output;
    }


    //buttons
    public void SetSnap(bool value)
    {
        isSnap = value;
    }

    public void SetDelete(bool value)
    {
        isDelete = value;
    }

    public void SetRotate(int axis)
    {
        rotationAxis = (float)axis;
        isRotating = true;
    }
    public void EndRotate()
    {
        rotationAxis = 0;
        isRotating = false;
    }

    //public
    public static bool IsOverUI()
    {
        if (Input.touchCount == 0) return EventSystem.current.IsPointerOverGameObject();

        foreach (Touch touch in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return true;
            }
        }

        return false;
    }

    //input
    public static bool Shift()
    {
        return Input.GetButton("Shift") || isSnap;
    }

    public static bool Rotate()
    {
        if ((isRotating || Input.GetButtonDown("Rotate")))
        {
            isRotating = false;
            return true;
        }

        return isRotating || (Input.GetButton("Rotate") && Shift());
    }

    public static float RotationAxis()
    {
        return Input.GetAxis("Rotate") + rotationAxis;
    }

    public static bool Build()
    {
        if (Input.touchCount > maxTouches)
        {
            maxTouches = Input.touchCount;
        }
        else if (Input.touchCount == 0)
        {
            maxTouches = 0;
        }
        if (Input.touchCount == 1 && Input.GetTouch(0).phase != TouchPhase.Ended && maxTouches <= 1)
        {
            return !IsOverUI() && !EditorSelector.isEditing && !isDelete;
        }
        return Input.GetMouseButton(0) && Input.touchCount <= 0 && !IsOverUI() && !EditorSelector.isEditing;
    }

    public static Vector3 Pointer(int id = 0)
    {
        if (Input.touchCount > 0)
        {
            return GlobalPosition(Input.GetTouch(id).position);
        }

        return mousePos;
    }

    public static bool Delete()
    {
        if (Input.touchCount == 1)
        {
            return !IsOverUI() && !EditorSelector.isEditing && !ContinueMove() && isDelete;
        }
        return Input.GetMouseButton(1) && Input.touchCount <= 0 && !IsOverUI() && !EditorSelector.isEditing && !ContinueMove();
    }

    public static bool StartMove()
    {
        if (Input.touchCount == 2)
        {
            lastDist = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                return true;
            }
        }

        return Input.GetMouseButtonDown(2);
    }

    public static bool ContinueMove()
    {
        if (Input.GetMouseButton(2) || Input.touchCount == 2)
        {
            return true;
        }

        return false;
    }

    public static float ScrollAxis()
    {
        if (Input.touchCount == 2)
        {
            float num = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            scrollAxis = num - lastDist;
            lastDist = num;
            return scrollAxis / 100f;
        }
        return Input.GetAxis("Scroll");
    }



    public static bool TouchDown()
    {
        if (Input.touchCount > 0)
        {
            isSelecting = 1;
            return Input.GetTouch(0).phase == TouchPhase.Began && !IsOverUI();
        }
        return Input.GetMouseButtonDown(0);
    }

    public static bool Touch()
    {
        return Input.GetMouseButton(0) || Input.touchCount > 0;
    }

    public static bool TouchUp()
    {
        if (isSelecting == 1)
        {
            isSelecting = 0;
            return true;
        }
        return Input.GetMouseButtonUp(0);
    }

    public static float HorizontalConstrolls()
    {
        if (Input.GetAxis("Horizontal") != 0f)
        {
            return Input.GetAxis("Horizontal");
        }
        return joystick.Horizontal;
    }

    public static bool IsVerticalControlls()
    {
        foreach (Touch touch in Input.touches)
        {
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return true;
            }
        }
        return (Input.GetButton("Vertical") && Input.GetAxis("Vertical") > 0f) || Input.GetButton("Jump");
    }

    public static bool ButtonDown()
    {
        foreach (Touch touch in Input.touches)
        {
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return touch.phase == TouchPhase.Began;
            }
        }
        return (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0f) || Input.GetButtonDown("Jump");
    }
}