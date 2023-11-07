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

    private void Start()
    {
        SelfAnimator = GetComponent<Animator>();
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
        SelfAnimator.Play("ChargeUp");

        yield return new WaitForSeconds(0.01f); // Detect new animation
        yield return new WaitForSeconds(SelfAnimator.GetCurrentAnimatorStateInfo(0).length);

        if(transform.localScale.x == 1)
            Instantiate(FireDartRight, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.125f, transform.position.z), Quaternion.identity);
        else if(transform.localScale.x == -1)
            Instantiate(FireDartLeft, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.125f, transform.position.z), Quaternion.identity);
        
        SelfAnimator.Play("ChargeDown");

        yield return new WaitForSeconds(0.01f); // Detect new animation
        yield return new WaitForSeconds(SelfAnimator.GetCurrentAnimatorStateInfo(0).length);

        CanShoot = true;
    }
}
