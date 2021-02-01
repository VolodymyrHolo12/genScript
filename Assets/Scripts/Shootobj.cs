using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootobj : MonoBehaviour
{
    //public AudioClip ShotSound;

    Rigidbody2D rigidbody2d;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
         if(transform.position.magnitude > 100.0f)
         {
             Destroy(gameObject);
         }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Projectile collision with " + other.gameObject);
        /*EnemyController e = other.collider.GetComponent<EnemyController>();
        if(e != null)
        {
            e.Fix();
        }*/
        Destroy(gameObject);
    }
    
    
}