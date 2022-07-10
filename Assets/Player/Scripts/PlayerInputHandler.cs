using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movement;
    private Vector2 mousePos;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseRadius;
    [SerializeField] private float pickupRadius;
    //[SerializeField] private float bulletForce;
    [SerializeField] private Camera cam;
    //[SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int fireCD;
    [SerializeField] private SelectionManager selectionManager;

    private Rigidbody2D rb;
    private Transform firePoint;
    private bool isFiring;
    [SerializeField] private float blowbackForce;
    [SerializeField] private int bufferFrames;
    [SerializeField] private GameObject gunPrefab;
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
            //redundant?
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
        if (isFiring)
        {
            Shoot(rb);
            isFiring = false;
        }
    }

    void Shoot(Rigidbody2D body)
    {
        Debug.Log(gun.name);
        Debug.Log(gunComponent.name);
        if (gun != null)
        {
            gunComponent.Shoot(mousePos);
        }
    }


    void PickupGun()
    {
        //Debug.Log("MOUSE DISTANCE" + Vector2.Distance(mousePos, rb.position));
        if (Vector2.Distance(mousePos,rb.position) > pickupRadius)
        {
            return;
        }
        Transform potentialGun = selectionManager.GetSelection();

        if (potentialGun.gameObject.TryGetComponent<Weapon>(out Weapon stat) && potentialGun.transform.parent == null)
        {
            Debug.Log("Picking UP " + potentialGun.gameObject.name);
            DropGun();
            SetGun(potentialGun.gameObject);
        }
    }

    void DropGun()
    {
        gun.transform.parent = null;
        gun.GetComponent<BoxCollider2D>().enabled = true;
        SetGun(null);
    }

    void CreateGun(GameObject prefab)
    {
        GameObject tempgun = Instantiate(prefab, gunPoint.position, this.transform.rotation);
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
