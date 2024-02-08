using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    private enum State{moving, jumping, falling};

    State status;
    public GameObject player;
    public Vector3 startpoz;

    public AnimationCurve jump;
    public AnimationCurve fall;
    public AnimationCurve follow;
    public int switchStat;
    public float timer;
    public float xSpeed;
    public float jumpheight;
    public float maxDis = 50f;
    [SerializeField] private LayerMask lmPlatfrom;
    public float extraHeightText = 10f;
    public float invTime = 0f;
    BoxCollider2D boxcl2D;

    Vector3 pozChange;
    float remain;
    float distance;
    float iFrame;
    
    void Start()
    {
        boxcl2D = GetComponent<BoxCollider2D>();
        status = State.moving;
        switchStat = Random.Range(6, 10);
        timer = 0;
        distance = 0;
        if(player == null)
            player = GameObject.Find("Player");

       startpoz = transform.position;
    }

    void Update()
    {
        switch (status) {
            case State.moving:
                pozChange = (transform.position - player.transform.position).normalized; 
                pozChange = new Vector3(pozChange.x * xSpeed * Time.deltaTime, pozChange.y * 0, pozChange.z * 0);

                if(!(pozChange.x > 0 && Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.left, extraHeightText, lmPlatfrom))
                    && !(pozChange.x < 0 && Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.right, extraHeightText, lmPlatfrom))
                    && Mathf.Abs(pozChange.x) < 15){ 
                    transform.position -= pozChange;
                }

                if (timer >= switchStat) {
                    status = State.jumping;
                    remain = 0;

                    distance = transform.position.x - player.transform.position.x;
                    if(transform.position.x > player.transform.position.x)
                        if (distance > maxDis)
                            distance = maxDis;
                    else 
                        if(distance < -maxDis)
                            distance = -maxDis;
                    startpoz = transform.position;
                }

                timer += Time.deltaTime;
                break;

            case State.jumping:
                if (remain >= 1) {
                    status = State.falling;
                }

                //Debug.Log(Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.left, extraHeightText, lmPlatfrom));

                transform.position = new Vector3(transform.position.x, startpoz.y + jump.Evaluate(remain) * jumpheight, startpoz.y);
                if (!(Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.left, extraHeightText, lmPlatfrom) ||
                      Physics2D.BoxCast(boxcl2D.bounds.center, boxcl2D.bounds.size - new Vector3(0, 0.1f, 0), 0f, Vector2.right, extraHeightText, lmPlatfrom))) {
                    transform.position = new Vector3(startpoz.x - follow.Evaluate(remain) * distance, transform.position.y, startpoz.y);
                }

                remain += Time.deltaTime * 1.3f;
                break;
            case State.falling:
                if(remain <= 0) {
                    status = State.moving;
                    switchStat = Random.Range(6, 8);
                    timer = remain = 0;
                }

                transform.position = new Vector3(transform.position.x, startpoz.y + fall.Evaluate(remain) * jumpheight, startpoz.y);

                remain -= Time.deltaTime * 2.6f;
                break;
        }


        if(iFrame > 0) {
            iFrame -= Time.deltaTime;
        }else 
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), boxcl2D, false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), boxcl2D, true);
            iFrame = invTime;
        }
    }
}
