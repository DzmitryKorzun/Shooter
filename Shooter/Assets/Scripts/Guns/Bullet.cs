using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform bulletTransform;

    private Gun myGun;
    private float speed;
    private WeaponOwner bulletOwner;
    private Vector3 endPoint;
    private float damage;
    
    private void FixedUpdate()
    {
        bulletTransform.position = Vector3.MoveTowards(bulletTransform.position, endPoint, speed);
        if (bulletTransform.position.Equals(endPoint))
        {
            this.gameObject.SetActive(false);
            myGun.ReturnBulletToQueue(this);
        }
    }

    public void Setup(float speed, Gun gun, WeaponOwner weaponOwner, Vector3 endPoint, float damage)
    {
        this.endPoint = endPoint;
        this.myGun = gun;
        bulletTransform.position = gun.transform.position;
        this.speed = speed;
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Попали: " + collision.gameObject.name);
        IHealth health = collision.gameObject.GetComponent<IHealth>();
        if (health != null)
        {

            health.getDamage(damage);
        }
        if (collision.gameObject.tag != "Bullet")
        {
            this.gameObject.SetActive(false);
            myGun.ReturnBulletToQueue(this);
        }

    }
}