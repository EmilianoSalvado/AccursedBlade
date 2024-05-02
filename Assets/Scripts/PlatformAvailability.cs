using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAvailability : MonoBehaviour
{
    [SerializeField] Platform _platform;

    private void Start()
    {
        PlatformOff();
    }

    public void PlatformOn()
    {
        _platform.enabled = true;
    }

    public void PlatformOff()
    {
        _platform.enabled = false;
    }
}