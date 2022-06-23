using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{

    private float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //uhhhh
        if (other.CompareTag("Player")){
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            var found = other.transform.gameObject.TryGetComponent<IDamageable>(out IDamageable stat);
            if (found) stat.Damage(damage);
        }
        Debug.Log("Bullet collided with " + other.name);
        Destroy(gameObject, 0f);
    }
}
