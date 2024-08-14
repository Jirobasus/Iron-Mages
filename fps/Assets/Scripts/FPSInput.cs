using UnityEngine;
using UnityEngine.UI;

public class FPSInput : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 6.0f;
    public float gravity = -9.8f;
    public Image staminaBarImage;
    public float increaseFillSpeed = 0.1f;
    public float decreaseFillSpeed = 0.5f;
    private bool isSprinting;
    private bool canResumeSprinting;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        
        characterController.Move(movement);
        /*
        if (staminaBarImage.fillAmount == 1f)
        {
            isStaminaRefilled = true;
        }*/

        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaBarImage.fillAmount > 0 && canResumeSprinting)
        {
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || staminaBarImage.fillAmount == 0)
        {
            isSprinting = false;
            canResumeSprinting = false;
        }

        if (isSprinting)
        {
            speed = 12f;
            float newFillAmount = staminaBarImage.fillAmount - (decreaseFillSpeed * Time.deltaTime);
            staminaBarImage.fillAmount = Mathf.Clamp01(newFillAmount);
        }
        else
        {
            speed = 6f;
            float newFillAmount = staminaBarImage.fillAmount + (increaseFillSpeed * Time.deltaTime);
            staminaBarImage.fillAmount = Mathf.Clamp01(newFillAmount);
        }

        if (staminaBarImage.fillAmount == 1f)
        {
            canResumeSprinting = true;
        }
    }
}
