using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnly : MonoBehaviour
{
    void Start()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
            gameObject.SetActive(false);
    }
}
