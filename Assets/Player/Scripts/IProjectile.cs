using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IProjectile
{
    abstract void SetDamage(float dmg);
    abstract void OnTriggerEnter2D(Collider2D other);
}
