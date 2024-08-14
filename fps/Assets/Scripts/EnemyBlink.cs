using System.Collections;
using UnityEngine;

public class EnemyBlink : MonoBehaviour
{
    private Animator enemyAnimator;
    public bool isBlinking;

    private void Start()
    {
        isBlinking = false;
        enemyAnimator = GetComponent<Animator>();
        StartCoroutine(BlinkAnimation());
    }

    IEnumerator BlinkAnimation()
    {
        while (true)
        {
            isBlinking = true;
            enemyAnimator.SetTrigger("Blink");
            yield return new WaitForSeconds(2f);
            isBlinking = false;
        }
    }
}
