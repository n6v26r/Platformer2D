using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySnapping : MonoBehaviour
{
    public BoxCollider2D bx;

    private void Awake() {
        bx = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + (collision.gameObject.transform.position.x < transform.position.x ? 1 : -1) * 0.1f, transform.position.y + bx.size.y*2, collision.gameObject.transform.position.y);
        }
    }
}
