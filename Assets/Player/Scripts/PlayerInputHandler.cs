using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movement;
    private Vector2 mousePos;
    [SerializeField] private float blowbackForce;
    [SerializeField] private int bufferFrames;
    [SerializeField] private float moveSpeed;
    //[SerializeField] private float bulletForce;
    [SerializeField] private Camera cam;
    //[SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int fireCD;
    private Rigidbody2D rb;
    //private Transform firePoint;
    private bool isFiring;
    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private GameObject emptyGunPrefab;
    [SerializeField] private GameObject gun;
    private Transform gunPoint;
    private Weapon gunComponent = null;

    void Awake()
    {
        movement = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
        //firePoint = transform.Find("FirePoint");
        gunPoint = transform.Find("GunPoint");
        isFiring = false;
        if (gun == null)
        {
            gun = Instantiate(gunPrefab, gunPoint.position, Quaternion.identity);
            gun.transform.SetParent(this.transform);
            SetGun(gun);
        }
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1"))
        {
            if (!isFiring)
            {
                isFiring = true;
            }
        }
        if (Input.GetKey(KeyCode.G) && gun != null)
        {
            DropGun();
        }
        if (Input.GetKey(KeyCode.K) && gun == null)
        {
            Debug.Log("I");
            CreateGun(gunPrefab);
        }
    }

    void CreateGun(GameObject prefab)
    {
        Debug.Log("II");
        gun = Instantiate(prefab, gunPoint.position, this.transform.rotation);
        gun.transform.SetParent(this.transform);
        SetGun(gun);
    }

    void SetGun(GameObject gun)
    {
        if (gun == null)
        {
            Debug.Log("III");
            gunComponent = null;
        }
        else
        {   
            gunComponent = gun.GetComponent<Weapon>();
        }
    }
        void FixedUpdate()
    {   
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        fireCD += 1;
        if (isFiring)
        {   
            if (fireCD >= bufferFrames)
            {
                Shoot(rb);
                fireCD = 0;
            }
            isFiring = !isFiring;
        }
    }

    void Shoot(Rigidbody2D body)
    {
        /*GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 bulletDir = (mousePos - (new Vector2(firePoint.position.x, firePoint.position.y)));
        bulletDir.Normalize();
        Rigidbody2D prb = projectile.GetComponent<Rigidbody2D>();
        prb.velocity = new Vector2(0f, 0f);
        prb.AddForce(bulletDir * bulletForce, ForceMode2D.Impulse);
        body.AddForce(-1 * bulletDir * bulletForce * blowbackForce, ForceMode2D.Force);*/
        Debug.Log(gun.name);
        Debug.Log(gunComponent.name);
        if (gun != null)
        {
            gunComponent.Shoot(mousePos);
        }
    }

    void PickupGun()
    {
        //i wonder how?
    }

    void DropGun()
    {
        gun.transform.parent = null;
        this.gun = null;
        SetGun(null);
    }
}
