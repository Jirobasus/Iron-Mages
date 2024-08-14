using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public Image image;
    public bool coolDown;
    void Start()
    {
        coolDown = false;
        image = GetComponent<Image>();
    }
    
    public void ChangeShield(float damage)
    {
        if (!coolDown)
        {
            StartCoroutine(ReduceShieldBar(damage));
        }
    }

    private IEnumerator ReduceShieldBar(float damage)
    {
        coolDown = true;
        image.fillAmount -= damage;
        yield return new WaitForSeconds(1);
        coolDown = false;
    }

    public void ResetCooldown()
    {
        StopCoroutine(ReduceShieldBar(0.2f));
        coolDown = false;
    }
}
