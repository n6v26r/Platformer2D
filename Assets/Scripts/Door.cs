using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Action<GameObject> OnDoorCollision;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnDoorCollision(collision.gameObject);
    }
}
