using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script")]

public class MouseLook : MonoBehaviour
{
    public SceneController sceneController;
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes Axes = RotationAxes.MouseXAndY;

    public float SensitivityX = 9.0f;
    public float SensitivityY = 9.0f;

    public float MinimumY = -45.0f;
    public float MaximumY = 45.0f;

    private float _rotationX = 0f;

    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null )
        {
            body.freezeRotation = true;
        }
    }
    void Update()
    {
        if (!sceneController.isPaused)
        {
            if (Axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * SensitivityX, 0);
            }
            else if (Axes == RotationAxes.MouseY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * SensitivityY;
                _rotationX = Mathf.Clamp(_rotationX, MinimumY, MaximumY);

                transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
            }
            else
            {
                _rotationX -= Input.GetAxis("Mouse Y") * SensitivityY;
                _rotationX = Mathf.Clamp(_rotationX, MinimumY, MaximumY);

                float delta = Input.GetAxis("Mouse X") * SensitivityX;
                float rotationY = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
    }
}
