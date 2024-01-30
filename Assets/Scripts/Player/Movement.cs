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
    [SerializeField] private LayerMask lmPlatfrom;
    float extraHeightText = .1f;

    public PhysicsMaterial2D good;
    public PhysicsMaterial2D air;
    public GameObject ground;
    public GameObject scoreUI;

    private Vector3 startPozDash;
    private Liquid[] liquids;

    public GameObject dashbarRama;
    public GameObject jumpbarRama;

    public Image healthbar;
    public Image dashbar;
    public Image doublejump;

    public GameObject silverKey;
    public GameObject silverKeyText;
    public GameObject silverKeyRama;

    public GameObject goldenKey;
    public GameObject goldenKeyText;
    public GameObject goldenKeyRama;

    float xInput, yInput;
    float jumped, onGround;
    public int leftWall, rightWall;

    public float jumpPower = 0f;
    public float acceleration = 0f;
    public float speedCap = 0f;
    public float holdGravity = 0f;
    public float baseGravity = 0f;
    [SerializeField] float jumpBuffer = 0f;
    [SerializeField] float coyoteTime = 0f;
    public int extraJumps = 0;
    float jumpsLeft;
    float allowWallJump;

    public float dashCooldown = 4f;
    public float dashPower = 20f;
    public float dashMaxAirTime = 0f;
    float dashAirtime;
    float dashTimer = 0f;
    int dashDir = 1;

    public float fallingSpeedWallClimb = 1f;
    public float wallJumpPower = 390f;

    public bool dashing = false;

    public float startHoldGrav = 0;

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
            liquids[i].OnLiquidEnter += EnteredLiquid;
            liquids[i].OnLiquidStay2D += StayedInLiquid;
            liquids[i].OnLiquidExit += LeftLiquid;
        }

        if(ground == null)
            ground = GameObject.Find("Grid/Ground");
        if (scoreUI == null)
            scoreUI = GameObject.Find("Canvas/Misc./Score");

        if (dashbarRama == null)
            dashbarRama = GameObject.Find("Canvas/Dash/rama_dash");
        if (jumpbarRama == null)
            jumpbarRama = GameObject.Find("Canvas/Jump/rama_jump");

        if (healthbar == null)
            healthbar = GameObject.Find("Canvas/Health/Image").GetComponent<Image>();
        if (dashbar == null)
            dashbar = GameObject.Find("Canvas/Dash/Image").GetComponent<Image>();
        if (doublejump == null)
            doublejump = GameObject.Find("Canvas/Jump/Image").GetComponent<Image>();

        if (silverKey == null)
            silverKey = GameObject.Find("Canvas/Silver Key/silverkey");
        if (silverKeyText == null)
            silverKeyText = GameObject.Find("Canvas/Silver Key/silverkey_text");
        if (silverKeyRama == null)
            silverKeyRama = GameObject.Find("Canvas/Silver Key/rama_silver");

        if (goldenKey == null)
            goldenKey = GameObject.Find("Canvas/Golden key/goldenkey");
        if (goldenKeyText == null)
            goldenKeyText = GameObject.Find("Canvas/Golden key/goldenkey_text");
        if (goldenKeyRama == null)
            goldenKeyRama = GameObject.Find("Canvas/Golden key/rama_golden");
    }

    private void EnteredLiquid(GameObject gameObject, int Type)
    {
        if(Type == 1)
        {
            acceleration = 50;
            speedCap = 1;
            jumpPower = 200;
            baseGravity = 1.8f;
        }
        else if(Type == 2)
        {
            acceleration = 20;
            speedCap = 4;
            jumpPower = 250;
            baseGravity = 0.3f;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void StayedInLiquid(GameObject gameObject, int Type)
    {
        if(Type == 1)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(0)) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150);
            }
        }
        else if(Type == 2)
        {
            onGround = 1;
        }
    }

    private void LeftLiquid(int Type)
    {
        if (Type == 1)
        {
            acceleration = 50;
            speedCap = 5;
            jumpPower = 550;
            baseGravity = 5;
        }
        else if (Type == 2)
        {
            acceleration = 50;
            speedCap = 5;
            jumpPower = 550;
            baseGravity = 5;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = baseGravity;
        startHoldGrav = holdGravity;
        healthbar.fillAmount = 1;
        jumpsLeft = extraJumps;
        allowWallJump = 1;
    }

    // Update is called once per frame
    void Update() {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        //Registers the jump input
        //---
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            jumped = jumpBuffer;
        //---

        //Chechink if the player is on ground and it signals it in "onground"
        //it resets double jump and applies friction if not moving
        //---
        if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, extraHeightText, lmPlatfrom)) {
            onGround = coyoteTime;
            jumpsLeft = extraJumps;
            if (xInput == 0 || Math.Abs(rb.velocity.x) > speedCap) {
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
        //---

        //Check if the player is on a wall and signals it in "leftwall" and "rightwall"
        //---
        if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.left, extraHeightText, lmPlatfrom))
            rightWall = 1;
        else
            rightWall = 0;

        if (Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.right, extraHeightText, lmPlatfrom))
            leftWall = 1;
        else
            leftWall = 0;
        //---

        //Character flip
        //---
        if(dashDir == 1)
            sp.flipX = true;
        else
            sp.flipX=false;
        //---


        //UI
        //----
        //Dash bar
        if (dashbar != null) {
            dashbar.enabled = dashing;
            dashbar.fillAmount = Mathf.Clamp(dashTimer / dashCooldown, 0, 1f);
        }
        if(dashbarRama != null)
            dashbarRama.SetActive(dashing);

        ///Double jump bar
        if(jumpbarRama != null)
            if (extraJumps == 0)
                jumpbarRama.SetActive(false);
            else
                jumpbarRama.SetActive(true);

        if(doublejump != null)
            doublejump.fillAmount = Mathf.Clamp((jumpsLeft / extraJumps), 0, 1f);

        //Health, score
        if(scoreUI != null)
            scoreUI.GetComponent<TMP_Text>().text = "Score: " + score.ToString();

        if(healthbar != null)
            healthbar.fillAmount = Mathf.Clamp(gameObject.GetComponent<Health>().GetHealth() / 100, 0, 1f);
        //----


        if (jumped>0)
            animator.SetBool("IsJumping", true);
        else
            animator.SetBool("IsJumping", false);

        if(xInput!=0)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);

        if(jumped > 0)
            animator.SetBool("IsMoving", false);
    }

    private void FixedUpdate() {
        //Adds force when the player wants to walk
        //It caps the walking speed, but it doesn't hard-cap the movement speed
        //---
        if ((xInput != 1 || (rb.velocity.x < speedCap && leftWall == 0)) && (xInput != -1 || (rb.velocity.x > -speedCap && rightWall == 0))) 
            rb.AddForce(new Vector2(acceleration * xInput, 0));
        //---

        animator.SetBool("isWallcliming", false);
        if (((rightWall == 1) && (allowWallJump == 1 && onGround <= 0))) {//If on a wall on the right
            rb.velocity = new Vector2(rb.velocity.x, -fallingSpeedWallClimb);//Grabs*
            if (jumped > 0) {//Jumps off
                jumped = 0;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(wallJumpPower, jumpPower * 1.15f));
                SoundManager?.PlaySound(SoundManager.PlayerJump);
            }
            animator.SetBool("isWallcliming", true);
        } else if (((leftWall == 1) && (allowWallJump == 1 && onGround <=0))) {//If on a wall on the left
            rb.velocity = new Vector2(rb.velocity.x, -fallingSpeedWallClimb);//Grabs*
            if (jumped > 0) {//Jumps off
                jumped = 0;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(-wallJumpPower, jumpPower * 1.15f));
                SoundManager?.PlaySound(SoundManager.PlayerJump);
            }
            animator.SetBool("isWallcliming", true);
        } else if (onGround > 0 || (jumpsLeft > 0)) {//if grounded(or has double jumps) 
            if (jumped > 0) {//and if pressing the jump input
                if (onGround <= 0)
                    jumpsLeft--;
                onGround = 0;
                jumped = 0;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpPower));
                SoundManager?.PlaySound(SoundManager.PlayerJump);
            }
            allowWallJump = 0;
        }
        if (onGround <= 0 && leftWall == 0 && rightWall == 0)
            allowWallJump = 1;

        //Allows exetended jump 
        //---
        if (rb.velocity.y > 0 && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
            rb.gravityScale = holdGravity;
        else
            rb.gravityScale = baseGravity;
        //---

        if (xInput != 0)
            dashDir = Mathf.RoundToInt(xInput);

        if ((Input.GetKey(KeyCode.LeftShift )|| Input.GetMouseButton(1)) && dashTimer >= dashCooldown && dashing) {
            SoundManager?.PlaySound(SoundManager.PlayerDash);
            dashTimer = 0;
            dashAirtime = dashMaxAirTime;
        }


        if (dashAirtime > 0) {
            rb.velocity = new Vector2(transform.localScale.x * dashPower * dashDir, 0);

            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.freezeRotation = true;
            transform.rotation = Quaternion.identity;

            dashAirtime -= 0.1f;
        } else {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (jumped > 0)
            jumped -= 0.1f;

        if (onGround > 0)
            onGround -= 0.1f;

        if (dashTimer < dashCooldown)
            dashTimer += 0.1f;

        if (rb.velocity.y < -15)
            rb.velocity = new Vector2(rb.velocity.x, -15);
    }
}
