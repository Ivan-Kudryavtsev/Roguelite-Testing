using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] public float damage;
    [SerializeField] public float bulletForce;
    [SerializeField] public float fireRate;
    [SerializeField] public float fireCD;
    [SerializeField] protected GameObject projectilePrefab;
    protected Transform firePoint;

    protected Weapon()
    {    }

    protected Weapon(GameObject obj, float dmg)
    {
        projectilePrefab = obj;
        dmg = damage;
    }

    public abstract void Shoot(Vector2 mousePos);
}
