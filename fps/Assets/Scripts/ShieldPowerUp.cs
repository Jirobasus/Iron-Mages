using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float speed = 70f;
    public Shield shield;
    [SerializeField] private AudioClip[] pickupAudioClips;
    void Start()
    {
        shield = FindObjectOfType<Shield>();
    }

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController player = other.GetComponent<CharacterController>();
        if (player != null && shield.image.fillAmount < 1)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(pickupAudioClips, transform, 0.5f);
            shield.image.fillAmount = 1;
            //sceneController.healthPowerUps.RemoveAt(sceneController.healthPowerUps.Count - 1);
            Destroy(this.gameObject);
        }
    }
}
