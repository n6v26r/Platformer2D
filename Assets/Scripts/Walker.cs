using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : PrototypeEnemyAI
{
    public float JumpPower = 300f;

    protected Vector2 Direction;
    protected float DistanceToWall; 
    [SerializeField] protected Vector2 PatrolDirection = Vector2.right;
    [SerializeField] protected float PatrolDistance = 10;

    [SerializeField] protected float PatrolStopBeforeWall = 2;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = StartPosition;
        Rigidbody2D SelfRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetTarget(Victim.GetComponent<Transform>().position);
        
        bool IsVictimVisible = FollowVictim();
        if(!IsVictimVisible){ // Patrol
            Debug.Log("Patrol");
            Patrol();
        }
        else
            PatrolDirection = Direction;
        MoveTarget();
        //if(CanJump) SelfRigidBody.AddForce(Vector3.up * JumpPower); 
    }   

    bool FollowVictim(){
        if(Direction!=Vector2.zero){
            SetTarget(new Vector2(transform.position.x+Direction.x, transform.position.y+Direction.y));
            return true;
        }
        return false;
    }

    void Patrol(){
        if(DistanceToWall>PatrolStopBeforeWall) SetTarget(new Vector2(transform.position.x+PatrolDirection.x, transform.position.y));
        else{
            PatrolDirection = new Vector2(-PatrolDirection.x, 0);
            SetTarget(transform.position);
        }
    }

    void FixedUpdate() {
        Direction = RaycastVictim();
        DistanceToWall = RaycastWall(PatrolDirection, PatrolDistance);
    }

    protected Vector2 RaycastVictim(){
        RaycastHit2D hit;
        for (float i = 0; i<=1; i+=0.1f){
            hit = Physics2D.Raycast(transform.position, new Vector2(1-i, i), Mathf.Infinity, (1<<6)+(1<<3)); // Player and ground
            if(hit.collider!=null)
            {
                if(hit.collider.gameObject == Victim)
                    return Vector2.right;
            }

            hit = Physics2D.Raycast(transform.position, new Vector2(-1+i, i), Mathf.Infinity, (1<<6)+(1<<3));
            if(hit.collider!=null)
            {
                if(hit.collider.gameObject == Victim)
                    return Vector2.left;
            }
        }
        return Vector2.zero;
    }

    protected float RaycastWall(Vector2 direction, float MaxDistance=Mathf.Infinity){
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, (1<<3)); // Ground
        if(hit.collider!=null)
        {
            return hit.distance;
        }
        return MaxDistance;
    }

    /*private void OnCollisionEnter2D(Collision2D other) {
        CanJump = true;
    }
    private void OnCollisionExit2D(Collision2D other) {
        CanJump = false;
    } */

}
