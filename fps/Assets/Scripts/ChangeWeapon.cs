using System.Collections;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject gunObject;
    public GameObject knifeObject;
    private Animator gunAnimator;
    private Animator knifeAnimator;
    public RayShooter rayShooter;

    public int selectedWeapon = 0;
    void Start()
    {
        gunAnimator = gunObject.GetComponent<Animator>();
        knifeAnimator = knifeObject.GetComponent<Animator>();
        rayShooter = FindObjectOfType<RayShooter>();

        SelectWeapon();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ChangeWeaponToKnife();
        }
        else if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            ChangeWeaponToGun();
        }
    }

    private IEnumerator WeaponChange()
    {
        if (selectedWeapon == 0)
        {
            rayShooter.isAnimationRunning = true;
            gunAnimator.SetTrigger("GunRemove");
            yield return new WaitForSeconds(gunAnimator.GetCurrentAnimatorStateInfo(0).length);
            SelectWeapon();
            knifeAnimator.SetTrigger("KnifeGet");
            yield return new WaitForSeconds(knifeAnimator.GetCurrentAnimatorStateInfo(0).length);
            rayShooter.isAnimationRunning = false;
        }
        else if (selectedWeapon == 1)
        {
            rayShooter.isAnimationRunning = true;
            knifeAnimator.SetTrigger("KnifeRemove");
            yield return new WaitForSeconds(knifeAnimator.GetCurrentAnimatorStateInfo(0).length);
            SelectWeapon();
            gunAnimator.SetTrigger("GunGet");
            yield return new WaitForSeconds(knifeAnimator.GetCurrentAnimatorStateInfo(0).length);
            rayShooter.isAnimationRunning = false;
        }
    }

    public void ChangeWeaponToGun()
    {
        if (selectedWeapon == 0 && !rayShooter.isAnimationRunning)
        {
            selectedWeapon = 1;
            StartCoroutine(WeaponChange());
        }
        
    }

    public void ChangeWeaponToKnife()
    {
        if (selectedWeapon == 1 && !rayShooter.isAnimationRunning)
        {
            selectedWeapon = 0;
            StartCoroutine(WeaponChange());
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                foreach(Transform child in weapon)
                {
                    child.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform child in weapon)
                {
                    child.gameObject.SetActive(false);
                }
            }
            i++;
        }
    }
}
