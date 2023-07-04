using System;
using UnityEngine;

public class LavaBlock : MonoBehaviour
{
    public Action<GameObject> OnLavaStay;
    public Action<GameObject> OnLavaExit;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (OnLavaStay != null)
        {
            OnLavaStay(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (OnLavaStay != null)
        {
            OnLavaExit(collision.gameObject);
        }
    }
}
