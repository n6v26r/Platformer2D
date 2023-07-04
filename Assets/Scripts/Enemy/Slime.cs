using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Walker
{

    protected float JumpCooldownTimer;
    protected bool CanJump;
    [SerializeField] protected float JumpPower = 300f;
    [SerializeField] protected float JumpCooldown = 3f;

    void Awake(){
        SelfRigidBody = GetComponent<Rigidbody2D>();
        SelfBoxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate() {
        JumpCooldownTimer += Time.fixedDeltaTime;

        if(CanJump && JumpCooldownTimer>JumpCooldown){
            SelfRigidBody.AddForce(Vector2.up*JumpPower);
            CanJump = false;
            JumpCooldownTimer = 0f;
        }

        Direction = RaycastVictim();
        DistanceToWall = RaycastWall(PatrolDirection, PatrolDistance);


        if (Physics2D.BoxCast(SelfBoxCollider.bounds.center, SelfBoxCollider.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, .31f, (1<<3))){
            CanJump = true;
        }
    }
}
