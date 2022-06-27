using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummyScript : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField] private float hp;

    void Awake()
    {
        hp = 1000;
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
        {
            this.Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject, 0f);
    }
   
}
