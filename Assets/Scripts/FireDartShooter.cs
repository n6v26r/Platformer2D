using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDartShooter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public GameObject FireDartRight;
    public GameObject FireDartLeft;

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
        if(transform.localScale.x == 1)
            Instantiate(FireDartRight, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.125f, transform.position.z), Quaternion.identity);
        else if(transform.localScale.x == -1)
            Instantiate(FireDartLeft, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.125f, transform.position.z), Quaternion.identity);
        spriteRenderer.sprite = sprites[2];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.sprite = sprites[0];
        CanShoot = true;
    }
}
