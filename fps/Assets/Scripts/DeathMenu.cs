using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ContinueForAdExtern();

    public Canvas deathMenu;
    public SceneController sceneController;
    public Health health;
    public AudioSource arenaSoundtrack;
    public bool watchedAdToContinue = false;
    public PlatformSwitcher platformSwitcher;

    private void Start()
    {
        health = FindObjectOfType<Health>();
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Arena");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        sceneController.isInGame = true;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ContinueForAdButton()
    {
        ContinueForAdExtern();
    }

    public void ContinueForAd()
    {
        if (platformSwitcher.isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        watchedAdToContinue = true;
        Time.timeScale = 1f;
        sceneController.isPaused = false;
        health.image.fillAmount = 1f;
        health.isDeathScreenLoaded = false;
        arenaSoundtrack.Play();
        deathMenu.gameObject.SetActive(false);
        sceneController.isInGame = true;
    }
}
