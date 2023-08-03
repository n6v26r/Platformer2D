using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    private bool Blew = false;
    public Action<float, float> OnBlow;
    public Action<GameObject> OnBlowVictim;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Blew == true)
        {
            OnBlowVictim(collision.gameObject);
        }
    }

    public void Blow()
    {
        if (Blew == false)
        {
            Debug.Log("I`m boutta to blowwwww uhhhhhh");
            StartCoroutine(Blowing());
        }
    }

    private IEnumerator Blowing()
    {
        yield return new WaitForSeconds(0.1f);
        Blew = true;
        OnBlow(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(0.5f);
        Blew = false;
        Destroy(gameObject);
    }
}
