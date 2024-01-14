using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public List<GameObject> Checkpoints = new List<GameObject>();
    public GameObject DeathManager;
    public Sprite On;
    public Sprite Off;

    private SoundManger SoundManager;
    private SpriteRenderer sp;

    private void Awake() {
        sp = GetComponent<SpriteRenderer>();
        SoundManager = FindAnyObjectByType<SoundManger>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player" && sp.sprite == Off) {
            DeathManager.GetComponent<DeathManager>().spawnpoint.transform.position = transform.position;
            sp.sprite = On;
            for (int i = 0; i < Checkpoints.Count; i++)
                Checkpoints[i].GetComponent<SpriteRenderer>().sprite = Off;
            SoundManager?.PlaySound(SoundManager.flame);
        }
    }
}
