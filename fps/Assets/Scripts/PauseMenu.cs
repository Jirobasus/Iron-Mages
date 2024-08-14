using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas UI;
    public Canvas mobileUI;
    public Canvas joystickCanvas;
    public SceneController sceneController;
    public AudioSource arenaSoundtrack;
    public Camera _camera;
    public GameObject pauseMenu;
    public GameObject soundPreferencesMenu;
    public PlatformSwitcher platformSwitcher;
    public void ResumeGame()
    {
        if (platformSwitcher.isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UI.gameObject.SetActive(true);
        }
        else
        {
            mobileUI.gameObject.SetActive(true);
            joystickCanvas.gameObject.SetActive(true);
        }
        _camera.GetComponent<AudioListener>().enabled = true;
        arenaSoundtrack.Play();
        Time.timeScale = 1;
        sceneController.isPaused = false;
        this.gameObject.SetActive(false);
    }
    public void QuitToMainMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void EnterSoundPreferencesMenu()
    {
        pauseMenu.gameObject.SetActive(false);
        soundPreferencesMenu.gameObject.SetActive(true);
    }

    public void ExitSoundPreferencesMenu()
    {
        soundPreferencesMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }
}
