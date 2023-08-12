using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class DamageEffect{
    public float InflictDmg;
    public float iterations;
    public float delay;
    public string type;

    public DamageEffect(float InflictDmg, float iterations, float delay, string type="Custom"){
        this.InflictDmg = InflictDmg;
        this.iterations = iterations;
        this.delay = delay;
        this.type = type;
    }
}

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    public float SpawnProtection = 0.5f;
    public float MaxHealth = 100f;

    public string[] ImuneTo;

    private float SpawnProtectionActive;

    private void Update(){
        if(SpawnProtection>0)
            SpawnProtection -= Time.deltaTime;
        
        if(health<=0){ // Player died
            StopAllCoroutines(); // Cancel all working damage effects
            gameObject.transform.position = FindObjectOfType<Spawnpoint>().transform.position; // Move player to spawnpoint
            ActivateSpawnProtection();
            ResetHealth();
        }   
    }
    
    private void ResetHealth(){
        health = MaxHealth;
    }

    public void ActivateSpawnProtection(){
        SpawnProtectionActive = SpawnProtection;
    }

    public void InflictDamage(float damage){
        if(SpawnProtectionActive<=0)
            health -= damage;
    }

    public float GetHealth(){
        return health;
    }

    public void AddHealth(float buff){
        health += buff;
        if(health>MaxHealth)
            ResetHealth();
    }

    public void ApplyEffect(DamageEffect effect){
        for(int i=0; i<ImuneTo.Length; i++){
            if(ImuneTo[i]==effect.type)
                return;
        }
        StartCoroutine(DmgEffect(effect));
    }

    IEnumerator DmgEffect(DamageEffect effect){
        for(int i=0; i<effect.iterations; i++){
            InflictDamage(effect.InflictDmg);
            yield return new WaitForSeconds(effect.delay);
        }
    }
}
