using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    public Transform Fire;
    public GameObject bulletPref;
    public float bulletForce = 20f;
    public float bulletsNumber = 10;
    public float radius = 4f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, Fire.position, Fire.rotation); // костыль, нужно исправить
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Fire.up * bulletForce, ForceMode2D.Impulse);
    }
}
