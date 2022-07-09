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
    [SerializeField] private float mouseRadius;
    [SerializeField] private float pickupRadius;
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
    private PositionComparer posComp = new PositionComparer();

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
            //gun.transform.SetParent(this.transform);
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("pickpig up");
            PickupGun();
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
        posComp.SetPosition(mousePos);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, mouseRadius);
        List<Collider2D> weapons = new List<Collider2D>();
        // why am i adding colliders instead of the actual gameObjects???
        foreach (Collider2D col in colliders) {
            Debug.Log("Collider " + col.gameObject.name);
            if (col.gameObject.TryGetComponent<Weapon>(out Weapon stat) && col.transform.parent == null)
            {
                Debug.Log("Picking UP " + col.gameObject.name);
                weapons.Add(col);
            }
        }
        weapons.Sort(posComp);
        //Debug.Log("HERE");
        Debug.Log("CHOSE " + weapons[0].gameObject.name);
        //Debug.Log("THERE");*/
        DropGun();
        //Destroy(weapons[0].gameObject, 0f);
        SetGun(weapons[0].gameObject);
        // get distance of weapon from mousepos
        // get closest weapon and pcik up
    }

    void DropGun()
    {
        gun.transform.parent = null;
        gun.GetComponent<BoxCollider2D>().enabled = true;
        //this.gun = null;
        SetGun(null);
    }

    void CreateGun(GameObject prefab)
    {
        Debug.Log("II");
        GameObject tempgun = Instantiate(prefab, gunPoint.position, this.transform.rotation);
        //gun.transform.SetParent(this.transform);
        SetGun(tempgun);
    }

    void SetGun(GameObject gun)
    {
        if (gun == null)
        {
            Debug.Log("SETTING NULL GUN");
            this.gun = null;
            gunComponent = null;
        }
        else
        {
            Debug.Log("SETTING" + gun.name);
            this.gun = gun;
            gun.transform.SetParent(this.transform);
            gun.transform.position = gunPoint.position;
            gun.transform.rotation = gunPoint.rotation;
            //gun collider needs to be off
            gun.GetComponent<BoxCollider2D>().enabled = false;
            gunComponent = gun.GetComponent<Weapon>();
        }
    }
}
