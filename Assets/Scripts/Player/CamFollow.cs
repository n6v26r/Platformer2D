using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject Entity;
    public Vector3 offset;
    public Vector2 EntityAllowedFreeMovementSpace = new Vector2(2, 2);
    public float StopFollowOnEntityStill = 0.2f;
    public float Speed = 5f;

    private Vector2 lastEntityPosition;
    private float EntityStillCooldown;
    private void Update() {
        if(Entity==null){
            Debug.Log("<color=yellow>[CamFollow WARN]: No entity specified!</color>"); 
            return;
        }
        
        Vector2 EntityPos = Entity.transform.position - offset;
        Vector3 MoveTo = gameObject.transform.position;
        if(EntityPos==lastEntityPosition)
            EntityStillCooldown+=Time.deltaTime;
        else
            EntityStillCooldown = 0f;
        
        Vector2 Pos2D = gameObject.transform.position;
        if(EntityStillCooldown>StopFollowOnEntityStill && Mathf.Abs(EntityPos.x-Pos2D.x)<EntityAllowedFreeMovementSpace.x && Mathf.Abs(EntityPos.y-Pos2D.y)<EntityAllowedFreeMovementSpace.y){
            EntityPos = lastEntityPosition;
        }
        
        Vector2 MoveTo2D = Vector2.MoveTowards(MoveTo, EntityPos, Speed*Time.deltaTime);
        MoveTo = new Vector3(MoveTo2D.x, MoveTo2D.y, MoveTo.z);
        
        gameObject.transform.position = MoveTo;
        lastEntityPosition = EntityPos;
    }
}
