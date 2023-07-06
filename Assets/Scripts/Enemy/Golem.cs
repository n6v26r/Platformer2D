using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Walker
{

    private Animator SelfAnimator;
    private SpriteRenderer SelfSpriteRenderer;
    private bool StuffAbove;
    private bool IsLaunching;
    private bool JustLaunched;
    [SerializeField] private float LaunchPower = 500;
    [SerializeField] private float LaunchDelay = 1;

    void Awake(){
        SelfAnimator = GetComponent<Animator>();
        SelfRigidBody = GetComponent<Rigidbody2D>();
        SelfBoxCollider = GetComponent<BoxCollider2D>();
        SelfSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start(){
        SelfRigidBody.bodyType = RigidbodyType2D.Kinematic;
    }
     
    void Update()
    {  
        SelfAnimator.SetBool("IsLaunching", IsLaunching);
        SelfAnimator.SetBool("Launched", JustLaunched);

        if(!StuffAbove){
            SelfRigidBody.bodyType = RigidbodyType2D.Kinematic;

            if(PatrolDirection.x<0)
                SelfSpriteRenderer.flipX = true;
            else if(PatrolDirection.x>0)
                SelfSpriteRenderer.flipX = false;

            Patrol();
            MoveTarget();
        }
        else{
            SelfRigidBody.bodyType = RigidbodyType2D.Static;
        }
    }   

    void FixedUpdate(){
        RaycastHit2D above = Physics2D.BoxCast(SelfBoxCollider.bounds.center, SelfBoxCollider.bounds.size, 0f, Vector2.up, 0.31f, (1<<3)+(1<<6)+(1<<7));
        if(above.collider!=null) {
            StuffAbove = true;
            if(!IsLaunching)
                StopCoroutine("Launch");
                IsLaunching = true;
                StartCoroutine(Launch(above.collider.gameObject));
        }
        else {StuffAbove = false;}
        DistanceToWall = RaycastWall(PatrolDirection, PatrolDistance);
    }

    IEnumerator Launch(GameObject go){
        yield return new WaitForSeconds(LaunchDelay);
        RaycastHit2D above = Physics2D.BoxCast(SelfBoxCollider.bounds.center, SelfBoxCollider.bounds.size, 0f, Vector2.up, 0.31f, (1<<3)+(1<<6)+(1<<7));
        if(above.collider != null && above.collider.gameObject!=null && above.collider.gameObject == go){
            go.GetComponent<Rigidbody2D>().AddForce(Vector2.up*LaunchPower);
        }
        IsLaunching = false;
        StartCoroutine(Launched());
    }

    IEnumerator Launched(){
        JustLaunched = true;
        yield return new WaitForSeconds(0.2f);
        JustLaunched = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider == null || other.collider.gameObject == null) return;
        Rigidbody2D rb = other.collider.gameObject.GetComponent<Rigidbody2D>();
        if(rb == null) return;
        rb.velocity = Vector2.zero;
    }
}
