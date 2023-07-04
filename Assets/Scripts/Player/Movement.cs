using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxcl2D;
    [SerializeField] private LayerMask lm_platfrom;
    float extraHeightText = .1f;

    public PhysicsMaterial2D good;
    public PhysicsMaterial2D air;
    public GameObject ground;

    public GameObject healthbar;
    private Vector3 startpoz_health;

    float xinput, yinput;
    float jumped, onground;

    public float jumppower = 0f;
    public float acceleration = 0f;
    public float speedcap = 0f;
    public float HOLD_GRAVITY = 0f;
    public float BASE_GRAVITY = 0f;
    [SerializeField] float JUMPBUFFER = 0f;
    [SerializeField] float COYOTE_TIME = 0f;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxcl2D = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = BASE_GRAVITY;
        startpoz_health = healthbar.transform.position;
    }

    // Update is called once per frame
    void Update() {
        xinput = Input.GetAxisRaw("Horizontal");
        yinput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.UpArrow))
            jumped = JUMPBUFFER;

        if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, extraHeightText, lm_platfrom)) {
            onground = COYOTE_TIME;
            ground.GetComponent<Rigidbody2D>().sharedMaterial = good;
            boxcl2D.sharedMaterial = good;
        } else {
            ground.GetComponent<Rigidbody2D>().sharedMaterial = air;
            boxcl2D.sharedMaterial = air;
        }
        
        healthbar.transform.position = startpoz_health - new Vector3((100 - gameObject.GetComponent<Health>().health)*3.3f, 0, 0);
    }

    private void FixedUpdate() {
        if((xinput != 1 || (rb.velocity.x < speedcap )) && (xinput != -1 || (rb.velocity.x > -speedcap)))
            rb.AddForce(new Vector2(acceleration * xinput, 0));

        if (jumped > 0 && onground > 0) {
            onground = 0;
            jumped = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumppower));
        }

        if (rb.velocity.y > 0 && Input.GetKey(KeyCode.UpArrow))
            rb.gravityScale = HOLD_GRAVITY;
        else
            rb.gravityScale = BASE_GRAVITY;

        if (jumped > 0)
            jumped -= 0.1f;

        if (onground > 0)
            onground -= 0.1f;

        if (rb.velocity.y < -15)
            rb.velocity = new Vector2(rb.velocity.x, -15);
    }
}
