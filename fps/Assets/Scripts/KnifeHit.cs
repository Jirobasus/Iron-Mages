using System.Collections;
using UnityEngine;

public class KnifeHit : MonoBehaviour
{
    public RayShooter rayShooter;
    public ChangeWeapon changeWeapon;
    private Animator knifeAnimator;
    private Collider knifeCollider;
    public ReactiveTarget reactiveTarget;
    public GameObject enemyPrefab;
    public SceneController sceneController;
    public PlatformSwitcher platformSwitcher;
    void Start()
    {
        changeWeapon = FindObjectOfType<ChangeWeapon>();
        rayShooter = FindObjectOfType<RayShooter>();
        sceneController = FindObjectOfType<SceneController>();
        knifeAnimator = GetComponent<Animator>();
        knifeCollider = GetComponent<Collider>();
        knifeCollider.enabled = false;
        reactiveTarget = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && platformSwitcher.isDesktop)
        {
            KnifeHitInput();
        }
    }

    public void KnifeHitInput()
    {
        if (changeWeapon.selectedWeapon == 0 && !rayShooter.isAnimationRunning)
        {
            StartCoroutine(KnifeHitAnimation());
        }
    }

    private IEnumerator KnifeHitAnimation()
    {
        rayShooter.isAnimationRunning = true;
        knifeAnimator.SetTrigger("KnifeHit");
        yield return new WaitForSeconds(knifeAnimator.GetCurrentAnimatorStateInfo(0).length / 2f);
        knifeCollider.enabled = true;
        yield return new WaitForSeconds(knifeAnimator.GetCurrentAnimatorStateInfo(0).length / 2f);
        knifeCollider.enabled = false;
        rayShooter.isAnimationRunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        WanderingAI wanderingAI = other.GetComponent<WanderingAI>();
        if (wanderingAI != null)
        {
            knifeCollider.enabled = false;
            ReactiveTarget target = other.gameObject.GetComponent<ReactiveTarget>();
            target.ReactToHit();
        }
    }
}
