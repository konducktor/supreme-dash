using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour
{
    [SerializeField] private int objectIndex = 0;
    private Transform tf;

    void Start()
    {
        tf = GetComponent<Transform>();
    }

    void OnMouseOver()
    {
        objectIndex = EditorLogic.level.FindIndex(o => o == gameObject);
        if (EditorLogic.objects[objectIndex].layer == LayerManager.currentLayer && GameInput.Delete())
        {
            EditorLogic.level.RemoveAt(objectIndex);
            EditorLogic.objects.RemoveAt(objectIndex);

            Destroy(gameObject);
        }
    }
}
