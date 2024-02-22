using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDartShooter : MonoBehaviour
{
    public GameObject FireDartRight;
    public GameObject FireDartLeft;

    private Animator SelfAnimator;
    private bool ShouldShoot = false;
    private bool CanShoot = true;

    public float delay = 0f;
    public float timer = 0f;

    private void Start()
    {
        SelfAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(delay <= timer && CanShoot == true)
        {
            StartCoroutine(Shoot());
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    public void AllowShoot()
    {
        timer = delay;
    }

    private IEnumerator Shoot()
    {
        CanShoot = false;
        SelfAnimator.Play("ChargeUp");

        yield return new WaitForSeconds(0.02f); // Detect new animation
        yield return new WaitForSeconds(SelfAnimator.GetCurrentAnimatorStateInfo(0).length);

        Instantiate(FireDartRight, new Vector3(transform.position.x, transform.position.y, transform.position.z), gameObject.transform.rotation);

        SelfAnimator.Play("ChargeDown");

        yield return new WaitForSeconds(0.02f); // Detect new animation
        yield return new WaitForSeconds(SelfAnimator.GetCurrentAnimatorStateInfo(0).length);

        CanShoot = true;
    }
}
