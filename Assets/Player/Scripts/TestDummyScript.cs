using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummyScript : Enemy
{
    [SerializeField] private float attackRange;
    [SerializeField] private float turnRate;
    [SerializeField] private float angleThreshold;
    [SerializeField] private Transform target;
    [SerializeField] private Weapon gunComponent;
    private GameObject gun;
    private Rigidbody2D body;
    private int hitLayer;

    void Awake()
    {
        hp = 1000;
        hitLayer = LayerMask.NameToLayer("Player");
        body = GetComponent<Rigidbody2D>();
        gun = transform.GetChild(0).gameObject;
        gun.transform.position = this.transform.position;
        gun.transform.rotation = this.transform.rotation;
        gunComponent = gun.GetComponent<Weapon>();
        gunComponent.SetLayer(hitLayer);
    }

    void FixedUpdate()
    {
        //look for player
        //Debug.Log(Vector2.Distance(target.position, this.transform.position));
        if (Vector2.Distance(target.position, this.transform.position) < attackRange)
        {
            //Debug.Log("ATTACKING");
            Attack();
        }
        // turn towards player
        //Debug.Log("SIGNEDANGLE" + angleToTarget());
        if (angleToTarget() > angleThreshold)
        {
            //Debug.Log("TURNING");
            Turn();
        }
        // fire at player
    }

    public override void Kill()
    {
        Destroy(gameObject, 0f);
    }

    void Attack()
    {
        //just shoot if angle is close enough
        if (angleToTarget() < angleThreshold)
        {
            //Debug.Log("SHOOTING");
            gunComponent.Shoot();
        }
    }

    private float signedAngleToTarget()
    {
        //transform has position as vector2 by default
        Vector2 targetLine = new Vector2(target.position.x, target.position.y);
        return Vector2.SignedAngle(body.transform.up, (targetLine - body.position));
    }

    private float angleToTarget()
    {
        //transform has position as vector2 by default
        Vector2 targetLine = new Vector2(target.position.x, target.position.y);
        return Vector2.Angle(body.transform.up, (targetLine - body.position));
    }


    void Turn()
    {
        //turn towards target
        float angle = signedAngleToTarget();
       // Debug.Log("ANGLE" + angle);
        if (angle < 0)
        {   
            body.rotation -= turnRate;
        } else if (angle > 0)
        {
            body.rotation += turnRate;
        }

    }

}
