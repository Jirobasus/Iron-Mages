using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class TouchLook : MonoBehaviour
{
    public SceneController sceneController;
    public FixedJoystick joystick;
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes Axes = RotationAxes.MouseXAndY;

    public float SwipeSensitivity = 0.1f;

    public float MinimumY = -45.0f;
    public float MaximumY = 45.0f;

    public Rect cameraControlZone;

    private float _rotationX = 0f;
    private float _rotationY = 0f;

    private Vector2 touchStartPos;

    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
        float joystickSize = joystick.GetComponent<RectTransform>().rect.width;

        float zoneX = 0;
        float zoneY = 0;
        float zoneWidth = joystickSize * 4;
        float zoneHeight = joystickSize * 3;

        cameraControlZone = new Rect(zoneX, zoneY, zoneWidth, zoneHeight);
    }

    void Update()
    {
        if (!sceneController.isPaused)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchStartPos = touch.position;
                        break;

                    case TouchPhase.Moved:
                        if (!cameraControlZone.Contains(touch.position))
                        {
                            Vector2 delta = touch.position - touchStartPos;
                            RotateCamera(delta.x * SwipeSensitivity, delta.y * SwipeSensitivity); // Use X and Y-axis delta for camera rotation
                            touchStartPos = touch.position; // Update start position for next frame
                        }
                        break;
                }
            }
        }
    }

    void RotateCamera(float deltaX, float deltaY)
    {
        if (Axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, deltaX, 0);
        }
        else if (Axes == RotationAxes.MouseY)
        {
            _rotationX -= deltaY;
            _rotationX = Mathf.Clamp(_rotationX, MinimumY, MaximumY);

            transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
        }
        else
        {
            _rotationX -= deltaY;
            _rotationX = Mathf.Clamp(_rotationX, MinimumY, MaximumY);

            _rotationY += deltaX;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
    }
}
