using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummyScript : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp;

    void Awake()
    {
        hp = 1000;
    }

    public void Damage(float damage)
    {
        hp -= damage;
    }
}
