using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK : Gun, IGun
{
    private void Start()
    {
        currentAmmoCount = magazine—apacity;
        if (owner == WeaponOwner.Enemy)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && isDelay == false)
        {
            GunFire();
        }
    }

    public void GunFire()
    {
        isDelay = true;
        Invoke(nameof(ShotDelay), speedFire);
        Fire();
    }
}
