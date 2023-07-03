using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeEnemyAI : MonoBehaviour
{
    public float Speed = 2f;
    public Vector3 StartPosition = new Vector3(0, 0, 0);

    [SerializeField] protected Vector2 Target;
    public GameObject Victim;

    // Start is called before the first frame update
    protected  Rigidbody2D SelfRigidBody;
    void Start()
    {
        transform.position = StartPosition;
        SelfRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        SetTarget(Victim.GetComponent<Transform>().position);
        MoveTarget();
    }   

    // Will Infinetly target the player
    // Stupid and Goofy
    protected virtual void MoveTarget(){
        transform.position = Vector2.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
    }

    protected void SetTarget(Vector2 NewTarget){Target = NewTarget;}

}
