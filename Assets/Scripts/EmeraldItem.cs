using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldItem : MonoBehaviour
{
    public Action<GameObject> OnEmeraldEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnEmeraldEnter != null && collision.gameObject.tag == "Player")
        {
            OnEmeraldEnter(collision.gameObject);
            Destroy(gameObject);
        }

    }
}