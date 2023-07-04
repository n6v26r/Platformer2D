using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellingSpike : PrototypeEnemyAI
{
    
    protected BoxCollider2D SelfBoxCollider;
    protected bool EntityBelow;
    protected bool IsFalling;
    protected bool DidHit;

    [SerializeField] protected float SecondsBeforeKO = 3;
    // Start is called before the first frame update
    void Awake(){
        SelfRigidBody = GetComponent<Rigidbody2D>();
        SelfBoxCollider = GetComponent<BoxCollider2D>(); 
    }
    
    void Start()
    {
        SelfRigidBody.bodyType = RigidbodyType2D.Dynamic;
        SelfRigidBody.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(EntityBelow){
            IsFalling = true;
            SelfRigidBody.gravityScale = 4;
            EntityBelow = false;
        }
    }

    void FixedUpdate(){
        RaycastHit2D below = Physics2D.BoxCast(SelfBoxCollider.bounds.center, SelfBoxCollider.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, Mathf.Infinity, (1<<3)+(1<<6)+(1<<7));
        if (below.collider != null){
            Debug.Log("Below!");
            if(below.collider.gameObject.layer != 3){
                Debug.Log("Entity below");
                EntityBelow = true;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D other){
        if(IsFalling){
            Debug.Log("HIT");
            DidHit = true;
            StartCoroutine(WaitForKO());
        }
    }
    
    IEnumerator WaitForKO(){
        yield return new WaitForSeconds(SecondsBeforeKO);
        Destroy(gameObject);
    }
}
