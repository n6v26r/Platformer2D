using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spike : MonoBehaviour
{
    public Action<GameObject> OnHit;
    private SoundManger SoundManager;

    private void Awake(){
        SoundManager = FindAnyObjectByType<SoundManger>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
            SoundManager.PlaySound(SoundManager.CellingSpike);
        OnHit(other.gameObject);
    }
}
