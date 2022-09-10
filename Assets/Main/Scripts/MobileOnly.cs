using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnly : MonoBehaviour
{
    [SerializeField] bool forMobile = true;

    void Start()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
            gameObject.SetActive(!forMobile);
    }
}
