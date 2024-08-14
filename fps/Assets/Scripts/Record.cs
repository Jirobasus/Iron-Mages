using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]

public class PlayerInfo
{
    public int waveRecord;
    public float masterVolume;
    public float soundFXVolume;
    public float musicVolume;
    public float sensitivity;
}

public class Record : MonoBehaviour
{
    public PlayerInfo playerInfo;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public static Record Instance;

    private void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            LoadExtern();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(playerInfo);
        SaveExtern(jsonString);
    }

    public void SetPlayerInfo(string value)
    {
        playerInfo = JsonUtility.FromJson<PlayerInfo>(value);
    }
}
