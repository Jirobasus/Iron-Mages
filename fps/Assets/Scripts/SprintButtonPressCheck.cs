using UnityEngine;
using UnityEngine.EventSystems;

public class SprintButtonPressCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool _pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
    }
}
