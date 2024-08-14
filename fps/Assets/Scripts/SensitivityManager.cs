using UnityEngine;

public class SensitivityManager : MonoBehaviour
{
    public MouseLook mouseLookPlayer;
    public MouseLook mouseLookCamera;
    public TouchLook touchLookPlayer;
    public TouchLook touchLookCamera;

    public void ChangeSensitivity(float level)
    {
        mouseLookPlayer.SensitivityX = level;
        mouseLookPlayer.SensitivityY = level;
        mouseLookCamera.SensitivityX = level;
        mouseLookCamera.SensitivityY = level;
        touchLookPlayer.SwipeSensitivity = level / 20f;
        touchLookCamera.SwipeSensitivity = level / 20f;
    }
}
