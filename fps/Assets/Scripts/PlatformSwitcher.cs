using UnityEngine;

public class PlatformSwitcher : MonoBehaviour
{
    public bool isDesktop = false;
    public Canvas desktopUI;
    public Canvas mobileUI;
    public Canvas joystickCanvas;
    public GameObject _player;
    public Camera _camera;
    public FPSInputMobile inputMobile;
    public FPSInput inputDesktop;
    public PlatformHandler platformHandler;
    private void Awake()
    {
        platformHandler = FindObjectOfType<PlatformHandler>();
        isDesktop = platformHandler.isDesktop;

        inputMobile = FindObjectOfType<FPSInputMobile>();
        inputDesktop = FindObjectOfType<FPSInput>();
        if (isDesktop)
        {
            _player.GetComponent<MouseLook>().enabled = true;
            _camera.GetComponent<MouseLook>().enabled = true;
            _player.GetComponent<TouchLook>().enabled = false;
            _camera.GetComponent<TouchLook>().enabled = false;
            inputMobile.enabled = false;
            inputDesktop.enabled = true;
            desktopUI.gameObject.SetActive(true);
            mobileUI.gameObject.SetActive(false);
            joystickCanvas.gameObject.SetActive(false);
        }
        else if (!isDesktop)
        {
            _player.GetComponent<MouseLook>().enabled = false;
            _camera.GetComponent<MouseLook>().enabled = false;
            _player.GetComponent<TouchLook>().enabled = true;
            _camera.GetComponent<TouchLook>().enabled = true;
            inputMobile.enabled = true;
            inputDesktop.enabled = false;
            desktopUI.gameObject.SetActive(false);
            mobileUI.gameObject.SetActive(true);
            joystickCanvas.gameObject.SetActive(true);
        }
    }
}
