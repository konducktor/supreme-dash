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
        if (GameInput.Delete())
        {
            EditorLogic.level.RemoveAt(objectIndex);
            EditorLogic.objects.RemoveAt(objectIndex);

            Destroy(gameObject);
        }
    }

    void Update() //так кстати тоже делать плохо
    {
        objectIndex = EditorLogic.level.FindIndex(o => o == gameObject);

        bool conditions =
            EditorSelector.topLeft.x <= tf.position.x &&
            EditorSelector.topLeft.y >= tf.position.y &&
            EditorSelector.downRight.x >= tf.position.x &&
            EditorSelector.downRight.y <= tf.position.y &&

            EditorSelector.topLeft != Vector3.zero &&
            EditorSelector.downRight != Vector3.zero &&

            EditorLogic.objects[objectIndex].layer == LayerManager.currentLayer
        ;

        if (conditions)
        {
            EditorSelector.newSelected.Add(objectIndex);
        }
    }
}
