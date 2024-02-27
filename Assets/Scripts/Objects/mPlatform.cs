using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mPlatform : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject>();
    public List<int> speed = new List<int>();
    public List<int> start_up = new List<int>();
    public Vector3 launchdir;
    public static float launchspeed = 1.1f;

    private int dir, start;
    public int poz;

    // Start is called before the first frame update
    void Start()
    {
        poz = 0;
        start = start_up[0];
    }

    // Update is called once per frame
    void Update() {
        if (start != 0) {
            if (Vector2.Distance(transform.position, points[poz].transform.position) <= 0.02f) {
                poz = (poz + 1) % points.Count;
                start = start_up[poz];
            }

            transform.position = Vector3.MoveTowards(transform.position, points[poz].transform.position, speed[poz] * Time.deltaTime);
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            launchdir = (points[poz].transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(launchdir.x * speed[poz] * 1f, launchdir.y * speed[poz] * 0.9f);
            collision.gameObject.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
            collision.transform.SetParent(null);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (collision.gameObject.GetComponent<Movement>().dashAirtime <= 0) {
                start = 1;
                collision.gameObject.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
                collision.transform.SetParent(transform);
            } else {
                collision.transform.SetParent(null);
            }
        }
    }

    IEnumerator wait(float seconds) {
        yield return new WaitForSeconds(seconds);
    }
}
