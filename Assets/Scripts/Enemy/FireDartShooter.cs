using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDartShooter : MonoBehaviour
{
    public GameObject FireDartRight;
    public GameObject FireDartLeft;
    public GameObject bullet;

    private Animator SelfAnimator;
    private bool ShouldShoot = false;
    private bool CanShoot = true;
    private int stop = 0;

    public float waitTime = 0f;
    public float delay = 0f;
    public float timer = 0f;
    public float speed = 0f;

    private void Start()
    {
        SelfAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (waitTime <= 0) {
            if (delay <= timer && CanShoot == true) {
                StartCoroutine(Shoot());
                timer = 0f;
            }
            if (stop == 0)
                timer += Time.deltaTime;
        } else {
            waitTime -= Time.deltaTime;
        }
    }

    public void AllowShoot()
    {
        timer = delay;
    }

    private IEnumerator Shoot()
    {
        stop = 1;
        SelfAnimator.Play("ChargeUp");

        yield return new WaitForSeconds(0.02f); // Detect new animation
        yield return new WaitForSeconds(SelfAnimator.GetCurrentAnimatorStateInfo(0).length);

        bullet = Instantiate(FireDartRight, new Vector3(transform.position.x, transform.position.y, transform.position.z), gameObject.transform.rotation);
        bullet.GetComponent<FireDart>().Speed = speed;
        bullet.transform.localScale = transform.localScale;

        SelfAnimator.Play("ChargeDown");

        yield return new WaitForSeconds(0.01f); // Detect new animation
        yield return new WaitForSeconds(SelfAnimator.GetCurrentAnimatorStateInfo(0).length);
        stop = 0;
    }
}
