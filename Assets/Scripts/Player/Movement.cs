using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Movement : MonoBehaviour
{
    private SoundManger SoundManager;
    public Action death;
    Rigidbody2D rb;
    SpriteRenderer sp;
    BoxCollider2D boxcl2D;
    private BoxCollider2D ppbx; 
    Animator animator;
    [SerializeField] private LayerMask lm_platfrom;
    float extraHeightText = .1f;

    public PhysicsMaterial2D good;
    public PhysicsMaterial2D air;
    public GameObject ground;
    public GameObject spawnpoint;
    public GameObject ScoreUI;

    public Image healthbar;

    public Image dashbar;
    private Vector3 startpoz_dash;
    private Liquid[] liquids;

    float xinput, yinput;
    float jumped, onground;

    public float jumppower = 0f;
    public float acceleration = 0f;
    public float speedcap = 0f;
    public float HOLD_GRAVITY = 0f;
    public float BASE_GRAVITY = 0f;
    [SerializeField] float JUMPBUFFER = 0f;
    [SerializeField] float COYOTE_TIME = 0f;
    public int extrajumps = 0;
    int jumpsleft;

    public float dash_cooldown = 4f;
    public float DASH_POWER = 20f;
    public float DASH_MAXAIRTIME = 0f;
    float dash_airtime;
    float dash_timer = 0f;
    int dash_dir = 1;

    public float FALLINGSPEED_WALLCLIMB = 1f;
    public float WALLJUMPPOWER = 390f;

    public bool dashing = false;

    public float start_holdgrav = 0;

    public static int score = 0;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxcl2D = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        SoundManager = FindAnyObjectByType<SoundManger>();

        liquids = FindObjectsOfType<Liquid>();
        for (int i = 0; i < liquids.Length; ++i)
        {
            liquids[i].OnLiquidStay2D += StayedInLiquid;
            liquids[i].OnLiquidExit += LeftLiquid;
        }
    }

    private void StayedInLiquid(GameObject gameObject, int Type)
    {
        if(Type == 1)
        {
            acceleration = 50;
            speedcap = 1;
            jumppower = 200;
            BASE_GRAVITY = 1.8f;

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(0)) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150);
            }
        }
        else if(Type == 2)
        {

        }
    }

    private void LeftLiquid(int Type)
    {
        if (Type == 1)
        {
            speedcap = 5;
            jumppower = 550;
            BASE_GRAVITY = 5;
        }
        else if (Type == 2)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = BASE_GRAVITY;
        start_holdgrav = HOLD_GRAVITY;
        healthbar.fillAmount = 1;
        jumpsleft = extrajumps;
    }

    // Update is called once per frame
    void Update() {
        xinput = Input.GetAxisRaw("Horizontal");
        yinput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            jumped = JUMPBUFFER;

        if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, extraHeightText, lm_platfrom)) {
            onground = COYOTE_TIME;
            jumpsleft = extrajumps;
            if (xinput == 0 || Math.Abs(rb.velocity.x) > speedcap) {
                ground.GetComponent<Rigidbody2D>().sharedMaterial = good;
                boxcl2D.sharedMaterial = good;
            } else {
                ground.GetComponent<Rigidbody2D>().sharedMaterial = air;
                boxcl2D.sharedMaterial = air;
            }
        } else {
            ground.GetComponent<Rigidbody2D>().sharedMaterial = air;
            boxcl2D.sharedMaterial = air;
        }



        if(dash_dir == 1)
            sp.flipX = true;
        else
            sp.flipX=false;

        dashbar.enabled = dashing;

        healthbar.fillAmount = Mathf.Clamp(gameObject.GetComponent<Health>().health / 100, 0, 1f);
        dashbar.fillAmount = Mathf.Clamp(dash_timer / dash_cooldown, 0, 1f);
        ScoreUI.GetComponent<TMP_Text>().text = "Score: " + score.ToString();

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
            if(death != null)
                death();
            transform.position = spawnpoint.transform.position;
            GetComponent<Health>().health = 100;
            speedcap = 6;
            jumppower = 550;
            BASE_GRAVITY = 5;
        }
    }

    private void FixedUpdate() {
        if ((xinput != 1 || (rb.velocity.x < speedcap)) && (xinput != -1 || (rb.velocity.x > -speedcap))) {
            rb.AddForce(new Vector2(acceleration * xinput, 0));
        }

        animator.SetBool("isWallcliming", false);
        if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.left, extraHeightText, lm_platfrom) && (!Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(0))) {
            rb.velocity = new Vector2(rb.velocity.x, -FALLINGSPEED_WALLCLIMB);
            if (jumped > 0) {
                jumped = 0;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(WALLJUMPPOWER, jumppower * 1.15f));
                SoundManager.PlaySound(SoundManager.PlayerJump);
            }
            animator.SetBool("isWallcliming", true);
        } else if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.right, extraHeightText, lm_platfrom) && (!Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(0))) {
            rb.velocity = new Vector2(rb.velocity.x, -FALLINGSPEED_WALLCLIMB);
            if (jumped > 0) {
                jumped = 0;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(-WALLJUMPPOWER, jumppower * 1.15f));
                SoundManager.PlaySound(SoundManager.PlayerJump);
            }
            animator.SetBool("isWallcliming", true);
        } else if (onground > 0 || jumpsleft > 0) {
            if (jumped > 0) {
                if (onground == 0)
                    jumpsleft--;
                onground = 0;
                jumped = 0;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumppower));
                SoundManager.PlaySound(SoundManager.PlayerJump);
            }
        }

        if (rb.velocity.y > 0 && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
            rb.gravityScale = HOLD_GRAVITY;
        else
            rb.gravityScale = BASE_GRAVITY;

        if (xinput != 0)
            dash_dir = Mathf.RoundToInt(xinput);

        if ((Input.GetKey(KeyCode.LeftShift )|| Input.GetMouseButton(1)) && dash_timer >= dash_cooldown && dashing) {
            SoundManager.PlaySound(SoundManager.PlayerDash);
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "pressure plate") {
            ppbx = collision.gameObject.GetComponent<BoxCollider2D>();
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, collision.gameObject.transform.position.y+ppbx.size.y*2.7f, gameObject.transform.position.z);
        }

        if (collision.gameObject.tag == "spike") {
            SoundManager.PlaySound(SoundManager.CellingSpike);
            GetComponent<Health>().health = 0;
        }
    }
}
