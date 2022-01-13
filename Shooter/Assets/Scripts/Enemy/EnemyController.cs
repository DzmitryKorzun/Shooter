using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour, IHealth
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private EnemyUIController uIController;
    [SerializeField] private int repeatFireRate;
    [SerializeField] private Gun[] guns;

    private float health;
    private int IDGun;
    private IGun iGUN;

    private void Start()
    {
        health = maxHealth;
        ActiveRandomGun();
        InvokeRepeating(nameof(FireRandomGun), 0, repeatFireRate);
    }

    public void getDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        uIController.healthBarChange(health / maxHealth);
        if (health == 0)
        {
            CancelInvoke(nameof(FireRandomGun));
            this.gameObject.SetActive(false);
        }
    }

    private void ActiveRandomGun()
    {
        IDGun = UnityEngine.Random.Range(0, guns.Length);
        for (int i = 0; i < guns.Length; i++)
        {
            if (IDGun != (i + 1))
            {
                guns[i].gameObject.SetActive(false);
            }
            else
            {
                guns[i].gameObject.SetActive(true);
            }
        }
    }

    private void FireRandomGun()
    {
        guns[IDGun].gameObject.GetComponent<IGun>().GunFire();
    }

    private void FixedUpdate()
    {
       // enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, target.position, speed);
        enemyTransform.LookAt(target.position);
    }
}
