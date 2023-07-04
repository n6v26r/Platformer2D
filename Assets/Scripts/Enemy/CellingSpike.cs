using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellingSpike : PrototypeEnemyAI
{
    public Action<GameObject> OnSpikeHit;

    protected BoxCollider2D SelfBoxCollider;
    protected bool EntityBelow;
    protected bool IsFalling;

    [SerializeField] protected float SecondsBeforeKO = 3;
    // Start is called before the first frame update
    void Awake()
    {
        SelfRigidBody = GetComponent<Rigidbody2D>();
        SelfBoxCollider = GetComponent<BoxCollider2D>(); 
    }
    
    void Start()
    {
        SelfRigidBody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        if(EntityBelow){
            IsFalling = true;
            SelfRigidBody.bodyType = RigidbodyType2D.Dynamic;
            EntityBelow = false;
        }
    }

    void FixedUpdate(){
        RaycastHit2D below = Physics2D.BoxCast(SelfBoxCollider.bounds.center, SelfBoxCollider.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, Mathf.Infinity, (1<<3)+(1<<6)+(1<<7));
        if (below.collider != null){
            if(below.collider.gameObject.layer != 3){
                EntityBelow = true;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D other){
        if(IsFalling == true){
            IsFalling = false;
            StartCoroutine(WaitForKO());
            OnSpikeHit(gameObject);
        }
    }
    
    IEnumerator WaitForKO(){
        yield return new WaitForSeconds(SecondsBeforeKO);
        Destroy(gameObject);
    }
}
