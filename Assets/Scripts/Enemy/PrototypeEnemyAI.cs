using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeEnemyAI : MonoBehaviour
{
    public float Speed = 2f;
    public GameObject Victim;

    [SerializeField] protected Vector2 Target;
    protected Rigidbody2D SelfRigidBody;

    private void Awake(){
        SelfRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetTarget(Victim.GetComponent<Transform>().position);
        MoveTarget();
    }   

    protected virtual void MoveTarget(){
        transform.position = Vector2.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
    }

    protected void SetTarget(Vector2 NewTarget){Target = NewTarget;}

}
