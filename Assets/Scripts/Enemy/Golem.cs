using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Walker
{
    private bool StuffAbove;
    private bool IsLaunching;
    private float LaunchTimer;
    private bool JustLaunched;
    [SerializeField] private float LaunchPower = 500;
    [SerializeField] private float LaunchDelay = 1;
    [SerializeField] private float LaunchCooldown = 0.5F;
    void Start(){
        SelfRigidBody.bodyType = RigidbodyType2D.Kinematic;
    }
    
    void Update()
    {
        if(!StuffAbove){
            SelfRigidBody.bodyType = RigidbodyType2D.Kinematic;
            Patrol();
            MoveTarget();
        }
        else{
            SelfRigidBody.bodyType = RigidbodyType2D.Static;
        }
    }   

    void FixedUpdate(){
        LaunchTimer+=Time.fixedDeltaTime;
        SelfRigidBody.velocity = Vector2.zero;
        RaycastHit2D above = Physics2D.BoxCast(SelfBoxCollider.bounds.center, SelfBoxCollider.bounds.size, 0f, Vector2.up, 0.31f, (1<<3)+(1<<6)+(1<<7));
        if(above.collider!=null) {
            StuffAbove = true;
            if(!IsLaunching)
                if(LaunchTimer>LaunchCooldown){
                    StopCoroutine("Launch");
                    IsLaunching = true;
                    StartCoroutine(Launch(above.collider.gameObject));
                }
        }
        else {StuffAbove = false;}
        DistanceToWall = RaycastWall(PatrolDirection, PatrolDistance);
    }

    IEnumerator Launch(GameObject go){
        yield return new WaitForSeconds(LaunchDelay);
        RaycastHit2D above = Physics2D.BoxCast(SelfBoxCollider.bounds.center, SelfBoxCollider.bounds.size, 0f, Vector2.up, 0.31f, (1<<3)+(1<<6)+(1<<7));
        if(above.collider != null && above.collider.gameObject!=null && above.collider.gameObject == go)
            go.GetComponent<Rigidbody2D>().AddForce(Vector2.up*LaunchPower);
        IsLaunching = false;
        StartCoroutine(Launched());
        LaunchTimer = 0;
    }

    IEnumerator Launched(){
        JustLaunched = true;
        yield return new WaitForSeconds(0.2f);
        JustLaunched = false;
    }
}
