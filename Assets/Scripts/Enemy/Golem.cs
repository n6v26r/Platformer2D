using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golemn : Walker
{
    public bool StuffAbove;
    void Update()
    {
        if(!StuffAbove)
            Patrol();
            MoveTarget();
    }   

    void FixedUpdate(){
        RaycastHit2D above = Physics2D.BoxCast(SelfBoxCollider.bounds.center, SelfBoxCollider.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.up, 0.31f, (1<<3)+(1<<6)+(1<<7));
        if(above.collider!=null) StuffAbove = true;
        else StuffAbove = false;
    }
}
