using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public Action<GameObject> OnKeyEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnKeyEnter != null && collision.gameObject.tag == "Player")
        {
            OnKeyEnter(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
