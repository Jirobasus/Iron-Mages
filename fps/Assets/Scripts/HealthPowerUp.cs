using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public float speed = 70f;
    public Health health;
    public SceneController sceneController;
    [SerializeField] private AudioClip[] pickupAudioClips;

    private void Start()
    {
        health = FindObjectOfType<Health>();
        sceneController = FindObjectOfType<SceneController>();
    }

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController player = other.GetComponent<CharacterController>();
        if (player != null && health.image.fillAmount < 1)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(pickupAudioClips, transform, 0.5f);
            health.image.fillAmount += 0.2f;
            sceneController.healthPowerUps.RemoveAt(sceneController.healthPowerUps.Count - 1);
            Destroy(this.gameObject);
        }
    }
}
