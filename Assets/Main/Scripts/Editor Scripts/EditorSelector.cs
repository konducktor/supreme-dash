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
        LayerManager.SetOpacity();

        lastSelected.Clear();
        isEditing = false;
    }

    void Update()
    {
        if (!isEditing) return;

        if (!GameInput.Shift() && newSelected.Count > 0)
        {
            lastSelected.Clear();
        }

        SetObjOpacity(EditorLogic.level, 0.2f);
        SetObjOpacity(EditorLogic.level, lastSelected);

        SelectObjects();

        lastSelected.AddRange(newSelected);
        newSelected.Clear();
    }

    void SelectObjects()
    {
        if (GameInput.TouchDown() && !GameInput.IsOverUI())
        {
            start = GameInput.Pointer(0);

            return;
        }

        if (GameInput.Touch())
        {
            if (!GameInput.IsOverUI())
            {
                selectLine.positionCount = 4;
                selectLine.SetPosition(0, new Vector2(GameInput.Pointer(0).x, GameInput.Pointer(0).y));
                selectLine.SetPosition(1, new Vector2(start.x, GameInput.Pointer(0).y));
                selectLine.SetPosition(2, new Vector2(start.x, start.y));
                selectLine.SetPosition(3, new Vector2(GameInput.Pointer(0).x, start.y));

                return;
            }

            selectLine.positionCount = 0;
            return;
        }
        else
        {
            if (GameInput.TouchUp() && !GameInput.IsOverUI())
            {

                end = GameInput.Pointer(0);

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
    }

    IEnumerator Reset()
    {
        yield return null;

        selectLine.positionCount = 0;

        start = Vector3.zero;
        end = Vector3.zero;

        topLeft = Vector3.zero;
        downRight = Vector3.zero;

        yield break;
    }


    public static Color currentObjectColor;
    public void ObjectColor(InputField objectColor)
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

    public void ObjectAlpha(InputField objectAlpha)
    {
        float a = (float)Convert.ToDecimal(objectAlpha.text) / 100f;
        foreach (int index in lastSelected)
        {
            Color color = EditorLogic.level[index].GetComponent<SpriteRenderer>().color;
            color.a = a;
            EditorLogic.level[index].GetComponent<SpriteRenderer>().color = color;

            EditorLogic.objects[index].col.a = a;
        }
    }

    public static Vector3 currentObjectScale;

    public void IndividualObjectScale(InputField individualScale)
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

    public void ObjectScale(InputField objectScale)
    {
        Vector3 center = GetCenter();
        float d = (float)Convert.ToDecimal(objectScale.text);

        foreach (int b in lastSelected)
        {
            Transform transform = EditorLogic.level[b].transform;

            transform.position = center + (transform.position - center) * d;
            transform.localScale *= d;

            currentObjectScale = transform.localScale;
            EditorLogic.objects[b].scale = transform.localScale;
        }

        cursor.transform.localScale = currentObjectScale;

    }


    public void ObjectPositionX(InputField positionX)
    {
        float num = (float)Convert.ToDecimal(positionX.text);
        foreach (int index in EditorSelector.lastSelected)
        {
            Vector3 position = EditorLogic.level[index].transform.position;
            position.x += num;
            EditorLogic.level[index].transform.position = position;
            EditorLogic.SavedObject savedObject = EditorLogic.objects[index];
            savedObject.pos.x = savedObject.pos.x + num;
        }
        positionX.text = "";
    }

    public void ObjectPositionY(InputField positionY)
    {
        float num = (float)Convert.ToDecimal(positionY.text);
        foreach (int index in EditorSelector.lastSelected)
        {
            Vector3 position = EditorLogic.level[index].transform.position;
            position.y += num;
            EditorLogic.level[index].transform.position = position;
            EditorLogic.SavedObject savedObject = EditorLogic.objects[index];
            savedObject.pos.y = savedObject.pos.y + num;
        }
        positionY.text = "";
    }

    public void ObjectLayer(InputField layer)
    {
        int num = Convert.ToInt32(layer.text);
        foreach (int i in lastSelected)
        {
            EditorLogic.level[i].GetComponent<SpriteRenderer>().sortingOrder = num + 100;
            EditorLogic.objects[i].layer = num + 100;
        }
    }

    public void IsDeco(bool value)
    {
        foreach (int index in EditorSelector.lastSelected)
        {
            EditorLogic.objects[index].deco = value;
        }
    }


    void SetObjOpacity(List<GameObject> objects, List<int> IDs)
    {
        foreach (int index in IDs)
        {
            Color color = objects[index].GetComponent<SpriteRenderer>().color;
            color.a = EditorLogic.objects[index].col.a;
            objects[index].GetComponent<SpriteRenderer>().color = color;
        }
    }
    void SetObjOpacity(List<GameObject> objects, float opacity)
    {
        foreach (GameObject e in objects)
        {
            Color col = e.GetComponent<SpriteRenderer>().color;
            col.a = opacity;
            if (e.GetComponent<SpriteRenderer>().sortingOrder == LayerManager.currentLayer)
            {
                col.a = opacity * 3f;
            }
            e.GetComponent<SpriteRenderer>().color = col;
        }
    }

    private Vector3 GetCenter()
    {
        Vector3 a = Vector3.zero;
        foreach (int index in lastSelected)
        {
            a += EditorLogic.level[index].transform.position;
        }
        return a / (float)lastSelected.Count;
    }
}
