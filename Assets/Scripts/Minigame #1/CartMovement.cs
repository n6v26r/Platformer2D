using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMovement : MonoBehaviour
{
    private Animator m_Animator;
    private bool ducking;

    [SerializeField] private LayerMask lm_platfrom;
    float extraHeightText = .1f;
    BoxCollider2D boxcl2D;

    private Rigidbody2D rb;
    public float JUMPPOWER = 0f;
    public float leftspeed = 1f;
    public float rightspeed = 3f;

    void Awake() {
        m_Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxcl2D = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, extraHeightText, lm_platfrom))
            rb.velocity = new Vector2(rb.velocity.x, JUMPPOWER);

        if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity = new Vector2(-leftspeed, rb.velocity.y);
        else if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity = new Vector2(rightspeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        if (Input.GetKey(KeyCode.DownArrow))
            ducking = true;
        else
            ducking = false;
        

        m_Animator.SetBool("Ducking", ducking);
    }
}
