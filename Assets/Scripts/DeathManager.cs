using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private SoundManger SoundManager;
    [SerializeField] private LavaBlock lavaBlock;
    [SerializeField] private Movement playermovement;
    private CellingSpike[] cellingSpikes;
    private Slime[] slimes;

    void Awake()
    {
        SoundManager = FindAnyObjectByType<SoundManger>();

        lavaBlock.OnLavaStay2D += StayedInLava;
        lavaBlock.OnLavaExit += LeftLava;
        cellingSpikes = FindObjectsOfType<CellingSpike>();
        for (int i = 0; i < cellingSpikes.Length; ++i)
            cellingSpikes[i].OnSpikeHit+= SpikeHit;

        slimes = FindObjectsOfType<Slime>();
        for (int i = 0; i < slimes.Length; ++i)
            slimes[i].OnSlimeHit += SlimeHit;
    }

    private void OnDestroy()
    {
        lavaBlock.OnLavaStay2D -= StayedInLava;
        lavaBlock.OnLavaExit -= LeftLava;
    }

    private void StayedInLava(GameObject gameObject)
    {
        Health healthComp = gameObject.GetComponent<Health>();
        if(healthComp == null) return;
        healthComp.health -= 1f;
        playermovement.acceleration = 50;
        playermovement.speedcap = 1;
        playermovement.jumppower = 200;
        playermovement.BASE_GRAVITY = 1.8f;
        playermovement.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.UpArrow))
            playermovement.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*150);
        if(gameObject.tag != "Player")
            CheckDeath(healthComp);
    }

    private void LeftLava(GameObject gameObject)
    {
        playermovement.speedcap = 5;
        playermovement.jumppower = 550;
        playermovement.BASE_GRAVITY = 5;
    }

    private void SpikeHit(GameObject gameObject)
    {
        if (gameObject.layer == 6 || gameObject.layer == 7)
        {
            Health healthComp = gameObject.GetComponent<Health>();
            if (healthComp == null) return;
            healthComp.health -= 25f;
                SoundManager.PlaySound(SoundManager.PlayerHit);
            if (gameObject.tag != "Player")
                CheckDeath(healthComp);
        }
    }

    private void SlimeHit(GameObject gameObject)
    {
        if (gameObject.layer == 6 || gameObject.layer == 7)
        {
            Health healthComp = gameObject.GetComponent<Health>();
            if (healthComp == null) return;
                    healthComp.health -= 15f;
            SoundManager.PlaySound(SoundManager.PlayerHit);
            if (gameObject.tag != "Player")
                CheckDeath(healthComp);
        }
    }

    private void CheckDeath(Health healthComp)
    {
        if (healthComp.health <= 0)
        {
           Destroy(healthComp.gameObject);
        }
    }
}
