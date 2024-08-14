using System.Collections;            
using UnityEngine;
using UnityEngine.AI;

public class ReactiveTarget : MonoBehaviour
{
    public GameObject gunObject;
    private Animator gunAnimator;
    public ParticleSystem muzzleflash;
    public RayShooter rayShooter;
    public WanderingAI wanderingAI;
    public SceneController sceneController;

    private Animator enemyAnimator;
    private int dieAnimationIndex;
    private NavMeshAgent nav;

    private void Start()
    {
        rayShooter = FindObjectOfType<RayShooter>();
        sceneController = FindObjectOfType<SceneController>();
        gunObject = GameObject.Find("Gun");
        Transform muzzleFlashTransform = gunObject.transform.Find("MuzzleFlash");
        muzzleflash = muzzleFlashTransform.GetComponent<ParticleSystem>();
        gunAnimator = gunObject.GetComponent<Animator>();
        enemyAnimator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        wanderingAI = GetComponent<WanderingAI>();
    }
    
    public void ReactToHit()
    {
        WanderingAI behaviour = GetComponent<WanderingAI>();
        if (behaviour != null)
        {
            behaviour.SetAlive(false);
        }
        wanderingAI.isFollowingPlayer = false;
        nav.enabled = false;
        StartCoroutine(Die());
    }
    

    private IEnumerator Die()
    {
        Vector3 startPosition = transform.position;

        Vector3 targetPosition = new Vector3(startPosition.x, 0.5f, startPosition.z);

        dieAnimationIndex = Random.Range(1, 5);

        gameObject.GetComponent<Collider>().enabled = false;

        enemyAnimator.SetInteger("EnemyDie", dieAnimationIndex);

        bool changed = false;

        float elapsedTime = 0f;

        while (elapsedTime < 2f)
        {
            if (elapsedTime > 1f && !changed)
            {
                changed = true;
            }
            float t = Mathf.Clamp01(elapsedTime / 2f) * 3f;

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
        sceneController.enemies.RemoveAt(sceneController.enemies.Count - 1);
        enemyAnimator.SetInteger("EnemyDie", 0);
        changed = false;
    }
}
