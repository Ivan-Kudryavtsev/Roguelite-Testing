using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //uhhhh
        Debug.Log("Trigger");
        Destroy(this);
    }
}
