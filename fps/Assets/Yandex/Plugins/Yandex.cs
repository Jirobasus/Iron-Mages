using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.AudioSettings;

public class Yandex : MonoBehaviour
{
    public AudioSource arenaSoundtrack;
    public SceneController sceneController;

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void ShowMenuAd();

    [DllImport("__Internal")]
    private static extern void ShowRestartAd();

    public void RateGameButton()
    {
        RateGame();
    }

    public void PlayFullscreenAdMenu()
    {
        ShowMenuAd();
    }

    public void PlayFullscreenAdRestart()
    {
        ShowRestartAd();
    }
}
