using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //uhhhh
        if (other.CompareTag("Player")){
            return;
        }
        Debug.Log("Bullet collided with " + other.name);
        Destroy(gameObject, 0f);
    }
}
