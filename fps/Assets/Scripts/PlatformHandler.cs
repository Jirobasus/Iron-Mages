using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]

public class PlatformHandler : MonoBehaviour
{
    
    [DllImport("__Internal")]
    private static extern void LoadDevice();

    public static PlatformHandler Instance;
    public bool isDesktop;
    void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            LoadPlatform();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadPlatform()
    {
        LoadDevice();
    }

    public void SetPlatformDesktop()
    {
        isDesktop = true;
    }

    public void SetPlatformMobile()
    {
        isDesktop = false;
    }
}
