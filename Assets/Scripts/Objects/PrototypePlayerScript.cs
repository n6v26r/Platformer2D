using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypePlayerScript : MonoBehaviour
{
    public float Speed = 2f;
    public float JumpPower = 100;
    public Vector3 StartPosition = new Vector3(0, 0, 0);
    [SerializeField] private bool CanJump = false;

    // Start is called before the first frame update
    private Rigidbody2D SelfRigidBody;
    void Start()
    {
        transform.position = StartPosition;
        SelfRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){transform.position = new Vector3(transform.position.x+Speed*Time.deltaTime, transform.position.y, transform.position.z);}
        if(Input.GetKey(KeyCode.S)){transform.position = new Vector3(transform.position.x-Speed*Time.deltaTime, transform.position.y, transform.position.z);}
    }   

    void FixedUpdate(){
        if(CanJump && Input.GetKeyDown(KeyCode.Space)){
            SelfRigidBody.AddForce(Vector3.up * JumpPower); 
            CanJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        CanJump = true;
    }

}
