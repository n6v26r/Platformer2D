using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDartShooter : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;
    //HideInInspector
    private bool ShouldShoot = false;
    private bool CanShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldShoot == true && CanShoot == true)
        {
            StartCoroutine(Shoot());
            ShouldShoot = false;
        }
    }

    public void ShouldShootTrue()
    {
        ShouldShoot = true;
    }

    private IEnumerator Shoot()
    {
        CanShoot = false;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[2];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[3];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[4];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[5];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[6];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = sprites[2];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.sprite = sprites[0];
        CanShoot = true;
    }
}