using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGun
{
    public void Fire();
    public void GunFire();
}

interface IHealth
{
    public void getDamage(float damage);
}