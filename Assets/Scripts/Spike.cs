using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spike : MonoBehaviour
{
    public Action<GameObject> OnHit;

    private void OnCollisionEnter2D(Collision2D other) {
        OnHit(other.gameObject);
    }
}
