using UnityEngine;

public class ShootButtonHandler : MonoBehaviour
{
    public KnifeHit knifeHit;
    public RayShooter rayShooter;
    public ChangeWeapon changeWeapon;

    public void ShotHandler()
    {
        if (changeWeapon.selectedWeapon == 0)
        {
            knifeHit.KnifeHitInput();
        }
        else
        {
            rayShooter.ShootInput();
        }
    }
}
