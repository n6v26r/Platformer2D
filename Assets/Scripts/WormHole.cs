using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHole : MonoBehaviour
{

    public GameObject WormBrother;
    private WormHole WormBrotherScript;
    private bool canteleport = true;

    void Awake(){
        WormBrotherScript = WormBrother.GetComponent<WormHole>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canteleport == true)
        {
            WormBrotherScript.canteleport = false;
            collision.gameObject.transform.position = WormBrother.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canteleport = true;
    }
}
