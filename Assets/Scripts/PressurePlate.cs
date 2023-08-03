using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private FireDartShooter fireDartShooter;
    private TNT tnt;

    public GameObject[] traps;
    public Sprite[] sprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        
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
                    fireDartShooter.ShouldShootTrue();
                }
                else if (traps[i].tag == "TNT")
                {
                    tnt = traps[i].GetComponent<TNT>();
                    tnt.Blow();
                }
            }
        }
        Debug.Log("Press Me Daddy UWU");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sprite = sprites[0];
        Debug.Log("Why Not Pressing Anymore :(");
    }
}
