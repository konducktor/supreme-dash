using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjID : MonoBehaviour
{
    [SerializeField] int newID;

    public void ChangeID(int ID)
    {
        EditorLogic.objectID = ID;
    }
}
