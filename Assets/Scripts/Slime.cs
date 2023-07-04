using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Walker
{

    protected float JumpCooldownTimer;
    protected bool CanJump;
    [SerializeField] protected float JumpPower = 300f;
    [SerializeField] protected float JumpCooldown = 3f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = StartPosition;
        SelfRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        IgnorePlayerTimer += Time.deltaTime;
        SetTarget(Victim.GetComponent<Transform>().position);

        bool IsVictimVisible = FollowVictim();
        if(!IsVictimVisible || IgnorePlayerTimer<=IgnorePlayerCounter){ // Patrol
            Debug.Log("Slime enters patrol");
            Patrol();
        }
        else{
            Debug.Log("Slime exists patrol");
            IgnorePlayerTimer = 0;
            PatrolDirection = Direction; 

        }
        MoveTarget();
    }

    protected override void Patrol(){
        if(DistanceToWall>PatrolStopBeforeWall) SetTarget(new Vector2(transform.position.x+PatrolDirection.x, transform.position.y));
        else{
            PatrolDirection = new Vector2(-PatrolDirection.x, 0);
            SetTarget(transform.position);
        }
    }

    void FixedUpdate() {
        JumpCooldownTimer += Time.fixedDeltaTime;

        if(CanJump && JumpCooldownTimer>JumpCooldown){ // Only for debug.
            Debug.Log("Applied Jump!");
            SelfRigidBody.AddForce(Vector2.up*JumpPower);
            CanJump = false;
            JumpCooldownTimer = 0f;
        }

        Direction = RaycastVictim();
        DistanceToWall = RaycastWall(PatrolDirection, PatrolDistance);
    }

    // STUPIIIIIID. 
    // !!!TODO: Change this as soon as possible.
    private void OnCollisionEnter2D(Collision2D other) {
        CanJump = true;
    }

}
