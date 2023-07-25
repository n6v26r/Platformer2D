using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int DoorType;
    public Action<GameObject, int> OnDoorCollision;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnDoorCollision(collision.gameObject, DoorType);
    }
}
