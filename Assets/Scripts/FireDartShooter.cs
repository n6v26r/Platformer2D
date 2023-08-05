using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDartShooter : MonoBehaviour
{
    public GameObject FireDartRight;
    public GameObject FireDartLeft;
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private bool ShouldShoot = false;
    private bool CanShoot = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(ShouldShoot == true && CanShoot == true)
        {
            StartCoroutine(Shoot());
            ShouldShoot = false;
        }
    }

    public void AllowShoot()
    {
        ShouldShoot = true;
    }

    private IEnumerator Shoot()
    {
        CanShoot = false;
        // TODO @rrradu: For the love of god, please put a animator, this hurts my eyes!!!
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
        if(transform.localScale.x == 1)
            Instantiate(FireDartRight, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.125f, transform.position.z), Quaternion.identity);
        else if(transform.localScale.x == -1)
            Instantiate(FireDartLeft, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.125f, transform.position.z), Quaternion.identity);
        spriteRenderer.sprite = sprites[2];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.sprite = sprites[0];
        CanShoot = true;
    }
}
