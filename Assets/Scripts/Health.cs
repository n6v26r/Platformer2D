using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    public float SpawnProtection = 0.5f;
    public float MaxHealth = 100f;

    private float SpawnProtectionActive;

    private void Update(){
        if(SpawnProtection>0)
            SpawnProtection -= Time.deltaTime;
        
        if(health<=0){ // Player died
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
}
