using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopazItem : MonoBehaviour
{
    public Action<GameObject> OnTopazEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnTopazEnter != null && collision.gameObject.tag == "Player")
        {
            OnTopazEnter(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
