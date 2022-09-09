using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnly : MonoBehaviour
{
    [SerializeField] bool mobile = false;

    void Start()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
            gameObject.SetActive(mobile);
    }
}
