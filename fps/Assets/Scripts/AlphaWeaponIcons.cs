using UnityEngine;
using UnityEngine.UI;

public class AlphaWeaponIcons : MonoBehaviour
{
    public ChangeWeapon changeWeapon;
    public PlatformSwitcher platformSwitcher;
    public Button gunButton;
    public Button knifeButton;
    public Image knifeIcon;
    public Image gunIcon;
    public bool weaponSetToGun;

    private void Start()
    {
        if (!platformSwitcher.isDesktop)
        {
            knifeIcon = knifeButton.GetComponent<Image>();
            gunIcon = gunButton.GetComponent<Image>();
        }
        weaponSetToGun = true;
    }

    void Update()
    {
        if (changeWeapon.selectedWeapon == 0 && !weaponSetToGun)
        {
            Color knifeColor = knifeIcon.color;
            Color gunColor = gunIcon.color;
            knifeColor.a = 1;
            gunColor.a = 0.5f;
            knifeIcon.color = knifeColor;
            gunIcon.color = gunColor;
            weaponSetToGun = true;
        }
        else if (changeWeapon.selectedWeapon == 1 && weaponSetToGun)
        {
            Color knifeColor = knifeIcon.color;
            Color gunColor = gunIcon.color;
            gunColor.a = 1;
            knifeColor.a = 0.5f;
            knifeIcon.color = knifeColor;
            gunIcon.color = gunColor;
            weaponSetToGun = false;
        }
    }
}
