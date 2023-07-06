using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SoundManger SoundManager;

    public List<GameObject> Checkpoints = new List<GameObject>();
    public Sprite On;
    public Sprite Off;
    SpriteRenderer sp;
    int i;

    private void Awake() {
        sp = GetComponent<SpriteRenderer>();
        SoundManager = FindAnyObjectByType<SoundManger>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player" && sp.sprite == Off) {
            SoundManager.PlaySound(SoundManager.flame);
            collision.GetComponent<Movement>().spawnpoint.transform.position = transform.position;
            sp.sprite = On;
            for (i = 0; i < Checkpoints.Count; i++)
                Checkpoints[i].GetComponent<SpriteRenderer>().sprite = Off;
        }
    }
}
