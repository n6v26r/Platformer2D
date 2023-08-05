using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBlocks : MonoBehaviour
{
    public GameObject gemItem;
    public float timeRequired;
    
    private SoundManger SoundManager;
    private float timer;

    private void Awake()
    {
        SoundManager = FindAnyObjectByType<SoundManger>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            timer += Time.fixedDeltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer >= timeRequired)
        {
            gemItem.SetActive(true);
            Destroy(gameObject);
            SoundManager?.PlaySound(SoundManager.BlockBreak);
        }
    }
}
