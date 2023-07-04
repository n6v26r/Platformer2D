using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golemn : Walker
{


    // Update is called once per frame
    void Update()
    {
        bool IsVictimVisible = FollowVictim();
        if(!IsVictimVisible){
            if(RememberPlayerTimer>RememberPlayerCounter){
                //Debug.Log("Entity not visible! PATROL MODE");
                Patrol();
            }
            else{
                RememberPlayerTimer += Time.deltaTime;
            }
        }
        else{
            RememberPlayerTimer = 0;
           // Debug.Log("Entity is in follow mode!");
            PatrolDirection = Direction;
        }
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
        hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, (1<<3)); // Ground
        if(hit.collider!=null)
        {
            return hit.distance;
        }
        return MaxDistance;
    }
}
