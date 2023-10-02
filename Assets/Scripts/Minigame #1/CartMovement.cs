using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMovement : MonoBehaviour
{
    private Animator mAnimator;
    private bool ducking;

    [SerializeField] private LayerMask lmPlatfrom;
    float extraHeightText = .1f;
    BoxCollider2D boxCl2D;

    private Rigidbody2D rb;
    public float jumpPower = 0f;
    public float leftSpeed = 1f;
    public float rightSpeed = 3f;

    void Awake() {
        mAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCl2D = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Physics2D.BoxCast(boxCl2D.bounds.center, boxCl2D.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, extraHeightText, lmPlatfrom))
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity = new Vector2(-leftSpeed, rb.velocity.y);
        else if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity = new Vector2(rightSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        if (Input.GetKey(KeyCode.DownArrow))
            ducking = true;
        else
            ducking = false;
        

        mAnimator.SetBool("Ducking", ducking);
    }
}
