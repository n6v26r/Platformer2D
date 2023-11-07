using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHole : MonoBehaviour
{
    public SoundManger SoundManager;
    public GameObject WormBrother;
    private WormHole WormBrotherScript;
    private bool canteleport = true;

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    void Awake(){
        if(WormBrother != null)
            WormBrotherScript = WormBrother.GetComponent<WormHole>();
        SoundManager = FindAnyObjectByType<SoundManger>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canteleport == true && WormBrother != null)
        {
            SoundManager?.PlaySound(SoundManager.WormHoleEnter);
            WormBrotherScript.canteleport = false;
            collision.gameObject.transform.position = new Vector3(WormBrother.transform.position.x, WormBrother.transform.position.y);
         }
            StartCoroutine(Open());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(Close());
        canteleport = true;
    }

    public IEnumerator Open()
    {
        spriteRenderer.sprite = sprites[0];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[2];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[3];
    }

    public IEnumerator Close()
    {
        spriteRenderer.sprite = sprites[3];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[2];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[0];
    }
}
