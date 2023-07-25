using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public int ItemType;
    public Action<GameObject, int> OnItemEnter;

    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnItemEnter != null && collision.gameObject.tag == "Player")
        {
            OnItemEnter(collision.gameObject, ItemType);
            Destroy(gameObject);
        }

    }
}
