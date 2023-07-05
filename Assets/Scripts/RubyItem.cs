using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyItem : MonoBehaviour
{
    public Action<GameObject> OnRubyEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnRubyEnter != null && collision.gameObject.tag == "Player")
        {
            Health healthComp = collision.gameObject.GetComponent<Health>();
            OnRubyEnter(collision.gameObject);
            Destroy(gameObject);
            
        }
  
    }
}
