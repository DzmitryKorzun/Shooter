using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun, IGun
{
    [SerializeField] private int numberOfPelletsPerShot;

    private void Start()
    {
        currentAmmoCount = magazine—apacity;
        if (owner == WeaponOwner.Enemy)
        {
            this.enabled = false;
        }
    }

    public void GunFire()
    {
        for (int i = 0; i < numberOfPelletsPerShot; i++)
        {
            isDelay = true;
            Invoke(nameof(ShotDelay), speedFire);
            Fire();
        }
        currentAmmoCount--;
        GunActive();
        if (currentAmmoCount == 0)
        {
            MagazineReload();
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && isDelay == false)
        {
           GunFire();
        }
    }
}
