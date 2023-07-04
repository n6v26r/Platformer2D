using System;
using UnityEngine;

public class LavaBlock : MonoBehaviour
{
    public Action<GameObject> OnLavaStay2D;
    public Action<GameObject> OnLavaExit;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (OnLavaStay2D != null)
        {
            OnLavaStay2D(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (OnLavaExit != null)
        {
            OnLavaExit(collision.gameObject);
        }
    }
}
