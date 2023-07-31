using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private SoundManger SoundManager;
    
    public Movement playermovement;
    private CellingSpike[] cellingSpikes;
    private Slime[] slimes;
    private Liquid[] liquids;

    private GameObject Player;

    void Awake()
    {
        SoundManager = FindAnyObjectByType<SoundManger>();
        Player = GameObject.FindGameObjectWithTag("Player");

        liquids = FindObjectsOfType<Liquid>();
        for (int i = 0; i < liquids.Length; ++i)
        {
            liquids[i].OnLiquidStay2D += StayedInLiquid;
            liquids[i].OnLiquidExit += LeftLiquid;
        }

        cellingSpikes = FindObjectsOfType<CellingSpike>();
        for (int i = 0; i < cellingSpikes.Length; ++i)
            cellingSpikes[i].OnSpikeHit+= SpikeHit;

        slimes = FindObjectsOfType<Slime>();
        for (int i = 0; i < slimes.Length; ++i)
            slimes[i].OnSlimeHit += SlimeHit;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < liquids.Length; ++i)
        {
            liquids[i].OnLiquidStay2D -= StayedInLiquid;
            liquids[i].OnLiquidExit -= LeftLiquid;
        }
    }


    private int OnFire = 0;
    private float OnFireCooldown = 0.3f;
    private float LastOnFire = 0;
    private void FixedUpdate()
    {
        if(OnFire > 0 && Time.time - LastOnFire > OnFireCooldown)
        {
            OnFire--;
            Damage(Player, 1);
            LastOnFire = Time.time;
        }
    }

    private void StayedInLiquid(GameObject gameObject, int Type)
    {
        if(Type == 1)
        {
            LavaDamage(gameObject);
            playermovement.acceleration = 50;
            playermovement.speedcap = 1;
            playermovement.jumppower = 200;
            playermovement.BASE_GRAVITY = 1.8f;

            playermovement.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if((Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(0)) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
            {
                playermovement.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*150);
                EscapeLavaDamage(gameObject);
            }
        }
        else if(Type == 2)
        {
            OnFire = 0;
        }
    }

    private void LeftLiquid(int Type)
    {
        if (Type == 1)
        {
            playermovement.speedcap = 5;
            playermovement.jumppower = 550;
            playermovement.BASE_GRAVITY = 5;
        }
        else if(Type == 2)
        {

        }
    }

    private void LavaDamage(GameObject gameObject)
    {
        Damage(gameObject, 1);
    }

    private void EscapeLavaDamage(GameObject gameObject)
    {
        Damage(gameObject, 5);
    }

    private void SpikeHit(GameObject gameObject)
    {
        Damage(gameObject, 25);
    }

    private void SlimeHit(GameObject gameObject)
    {
       Damage(gameObject, 20f);
    }

    public void BulletHit(GameObject gameObject){
        Damage(gameObject, 30);
    }

    public void FireDartHit(GameObject gameObject)
    {
        Damage(gameObject, 5);
        OnFire += 25;
    }

    private void Damage(GameObject gameObject, float dmg){
        if (gameObject.layer == 6 || gameObject.layer == 7)
        {
            Health healthComp = gameObject.GetComponent<Health>();
            if (healthComp == null) return;
                    healthComp.health -= dmg;
            SoundManager.PlaySound(SoundManager.PlayerHit);
            if (gameObject.tag != "Player")
                CheckDeath(healthComp);
            else if (healthComp.health <= 0)
                OnFire = 0;
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
