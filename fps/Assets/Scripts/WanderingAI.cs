using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    private bool _alive;

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private AudioClip[] fireballAudioClips;
    private GameObject _fireball;

    private Animator fireballAnimator;
    public Animator enemyAnimator;
    public bool isShooting;
    public float wanderTimer;
    public float wanderDistance;
    public bool isFollowingPlayer;

    private Transform staffObject;
    private ParticleSystem staffFlash;
    private Transform playerTransform;
    private NavMeshAgent nav;
    private float timer;


    private void Start()
    {
        _alive = true;
        isShooting = false;
        enemyAnimator = GetComponent<Animator>();
        staffObject = transform.Find("MageStaff");
        staffFlash = staffObject.GetComponentInChildren<ParticleSystem>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }
    void Update()
    {
        if (_alive)
        {
            RaycastHit objectDistanceHit;
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out objectDistanceHit, 3f))
            {
                GameObject objectDistance = objectDistanceHit.transform.gameObject;
                if (objectDistance.GetComponent<CharacterController>() || objectDistance.GetComponent<WanderingAI>())
                {
                    nav.isStopped = true;
                }
                else
                {
                    nav.isStopped = false;
                    nav.destination = playerTransform.position;
                }
            }
            else
            {
                nav.isStopped = false;
                nav.destination = playerTransform.position;
            }

            RaycastHit playerHit;
            //Vector3 halfExtents = new Vector3(4f, 1f, 5f);

            if (Physics.Raycast(transform.position, transform.forward, out playerHit, 60f))
            {
                GameObject player = playerHit.transform.gameObject;
                if (player.GetComponent<CharacterController>())
                {
                    isFollowingPlayer = true;
                    nav.speed = 5;
                    Vector3 directionToPlayer = player.transform.position - transform.position;
                    directionToPlayer.y = 0f;
                    transform.rotation = Quaternion.LookRotation(directionToPlayer);
                    if (!isShooting)
                    {
                        StartCoroutine(StaffShoot());
                    }
                }
            }
            
        }

        
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    private IEnumerator StaffShoot()
    {
        isShooting = true;
        enemyAnimator.SetTrigger("StaffShoot");
        yield return new WaitForSeconds(1f);
        Vector3 staffPosition = staffObject.transform.position + transform.forward;
        staffFlash.Play();
        SoundFXManager.instance.PlayRandomSoundFXClip(fireballAudioClips, transform, 1);
        _fireball = Instantiate(fireballPrefab) as GameObject;
        fireballAnimator = _fireball.GetComponent<Animator>();
        fireballAnimator.SetBool("FireballFlying", true);
        _fireball.transform.position = staffPosition;
        _fireball.transform.rotation = Quaternion.LookRotation(-transform.forward);
        yield return new WaitForSeconds(1.5f);
        isShooting = false;
    }

    public static Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }
}
