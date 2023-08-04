using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    public int Type;

    public Action<GameObject, int> OnLiquidEnter;
    public Action<GameObject, int> OnLiquidStay2D;
    public Action<int> OnLiquidExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(OnLiquidEnter != null && collision.gameObject.tag == "Player")
        {
            OnLiquidEnter(collision.gameObject, Type);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (OnLiquidStay2D != null && collision.gameObject.tag == "Player")
        {
            OnLiquidStay2D(collision.gameObject, Type);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (OnLiquidExit != null && collision.gameObject.tag == "Player")
        {
            OnLiquidExit(Type);
        }
    }
}