using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sp;
    BoxCollider2D boxcl2D;
    Animator animator;
    [SerializeField] private LayerMask lm_platfrom;
    float extraHeightText = .1f;

    public PhysicsMaterial2D good;
    public PhysicsMaterial2D air;
    public GameObject ground;
    public GameObject spawnpoint;

    public GameObject healthbar;
    private Vector3 startpoz_health;

    public GameObject dashbar;
    private Vector3 startpoz_dash;

    float xinput, yinput;
    float jumped, onground;

    public float jumppower = 0f;
    public float acceleration = 0f;
    public float speedcap = 0f;
    public float HOLD_GRAVITY = 0f;
    public float BASE_GRAVITY = 0f;
    [SerializeField] float JUMPBUFFER = 0f;
    [SerializeField] float COYOTE_TIME = 0f;

    public float dash_cooldown = 4f;
    public float DASH_POWER = 20f;
    public float DASH_MAXAIRTIME = 0f;
    float dash_airtime;
    float dash_timer = 0f;
    int dash_dir = 1;

    public float FALLINGSPEED_WALLCLIMB = 1f;
    public float WALLJUMPPOWER = 390f;

    public bool dashing = false;

    public float emerald_power = 0;
    public float start_holdgrav = 0;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxcl2D = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = BASE_GRAVITY;
        startpoz_health = healthbar.transform.position;
        startpoz_dash = dashbar.transform.position;
        start_holdgrav = HOLD_GRAVITY;
    }

    // Update is called once per frame
    void Update() {
        xinput = Input.GetAxisRaw("Horizontal");
        yinput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            jumped = JUMPBUFFER;

        if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, extraHeightText, lm_platfrom)) {
            onground = COYOTE_TIME;
            ground.GetComponent<Rigidbody2D>().sharedMaterial = good;
            boxcl2D.sharedMaterial = good;
        } else {
            ground.GetComponent<Rigidbody2D>().sharedMaterial = air;
            boxcl2D.sharedMaterial = air;
        }

        if(dash_dir == 1)
            sp.flipX = true;
        else
            sp.flipX=false;

        dashbar.transform.position = startpoz_dash - new Vector3((dash_cooldown - dash_timer + 0.1f)*100, 0, 0);
        healthbar.transform.position = startpoz_health - new Vector3((100 - gameObject.GetComponent<Health>().health)*4.5f, 0, 0);
    
        if(jumped>0)
            animator.SetBool("IsJumping", true);
        else
            animator.SetBool("IsJumping", false);

        if(xinput!=0)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);

        if(jumped > 0) 
            animator.SetBool("IsMoving", false);
        

        if (GetComponent<Health>().health <= 0) {
            transform.position = spawnpoint.transform.position;
            GetComponent<Health>().health = 100;
            speedcap = 6;
            jumppower = 550;
            BASE_GRAVITY = 5;
        }
    }

    private void FixedUpdate() {
        if((xinput != 1 || (rb.velocity.x < speedcap )) && (xinput != -1 || (rb.velocity.x > -speedcap)))
            rb.AddForce(new Vector2(acceleration * xinput, 0));

        if (onground > 0) {
            if (jumped > 0) {
                onground = 0;
                jumped = 0;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumppower));
            }
        } else if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.left, extraHeightText, lm_platfrom)) {
            rb.velocity = new Vector2(rb.velocity.x, -FALLINGSPEED_WALLCLIMB);
            if (jumped > 0) {
                jumped = 0;
                rb.AddForce(new Vector2(WALLJUMPPOWER - 1, jumppower * 1.15f));
            }
        } else if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.right, extraHeightText, lm_platfrom)) {
            rb.velocity = new Vector2(rb.velocity.x, -FALLINGSPEED_WALLCLIMB);
            if (jumped > 0) {
                jumped = 0;
                rb.AddForce(new Vector2(-WALLJUMPPOWER + 1, jumppower * 1.15f));
            }
        }

        if (rb.velocity.y > 0 && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)))
            rb.gravityScale = HOLD_GRAVITY;
        else
            rb.gravityScale = BASE_GRAVITY;

        if (xinput != 0)
            dash_dir = Mathf.RoundToInt(xinput);

        if (Input.GetKey(KeyCode.LeftShift) && dash_timer >= dash_cooldown && dashing) {
            dash_timer = 0;
            dash_airtime = DASH_MAXAIRTIME;
        }


        if (dash_airtime > 0) {
            rb.velocity = new Vector2(transform.localScale.x * DASH_POWER * dash_dir, 0);

            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.freezeRotation = true;
            transform.rotation = Quaternion.identity;

            dash_airtime -= 0.1f;
        } else {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (jumped > 0)
            jumped -= 0.1f;

        if (onground > 0)
            onground -= 0.1f;

        if (dash_timer < dash_cooldown)
            dash_timer += 0.1f;

        if (rb.velocity.y < -15)
            rb.velocity = new Vector2(rb.velocity.x, -15);
    }
}
