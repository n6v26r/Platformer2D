using System;
using UnityEngine;

public class LavaBlock : MonoBehaviour
{
    public Action<GameObject> OnLavaStay;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (OnLavaStay != null)
        {
            OnLavaStay(collision.gameObject);
        }
    }
}
