using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public RayShooter rayShooter;
    //public bool hitMarkSpawned = false;
    public GameObject hitMarkInstance;
    public GameObject hitMark;
    public SceneController sceneController;

    private void Start()
    {
        rayShooter = FindObjectOfType<RayShooter>();
        sceneController = FindObjectOfType<SceneController>();
        //hitExplosion = GetComponent<ParticleSystem>();
        hitMark = GameObject.Find("HitMark");
        StartCoroutine(DestroyAfterDelay());
    }

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            hitMarkInstance = Instantiate(hitMark, rayShooter.hitPosition + (rayShooter.hitNormal * .01f), Quaternion.FromToRotation(Vector3.up, rayShooter.hitNormal));
            sceneController.DeleteHitMark(hitMarkInstance, gameObject.transform.position, rayShooter.bulletRotation);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Enemy")
        {
            ReactiveTarget target = other.gameObject.GetComponent<ReactiveTarget>();
            target.ReactToHit();
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
