using System.Collections;
using UnityEngine;

public class LogoResizer : MonoBehaviour
{
    public float resizeSpeed = 1.0f;
    public float scaleFactor = 0.1f;

    private Vector3 originalScale;
    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(ResizeLogo());
    }
    
    IEnumerator ResizeLogo()
    {
        while (true)
        {
            float scaleMultiplier = 1.0f + Mathf.Sin(Time.time * resizeSpeed) * scaleFactor;
            Vector3 newScale = originalScale * scaleMultiplier;
            transform.localScale = newScale;
            yield return null;
        }
    }
}
