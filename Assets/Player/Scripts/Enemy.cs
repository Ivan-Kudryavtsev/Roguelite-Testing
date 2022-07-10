using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField] protected float hp;

    // Update is called once per frame

    public void Damage(float damage) {
        hp -= damage;
        if (hp <= 0f)
        {
            this.Kill();
        }
    }

    public abstract void Kill();
}
