using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite On;
    SpriteRenderer sp;

    private void Awake() {
        sp = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            collision.GetComponent<Movement>().spawnpoint.transform.position = transform.position;
            sp.sprite = On;
        }
    }
}
