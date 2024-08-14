using UnityEngine;

public class AmmoBoxPowerUp : MonoBehaviour
{
    public float speed = 70f;
    public RayShooter rayShooter;
    public SceneController sceneController;
    [SerializeField] private AudioClip[] pickupAudioClips;
    void Start()
    {
        rayShooter = FindObjectOfType<RayShooter>();
        sceneController = FindObjectOfType<SceneController>();
    }
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController player = other.GetComponent<CharacterController>();
        if (player != null && (rayShooter.totalAmmo < 28 || rayShooter.ammoInMag < 7))
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(pickupAudioClips, transform, 0.5f);
            rayShooter.ammoInMag = 7;
            rayShooter.totalAmmo = 28;
            sceneController.ammoPowerUps.RemoveAt(sceneController.ammoPowerUps.Count - 1);
            Destroy(gameObject);
        }
    }
}
