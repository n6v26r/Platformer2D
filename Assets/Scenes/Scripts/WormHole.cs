using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHole : MonoBehaviour
{
    public SoundManger SoundManager;
    public GameObject WormBrother;
    private WormHole WormBrotherScript;
    private bool canteleport = true;

    void Awake(){
        WormBrotherScript = WormBrother.GetComponent<WormHole>();
        SoundManager = FindAnyObjectByType<SoundManger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canteleport == true)
        {
            SoundManager.PlaySound(SoundManager.WormHoleEnter);
            WormBrotherScript.canteleport = false;
            collision.gameObject.transform.position = new Vector3(WormBrother.transform.position.x, WormBrother.transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canteleport = true;
    }
}
