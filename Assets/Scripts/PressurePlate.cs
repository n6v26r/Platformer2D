using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private FireDartShooter fireDartShooter;

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
        if (collision.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = sprites[1];

            for(int i = 0; i<traps.Length; ++i)
            {
                if (traps[i].tag == "FireDartShooter")
                {
                    Debug.Log("Press Me Daddy UWU");
                    fireDartShooter = traps[i].GetComponent<FireDartShooter>();
                    fireDartShooter.ShouldShootTrue();
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = sprites[0];
            Debug.Log("Why Not Pressing Anymore :(");
        }
    }
}
