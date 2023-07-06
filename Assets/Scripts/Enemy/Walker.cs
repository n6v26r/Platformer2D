using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : PrototypeEnemyAI
{
    protected Vector2 Direction;
    protected float DistanceToWall;
    protected float RememberPlayerTimer;

    protected BoxCollider2D SelfBoxCollider;

    [SerializeField] protected Vector2 PatrolDirection = Vector2.right;
     [SerializeField] protected float PatrolDistance = 10;
    [SerializeField] protected float RememberPlayerCounter = 3f;
    [SerializeField] protected float PatrolStopBeforeWall = 2;

    [SerializeField] protected bool Agro = true;

    // Start is called before the first frame update

    void Awake(){
        SelfRigidBody = GetComponent<Rigidbody2D>();
        SelfBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Agro){
            bool IsVictimVisible = FollowVictim();
            if(!IsVictimVisible){
                if(RememberPlayerTimer>RememberPlayerCounter){
                    Patrol();
                }
                else{
                    RememberPlayerTimer += Time.deltaTime;
                }
            }
            else{
                RememberPlayerTimer = 0;
                PatrolDirection = Direction;
            }
        }
        else
            Patrol();
        MoveTarget();
    }

    protected virtual bool FollowVictim(){
        if(Direction!=Vector2.zero){
            SetTarget(new Vector2(transform.position.x+Direction.x, transform.position.y+Direction.y));
            return true;
        }
        return false;
    }

    protected virtual void Patrol(){
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
        hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, (1<<3)+(1<<6)); // Ground
        if(hit.collider!=null)
        {
            return hit.distance;
        }
        return MaxDistance;
    }
}
