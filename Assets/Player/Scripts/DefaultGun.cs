using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Weapon
{
    void Awake()
    {
        firePoint = transform.Find("GunFirePoint").transform;
        fireCD = 20000;
    }

    void FixedUpdate() {
        fireCD += 1;
    }

    public override void Shoot(Vector2 mousePos) 
    {   
        if (fireCD < fireRate)
        {
            //Debug.Log("!!!!!!");
            return;
        }
        //Debug.Log("}}}}}}}}}}");
        fireCD = 0f;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 bulletDir = (mousePos - (new Vector2(firePoint.position.x, firePoint.position.y)));
        bulletDir.Normalize();


        projectile.GetComponent<IProjectile>().SetDamage(damage);
        Rigidbody2D prb = projectile.GetComponent<Rigidbody2D>();
        prb.velocity = new Vector2(0f, 0f);
        prb.AddForce(bulletDir * bulletForce, ForceMode2D.Impulse);
    }
}
