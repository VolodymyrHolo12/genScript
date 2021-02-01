using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multyShot : Shooting
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        float startRot = Fire.rotation.z;
        //Debug.Log(startRot);
        for (int i = 0; i < bulletsNumber; i++)
        {
            float angle = Mathf.PI*2*radius / 4*bulletsNumber;
            float angleDeg = -angle * i * Mathf.Rad2Deg-45f;
            Fire.Rotate(0,0,angleDeg);
            GameObject bullet = Instantiate(bulletPref, Fire.position, Fire.rotation); // костыль, нужно исправить
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Fire.up * bulletForce, ForceMode2D.Impulse);
        }
        Fire.Rotate(0,0,startRot);
    }
}
