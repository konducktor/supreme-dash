using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjID : MonoBehaviour
{
    public void ChangeID(int ID)
    {
        EditorLogic.objectID = ID;
    }
}
