using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Weapon
{
    void Awake()
    {
        firePoint = transform.Find("GunFirePoint").transform;
    }

    public override void Shoot(Vector2 mousePos) 
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 bulletDir = (mousePos - (new Vector2(firePoint.position.x, firePoint.position.y)));
        bulletDir.Normalize();
        Rigidbody2D prb = projectile.GetComponent<Rigidbody2D>();
        prb.velocity = new Vector2(0f, 0f);
        prb.AddForce(bulletDir * bulletForce, ForceMode2D.Impulse);
    }
}
