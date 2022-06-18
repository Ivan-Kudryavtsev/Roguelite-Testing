using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movement;
    private Vector2 mousePos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bulletForce;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject projectilePrefab;
    private Rigidbody2D rb;
    private Transform firePoint;

    void Awake()
    {
        movement = new Vector3(0, 0);
        rb = GetComponent<Rigidbody2D>();
        firePoint = transform.Find("FirePoint");
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D prb = projectile.GetComponent<Rigidbody2D>();
        prb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
