using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private LavaBlock lavaBlock;
    [SerializeField] private Movement playermovement;
    private CellingSpike[] cellingSpikes;

    void Awake()
    {
        lavaBlock.OnLavaStay2D += StayedInLava;
        cellingSpikes = FindObjectsOfType<CellingSpike>();
        for (int i = 0; i < cellingSpikes.Length; ++i)
            cellingSpikes[i].OnSpikeHit+= SpikeHit;
    }

    private void OnDestroy()
    {
        lavaBlock.OnLavaStay2D -= StayedInLava;
    }

    private void StayedInLava(GameObject gameObject)
    {
        Health healthComp = gameObject.GetComponent<Health>();
        if(healthComp == null) return;
        healthComp.health -= 0.5f;
        playermovement.acceleration = 50;
        playermovement.speedcap = 1;
        playermovement.jumppower = 200;
        playermovement.BASE_GRAVITY = 1.8f;
        playermovement.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        

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
            healthComp.health -= 25f;

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
