using System.Collections;
using TMPro;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private AudioClip[] shotAudioClips;
    private GameObject _bullet;
    private Camera _camera;
    private Animator gunAnimator;

    public ParticleSystem muzzleflash;
    public ChangeWeapon changeWeapon;
    public Vector3 hitPosition;
    public Vector3 hitNormal;
    public GameObject hitObject;
    public Quaternion bulletRotation;
    public TextMeshProUGUI ammoTextMeshPro;
    public TextMeshProUGUI mobileAmmoTextMeshPro;
    public SceneController sceneController;
    public GameObject gunObject;
    public PlatformSwitcher platformSwitcher;
    public TextMeshProUGUI reloadTextMeshPro;
    public TextMeshProUGUI noAmmoTextMeshPro;

    public int totalAmmo = 28;
    public int ammoInMag = 7;

    public bool isAnimationRunning = false;
    void Start()
    {
        /*
        platformSwitcher = FindObjectOfType<PlatformSwitcher>();
        if (platformSwitcher.isDesktop)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        */
        _camera = GetComponent<Camera>();
        gunAnimator = gunObject.GetComponent<Animator>();
        changeWeapon = FindObjectOfType<ChangeWeapon>();
        hitPosition = new Vector3(0, 0, 0);
        hitNormal = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (!sceneController.isPaused)
        {
            UpdateText();
            if (changeWeapon.selectedWeapon == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ShootInput();
                }
                else if (Input.GetKey(KeyCode.R))
                {
                    ReloadInput();
                }
            }
        }

        if (ammoInMag == 0 && totalAmmo != 0)
        {
            reloadTextMeshPro.gameObject.SetActive(true);
        }
        else
        {
            reloadTextMeshPro.gameObject.SetActive(false);
        }

        if (ammoInMag == 0 && totalAmmo == 0)
        {
            noAmmoTextMeshPro.gameObject.SetActive(true);
        }
        else if (totalAmmo != 0)
        {
            noAmmoTextMeshPro.gameObject.SetActive(false);
        }
    }

    public void ShootInput()
    {
        Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && _bullet == null && !isAnimationRunning && !isAnimationRunning && ammoInMag > 0)
        {
            GameObject rayHitObject = hit.transform.gameObject;
            hitObject = rayHitObject;
            hitPosition = hit.point;
            hitNormal = hit.normal;
            StartCoroutine(SpawnBullet());
        }
        else if (_bullet == null && !isAnimationRunning && ammoInMag > 0)
        {
            StartCoroutine(SpawnBullet());
            StartCoroutine(ShotInSkybox());
        }
    }

    public void ReloadInput()
    {
        if (!isAnimationRunning && ammoInMag < 7 && totalAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator SpawnBullet()
    {
        ammoInMag--;
        isAnimationRunning = true;
        Vector3 bulletPosition = transform.position + transform.forward;
        bulletRotation = Quaternion.LookRotation(transform.forward);
        bulletRotation *= Quaternion.Euler(0, 90, 0);
        muzzleflash.Play();
        gunAnimator.SetTrigger("GunShooting");
        SoundFXManager.instance.PlayRandomSoundFXClip(shotAudioClips, transform, 1);
        _bullet = Instantiate(bulletPrefab);
        _bullet.transform.position = bulletPosition;
        _bullet.transform.rotation = bulletRotation;
        yield return new WaitForSeconds(gunAnimator.GetCurrentAnimatorStateInfo(0).length);

        isAnimationRunning = false;
    }

    private IEnumerator Reload()
    {
        isAnimationRunning = true;

        gunAnimator.SetTrigger("Reload");

        yield return new WaitForSeconds(gunAnimator.GetCurrentAnimatorStateInfo(0).length);

        int maxAmmoToReload = Mathf.Min(totalAmmo, 7 - ammoInMag);
        ammoInMag += maxAmmoToReload;
        totalAmmo -= maxAmmoToReload;
        

        isAnimationRunning = false;
    }

    private IEnumerator ShotInSkybox()
    {
        yield return new WaitForSeconds(1f);
        Destroy(_bullet);
    }

    private void UpdateText()
    {
        ammoTextMeshPro.text = "Ammo " + ammoInMag + "/" + totalAmmo;
        /*
        if (platformSwitcher.isDesktop)
        {

        }
        else
        {
            mobileAmmoTextMeshPro.text = "Ammo " + ammoInMag + "/" + totalAmmo;
        }
        */
    }
}
