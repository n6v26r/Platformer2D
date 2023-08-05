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
    [SerializeField] protected float DamageCoolDown = 0.3f;
    private void FixedUpdate() {
        DamageTimer += Time.fixedDeltaTime;
        JumpCooldownTimer += Time.fixedDeltaTime;

        if(CanJump && JumpCooldownTimer>JumpCooldown){
            if(FollowVictim())
                SoundManager.PlaySound(SoundManager.SlimeJump);
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
        OnSlimeHit?.Invoke(collision.gameObject);
        DamageTimer = 0;
    }

    protected override float RaycastWall(Vector2 direction, float MaxDistance=Mathf.Infinity){
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, (1<<3)+(1<<6)); // Ground
        if(hit.collider!=null)
        {
            if(hit.collider.gameObject.tag != "Player")
                return hit.distance;
        }
        return MaxDistance;
    }
}
