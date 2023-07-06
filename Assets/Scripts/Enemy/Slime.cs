using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Walker
{


    public Action<GameObject> OnSlimeHit;
    protected float JumpCooldownTimer;
    protected float DamageTimer;
    protected bool CanJump;
    [SerializeField] protected float JumpPower = 300f;
    [SerializeField] protected float JumpCooldown = 3f;
    [SerializeField] protected float DamageCoolDown = 0.2f;
    void FixedUpdate() {
        Debug.Log(DamageTimer);
        DamageTimer += Time.fixedDeltaTime;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player") return;
        if(DamageTimer <= DamageCoolDown) return;
        if(OnSlimeHit!=null)
            OnSlimeHit(collision.gameObject);
        DamageTimer = 0;
    }
}
