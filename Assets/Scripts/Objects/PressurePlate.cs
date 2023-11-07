using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] traps;
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private FireDartShooter fireDartShooter;
    private TNT tnt;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.sprite = sprites[1];

        for(int i = 0; i<traps.Length; ++i)
        {
            if (traps[i] != null)
            {
                if (traps[i].tag == "FireDartShooter")
                {
                    fireDartShooter = traps[i].GetComponent<FireDartShooter>();
                    fireDartShooter.AllowShoot();
                }
                else if (traps[i].tag == "TNT")
                {
                    tnt = traps[i].GetComponent<TNT>();
                    tnt.Blow();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sprite = sprites[0];
    }
}
