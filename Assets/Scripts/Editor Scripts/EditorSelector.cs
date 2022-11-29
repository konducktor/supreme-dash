using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EditorSelector : MonoBehaviour
{
    public static List<int> selected = new List<int>();

    private GameObject cursor;
    public static bool isEditing = false;

    private Vector3 start;
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
        start = GameInput.Pointer(0);

        SetObjOpacity(EditorLogic.level, 0.2f);
        SetObjOpacity(EditorLogic.level, selected);
    }

    public void EndEdit()
    {
        LayerManager.SetOpacity();

        selected.Clear();
        isEditing = false;
    }

    void Update()
    {
        if (!isEditing) return;

        if (!GameInput.Touch() && !GameInput.TouchUp())
        {
            start = GameInput.unsnapMousePos;

            return;
        }

        SelectObjects();
    }

    void SelectObjects()
    {
        Vector3 mousePos = GameInput.Pointer(0);

        if (GameInput.TouchDown() && !GameInput.IsOverUI())
        {
            start = mousePos;

            return;
        }

        if (GameInput.Touch() && start != mousePos)
        {
            selectLine.positionCount = 4;
            selectLine.SetPosition(0, new Vector2(mousePos.x, mousePos.y));
            selectLine.SetPosition(1, new Vector2(start.x, mousePos.y));
            selectLine.SetPosition(2, new Vector2(start.x, start.y));
            selectLine.SetPosition(3, new Vector2(mousePos.x, start.y));

            return;
        }

        if (GameInput.TouchUp() && start != mousePos) // && !GameInput.IsOverUI()
        {
            Debug.Log(start);
            Debug.Log(mousePos);

            Vector3 end = mousePos;

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

            if (!GameInput.Shift() && selected.Count > 0 && !GameInput.IsOverUI())
            {
                selected.Clear();
            }

            selected.AddRange(FindSelected(topLeft, downRight));
            selectLine.positionCount = 0;

            SetObjOpacity(EditorLogic.level, 0.2f);
            SetObjOpacity(EditorLogic.level, selected);
        }
    }


    private IEnumerable<int> FindSelected(Vector3 topLeft, Vector3 downRight)
    {
        var indexes = EditorLogic.objects.Where(block =>
            topLeft.x <= block.pos.x && topLeft.y >= block.pos.y &&
            downRight.x >= block.pos.x && downRight.y <= block.pos.y &&

            block.layer == LayerManager.currentLayer
        ).Select(block => EditorLogic.objects.IndexOf(block));

        return indexes;
    }

    public void CopyPaste()
    {
        foreach (int i in selected)
        {
            EditorLogic.SavedObject saveObject = EditorLogic.objects[i];

            EditorLogic.level.Add(Instantiate(EditorLogic.level[i]));
            EditorLogic.objects.Add(new EditorLogic.SavedObject(
                saveObject.id, saveObject.pos,
                saveObject.rot, saveObject.col,
                saveObject.scale, saveObject.layer,
                saveObject.deco
            ));
        }
    }

    public void Delete()
    {
        for (int i = selected.Count - 1; i >= 0; i--)
        {
            Destroy(EditorLogic.level[selected[i]]);

            EditorLogic.level.RemoveAt(selected[i]);
            EditorLogic.objects.RemoveAt(selected[i]);

            selected.RemoveAt(i);
        }
    }


    public static Color currentObjectColor;
    public void ObjectColor(InputField objectColor)
    {
        foreach (int i in selected)
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
        foreach (int index in selected)
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

        foreach (int i in selected)
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

        foreach (int b in selected)
        {
            Transform transform = EditorLogic.level[b].transform;

            transform.position = center + (transform.position - center) * d;
            transform.localScale *= d;

            currentObjectScale = transform.localScale;
            EditorLogic.objects[b].scale = transform.localScale;
        }

        cursor.transform.localScale = currentObjectScale;
        objectScale.text = "";
    }


    public void ObjectPositionX(InputField positionX)
    {
        float num = (float)Convert.ToDecimal(positionX.text);
        foreach (int index in EditorSelector.selected)
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
        foreach (int index in EditorSelector.selected)
        {
            Vector3 position = EditorLogic.level[index].transform.position;
            position.y += num;
            EditorLogic.level[index].transform.position = position;
            EditorLogic.SavedObject savedObject = EditorLogic.objects[index];
            savedObject.pos.y = savedObject.pos.y + num;
        }
        positionY.text = "";
    }

    public void ObjectRotation(InputField rotation)
    {
        Vector3 center = GetCenter();
        float num = (float)Convert.ToDecimal(rotation.text);

        foreach (int index in selected)
        {
            EditorLogic.level[index].transform.RotateAround(center, Vector3.back, num);

            EditorLogic.objects[index].rot = EditorLogic.level[index].transform.eulerAngles;
            EditorLogic.objects[index].pos = EditorLogic.level[index].transform.position;
        }

        cursor.transform.eulerAngles = new Vector3(0f, 0f, num);
        rotation.text = "";
    }

    public void IndividualObjectRotation(InputField individualRotation)
    {
        float num = (float)Convert.ToDecimal(individualRotation.text);

        foreach (int index in selected)
        {
            EditorLogic.level[index].transform.eulerAngles = new Vector3(0f, 0f, num);
            EditorLogic.objects[index].rot = new Vector3(0f, 0f, num);
        }

        cursor.transform.eulerAngles = new Vector3(0f, 0f, num);
    }

    public void ObjectLayer(InputField layer)
    {
        int num = Convert.ToInt32(layer.text);
        foreach (int i in selected)
        {
            EditorLogic.level[i].GetComponent<SpriteRenderer>().sortingOrder = num + 100;
            EditorLogic.objects[i].layer = num + 100;

            var csr = EditorLogic.level[i].GetComponentsInChildren<SpriteRenderer>();
            if (csr.Length > 1) csr[1].sortingOrder = num + 101;
        }
    }

    public void IsDeco(bool value)
    {
        foreach (int index in EditorSelector.selected)
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
        foreach (int index in selected)
        {
            a += EditorLogic.level[index].transform.position;
        }
        return a / (float)selected.Count;
    }
}
