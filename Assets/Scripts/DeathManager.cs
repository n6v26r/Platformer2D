using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    
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
        }
    }

    private void LavaDamage(GameObject gameObject)
    {
        Damage(gameObject, new DamageEffect(1, 1, 0, "Fire"));
    }

    private void EscapeLavaDamage(GameObject gameObject)
    {
        Damage(gameObject, new DamageEffect(5, 1, 0, "Fire"));
    }

    private void CellingSpikeHit(GameObject gameObject)
    {
        Damage(gameObject, new DamageEffect(25, 1, 0, "Hit"));
    }

    private void SpikeHit(GameObject gameObject){
        Health health = gameObject.GetComponent<Health>();
        if(health==null) return;

        Damage(gameObject, new DamageEffect(health.MaxHealth, 1, 0, "Hit"));
    }

    private void SlimeHit(GameObject gameObject)
    {
       Damage(gameObject, new DamageEffect(20f, 1, 0, "Hit"));
    }

    public void BulletHit(GameObject gameObject){
        Damage(gameObject, new DamageEffect(30, 1, 0, "Hit"));
    }

    public void FireDartHit(GameObject gameObject)
    {
        Damage(gameObject, new DamageEffect(5, 1, 0, "Hit"));
        Damage(gameObject, new DamageEffect(1, 25, 0.3f, "Fire"));
    }

    public void BlewUp(GameObject gameObject)
    {
        Health healthComp = gameObject.GetComponent<Health>();
        if(healthComp!=null)
            Damage(gameObject, new DamageEffect(healthComp.MaxHealth, 1, 0, "Explosion"));
    }

    private void Damage(GameObject gameObject, DamageEffect effect){
        healthComp = gameObject.GetComponent<Health>();
        healthComp?.ApplyEffect(effect); 
    }
}
