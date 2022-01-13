using UnityEngine;

public class Pistol : Gun, IGun
{
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
        isDelay = true;
        Invoke(nameof(ShotDelay), speedFire);
        Fire();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && isDelay == false)
        {
            GunFire();
        }
    }


}
