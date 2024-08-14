using UnityEngine;
using UnityEngine.UI;

public class FPSInputMobile : MonoBehaviour
{
    public FixedJoystick _joystick;

    [SerializeField] private float _moveSpeed;
    public float gravity = -9.8f;
    private CharacterController characterController;

    public Image staminaBarImage;
    public float increaseFillSpeed = 0.1f;
    public float decreaseFillSpeed = 0.5f;

    private bool isSprinting;
    private bool canResumeSprinting;
    public SprintButtonPressCheck sprintButton;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {   
        float deltaX = _joystick.Horizontal * _moveSpeed;
        float deltaZ = _joystick.Vertical * _moveSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _moveSpeed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        characterController.Move(movement);

        if (sprintButton._pressed && staminaBarImage.fillAmount > 0 && canResumeSprinting)
        {
            isSprinting = true;
        }

        if (!sprintButton._pressed || staminaBarImage.fillAmount == 0)
        {
            isSprinting = false;
            canResumeSprinting = false;
        }
        if (isSprinting)
        {
            _moveSpeed = 12f;
            float newFillAmount = staminaBarImage.fillAmount - (decreaseFillSpeed * Time.deltaTime);
            staminaBarImage.fillAmount = Mathf.Clamp01(newFillAmount);
        }
        else
        {
            _moveSpeed = 6f;
            float newFillAmount = staminaBarImage.fillAmount + (increaseFillSpeed * Time.deltaTime);
            staminaBarImage.fillAmount = Mathf.Clamp01(newFillAmount);
        }

        if (staminaBarImage.fillAmount == 1f)
        {
            canResumeSprinting = true;
        }
    }
}
