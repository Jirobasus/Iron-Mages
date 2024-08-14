using UnityEngine;
using UnityEngine.UI;

public class LoadPreferences : MonoBehaviour
{
    public Slider masterVolume;
    public Slider soundFXVolume;
    public Slider musicVolume;
    public Slider sensitivity;
    void Start()
    {
        masterVolume.value = Record.Instance.playerInfo.masterVolume != 0 ? Record.Instance.playerInfo.masterVolume : 1;
        soundFXVolume.value = Record.Instance.playerInfo.soundFXVolume != 0 ? Record.Instance.playerInfo.soundFXVolume : 1;
        musicVolume.value = Record.Instance.playerInfo.musicVolume != 0 ? Record.Instance.playerInfo.musicVolume : 1;
        sensitivity.value = Record.Instance.playerInfo.sensitivity != 0 ? Record.Instance.playerInfo.sensitivity : 5;
    }
}
