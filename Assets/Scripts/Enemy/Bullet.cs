using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bullet : MonoBehaviour
{
    private Action<GameObject> BulletHit;
    public float Speed = 10f;
    public float Lifespan = 1f;
    private void Awake(){
        DeathManager DM = FindObjectOfType<DeathManager>();
        BulletHit += DM.BulletHit;
    }
    private void Start(){
        StartCoroutine(Despawn());
    }
    IEnumerator Despawn(){
        yield return new WaitForSeconds(Lifespan);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==6 || other.gameObject.layer == 7){
            BulletHit(other.gameObject);
            Destroy(gameObject);
        }
    }
}
