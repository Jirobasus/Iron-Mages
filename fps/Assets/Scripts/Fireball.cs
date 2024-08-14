using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private AudioClip[] hurtAudioClips;
    public float speed = 10.0f;
    public float rotationSpeed = 10.0f;
    //public float damage = 0.2f;
    private Animator fireballAnimator;
    public Health healthScript;
    public Shield shieldScript;

    private void Start()
    {
        fireballAnimator = GetComponent<Animator>();
        healthScript = FindObjectOfType<Health>();
        shieldScript = FindObjectOfType<Shield>();
        StartCoroutine(DestroyAfterDelay());
    }

    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController player = other.GetComponent<CharacterController>();
        if (player != null)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(hurtAudioClips, transform, 1f);
            if (shieldScript.image.fillAmount > 0)
            {
                shieldScript.ChangeShield(0.2f);
            }
            else
            {
                healthScript.ChangeHealth(0.2f);
            }
        }
        fireballAnimator.SetBool("FireballFlying", false);
        Destroy(this.gameObject);
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
