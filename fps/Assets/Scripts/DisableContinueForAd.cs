using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisableContinueForAd : MonoBehaviour
{
    public DeathMenu deathMenu;

    private void OnEnable()
    {
        if (!deathMenu.watchedAdToContinue)
        {
            StartCoroutine(DisableButtonForPeriod());
        }
    }
    void Update()
    {
        if (deathMenu.watchedAdToContinue)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    private IEnumerator DisableButtonForPeriod()
    {
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - startTime < 1f)
        {
            gameObject.GetComponent<Button>().interactable = false;
            yield return null;
        }
        gameObject.GetComponent<Button>().interactable = true;
    }
}
