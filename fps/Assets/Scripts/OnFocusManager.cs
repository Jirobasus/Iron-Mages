using UnityEngine;

public class OnFocusManager : MonoBehaviour
{
    public SceneController sceneController;
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            sceneController.PauseGame();
        }
    }
}
