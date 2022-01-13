using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponOwner
{
    Person,
    Enemy
}

public class Gun: MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speedFire;
    [SerializeField] protected float fireDistanse;
    [SerializeField] protected float scatterRadius;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected WeaponOwner owner;
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected int magazineÑapacity;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected Transform head;

    protected int currentAmmoCount;
    protected bool isReloaded = false;
    protected Queue<Bullet> queueOfBullets = new Queue<Bullet>();
    protected bool isDelay = false;
    protected Vector3 end_position;
    protected Vector3 start_position;
    protected float distanseToEndPoint;

    public delegate void GunDelegate(int currentArmo, int magazineÑapacity, bool isReload);
    public event GunDelegate weaponStateChangeEvent;

    private Vector3 add = new Vector3(1, 1, 1);

    protected void MagazineReload()
    {
        weaponStateChangeEvent?.Invoke(currentAmmoCount, magazineÑapacity, true);
        isReloaded = true;
        Invoke(nameof(AddBullets), reloadTime);
    }

    protected void AddBullets()
    {
        isReloaded = false;
        currentAmmoCount = magazineÑapacity;
        weaponStateChangeEvent?.Invoke(currentAmmoCount, magazineÑapacity, false);
    }

    protected Bullet GetBullet()
    {
        if (queueOfBullets.Count == 0)
        {
            Bullet bul = Instantiate(bullet, this.transform.position, Quaternion.identity);
            bul.gameObject.SetActive(false);
            return bul;
        }
        else
        {
            return queueOfBullets.Dequeue();
        }
    }
    protected Vector3 DispersionÑalculation(Vector3 end_position, float distance)
    {
        float x = Random.Range(-scatterRadius, scatterRadius) * (distance / fireDistanse);
        float y = Random.Range(-scatterRadius, scatterRadius) * (distance / fireDistanse);
        float z = Random.Range(-scatterRadius, scatterRadius) * (distance / fireDistanse);
        return new Vector3(end_position.x + x, end_position.y + y, end_position.z + z);
    }
    protected void ShotDelay()
    {
        isDelay = false;
    }

    public void Fire()
    {
        if (currentAmmoCount > 0 && isReloaded == false)
        {
            Ray ray = new Ray(head.position, head.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, fireDistanse))
            {
                end_position = hit.point + transform.forward * 1.1f;
                start_position = ray.origin;
            }
            else
            {
                start_position = Camera.main.transform.position;
                end_position = start_position + transform.forward * fireDistanse;
            }
            Bullet bul = GetBullet();
            distanseToEndPoint = Vector3.Distance(start_position, end_position);
            bul.Setup(bulletSpeed, this, owner, DispersionÑalculation(end_position, distanseToEndPoint), damage);
            bul.gameObject.SetActive(true);
            currentAmmoCount--;
            GunActive();
            if (currentAmmoCount == 0)
            {
                MagazineReload();
            }
        }
        else
        {
            MagazineReload();
        }
    }

    public void ReturnBulletToQueue(Bullet bul)
    {
        queueOfBullets.Enqueue(bul);
    }

    public void GunActive()
    {
        weaponStateChangeEvent?.Invoke(currentAmmoCount, magazineÑapacity, false);
    }
}