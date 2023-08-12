using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private SoundManger SoundManager;
    
    private CellingSpike[] cellingSpikes;
    private Slime[] slimes;
    private Liquid[] liquids;
    private TNT[] tnts;
    private Health healthComp;
    private Spike[] spikes;


    public GameObject spawnpoint;

    private GameObject Player;

    private void Awake()
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
            cellingSpikes[i].OnSpikeHit+= CellingSpikeHit;

        slimes = FindObjectsOfType<Slime>();
        for (int i = 0; i < slimes.Length; ++i)
            slimes[i].OnSlimeHit += SlimeHit;

        tnts = FindObjectsOfType<TNT>();
        for (int i = 0; i < tnts.Length; ++i)
            tnts[i].OnBlowVictim += BlewUp;

        spikes = FindObjectsOfType<Spike>();
        for (int i = 0; i < spikes.Length; ++i)
            spikes[i].OnHit += SpikeHit;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < liquids.Length; ++i)
        {
            liquids[i].OnLiquidStay2D -= StayedInLiquid;
            liquids[i].OnLiquidExit -= LeftLiquid;
        }
    }

    private bool CanCatchOnFire = true;
    private int OnFire = 0;
    private float OnFireCooldown = 0.3f;
    private float LastOnFire = 0;
    private void Update()
    {
        Debug.Log(OnFire);
        if (CanCatchOnFire == true)
        {
            if (OnFire > 0 && Time.time - LastOnFire > OnFireCooldown)
            {
                OnFire--;
                Damage(Player, 1);
                LastOnFire = Time.time;
            }
        }
        else
            OnFire = 0;
    }

    private void StayedInLiquid(GameObject gameObject, int Type)
    {
        if(Type == 1)
        {
            LavaDamage(gameObject);

            if((Input.GetKey(KeyCode.LeftControl) || Input.GetMouseButton(0)) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
            {
                EscapeLavaDamage(gameObject);
            }
        }
        else if(Type == 2)
        {
            CanCatchOnFire = false;
        }
    }
    //TODO @rrradu: Complete this!
    private void LeftLiquid(int Type)
    {
        if (Type == 1)
        {
            
        }
        else if(Type == 2)
        {
            CanCatchOnFire = true;
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

    private void CellingSpikeHit(GameObject gameObject)
    {
        Damage(gameObject, 25);
    }

    private void SpikeHit(GameObject gameObject){
        Health health = gameObject.GetComponent<Health>();
        if(health==null) return;

        SoundManager?.PlaySound(SoundManager.CellingSpike);
        Damage(gameObject, health.MaxHealth);
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
        if(gameObject.tag == "Player"){
            OnFire += 25;
            Damage(gameObject, 5);
        }
    }

    public void BlewUp(GameObject gameObject)
    {
        if (gameObject.tag == "Player")
            Damage(gameObject, 100);
        else
            Destroy(gameObject);
    }

    private void Damage(GameObject gameObject, float dmg){
        if (gameObject.layer == 6 || gameObject.layer == 7)
        {
            healthComp = gameObject.GetComponent<Health>();
            if (healthComp == null) return;
                    healthComp.InflictDamage(dmg);
            if(gameObject.tag == "Player")
                SoundManager?.PlaySound(SoundManager.PlayerHit);
        }
    }
}
