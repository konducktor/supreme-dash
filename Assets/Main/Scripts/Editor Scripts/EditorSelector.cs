using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EditorSelector : MonoBehaviour
{
    public static List<int> newSelected = new List<int>();
    public static List<int> lastSelected = new List<int>();

    private GameObject cursor;
    public static bool isEditing = false;

    private Vector3 start, end;
    public static Vector3 topLeft, downRight;
    private LineRenderer selectLine;

    void Awake()
    {
        cursor = GameObject.Find("Cursor");

        currentObjectColor = new Color(1f, 1f, 1f);
        currentObjectScale = new Vector3(1f, 1f, 1f);

        selectLine = GetComponent<LineRenderer>();
    }

    public void StartEdit()
    {
        isEditing = true;
    }

    public void EndEdit()
    {
        SetObjOpacity(EditorLogic.level, 1f);
        lastSelected.Clear();
        isEditing = false;
    }

    void Update()
    {
        if (!isEditing) return;

        if (!Input.GetButton("Shift") && newSelected.Count > 0)
        {
            lastSelected.Clear();
        }

        SetObjOpacity(EditorLogic.level, 0.3f);
        SetObjOpacity(EditorLogic.level, lastSelected, 1f);

        SelectObjects();

        lastSelected.AddRange(newSelected);
        newSelected.Clear();
    }

    void SelectObjects()
    {
        if (Input.GetMouseButtonDown(0) && !GameInput.IsOverUI())
        {
            start = EditorCursor.mousePos;
            selectLine.positionCount = 4;

            return;
        }

        if (Input.GetMouseButton(0))
        {
            selectLine.SetPosition(0, new Vector2(EditorCursor.mousePos.x, EditorCursor.mousePos.y));
            selectLine.SetPosition(1, new Vector2(start.x, EditorCursor.mousePos.y));
            selectLine.SetPosition(2, new Vector2(start.x, start.y));
            selectLine.SetPosition(3, new Vector2(EditorCursor.mousePos.x, start.y));

            return;
        }

        if (Input.GetMouseButtonUp(0) && !GameInput.IsOverUI())
        {
            selectLine.positionCount = 0;

            end = EditorCursor.mousePos;

            topLeft = new Vector3(start.x, start.y, 0);
            downRight = new Vector3(end.x, end.y, 0);

            if (start.x > end.x)
            {
                topLeft.x = end.x;
                downRight.x = start.x;
            }
            if (start.y < end.y)
            {
                topLeft.y = end.y;
                downRight.y = start.y;
            }


            StartCoroutine(Reset());

            return;
        }
    }

    IEnumerator Reset()
    {
        yield return null;

        start = Vector3.zero;
        end = Vector3.zero;
        topLeft = Vector3.zero;
        downRight = Vector3.zero;
    }


    public static Color currentObjectColor;
    [SerializeField] private InputField objectColor;
    public void ObjectColor()
    {
        foreach (int i in lastSelected)
        {
            SpriteRenderer sr = EditorLogic.level[i].GetComponent<SpriteRenderer>();

            if (objectColor.text != "")
            {
                currentObjectColor = ColorSelector.ColorFromString(objectColor.text);
                sr.color = currentObjectColor;
                EditorLogic.objects[i].col = sr.color;
            }
            else
            {
                sr.color = Color.white;
            }

            Color cursorColor = cursor.GetComponent<SpriteRenderer>().color;

            cursorColor = sr.color;
            cursorColor.a = 0.5f;

            cursor.GetComponent<SpriteRenderer>().color = cursorColor;
        }
    }

    public static Vector3 currentObjectScale;
    [SerializeField] private InputField individualScale;

    public void IndividualObjectScale()
    {
        currentObjectScale = new Vector3(
                (float)Convert.ToDecimal(individualScale.text),
                (float)Convert.ToDecimal(individualScale.text), 1f
        );

        foreach (int i in lastSelected)
        {
            EditorLogic.level[i].transform.localScale = currentObjectScale;
            EditorLogic.objects[i].scale = currentObjectScale;
        }

        cursor.transform.localScale = currentObjectScale;
    }

    [SerializeField] private InputField objectScale;
    public void ObjectScale()
    {
        Vector3 center = new Vector3(0, 0, 0);

        foreach (int a in lastSelected)
        {
            center += EditorLogic.level[a].transform.position;
        }
        center = center / lastSelected.Count;

        foreach (int b in lastSelected)
        {
            Transform obj = EditorLogic.level[b].transform;

            obj.position = center + ((obj.position - center) * (float)Convert.ToDecimal(objectScale.text));

            EditorLogic.level[b].transform.localScale *= (float)Convert.ToDecimal(objectScale.text);
            currentObjectScale = EditorLogic.level[b].transform.localScale;
            EditorLogic.objects[b].scale = currentObjectScale;

            cursor.transform.localScale = currentObjectScale;
        }

    }

    void SetObjOpacity(List<GameObject> objects, List<int> IDs, float opacity)
    {

        foreach (int e in IDs)
        {
            Color col = objects[e].GetComponent<SpriteRenderer>().color;
            col.a = opacity;
            objects[e].GetComponent<SpriteRenderer>().color = col;
        }
    }
    void SetObjOpacity(List<GameObject> objects, float opacity)
    {
        foreach (GameObject e in objects)
        {
            Color col = e.GetComponent<SpriteRenderer>().color;
            col.a = opacity;
            e.GetComponent<SpriteRenderer>().color = col;
        }
    }
}
