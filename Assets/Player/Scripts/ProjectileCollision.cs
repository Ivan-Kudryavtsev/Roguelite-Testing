using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour, IProjectile
{

    [SerializeField] private float damage;
    private int hitLayer;
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void SetLayer(int layer)
    {
        hitLayer = layer;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //uhhhh
        Debug.Log(other.gameObject.layer + " AND " + hitLayer);
        if (other.gameObject.layer == hitLayer)
        {   
            //this is broken, is not detecting the layer
            Debug.Log("Bullet collided with " + other.gameObject.layer);
            var found = other.transform.gameObject.TryGetComponent<IDamageable>(out IDamageable stat);
            if (found) stat.Damage(damage);
        }
        Debug.Log("Bullet collided with " + other.name);
        Destroy(gameObject, 0f);
    }
}
