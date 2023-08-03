using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDart : MonoBehaviour
{
    private float Speed = 25f;
    private float Distante = 0f;
    private Vector3 targetpos;

    private Action<GameObject> FireDartHit;
    private DeathManager deathManager;


    private void Awake()
    {
        deathManager = FindObjectOfType<DeathManager>();
        FireDartHit += deathManager.FireDartHit;
        targetpos = new Vector3(transform.position.x-0.5f+Distante,transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.localScale.x == 1) 
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x+1, transform.position.y, transform.position.z), Speed * Time.deltaTime);
       else if(transform.localScale.x == -1)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "FireDartShooter")
        {
            if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
            {
                FireDartHit(collision.gameObject);
                Destroy(gameObject);
            }
            else if(collision.gameObject.layer == 0 || collision.gameObject.layer == 3)
                Destroy(gameObject);
        }
    }
}
