using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private LavaBlock lavaBlock;
    [SerializeField] private Movement playermovement;

    void Awake()
    {
        lavaBlock.OnLavaStay += StayedInLava;
        lavaBlock.OnLavaExit += LeftLava;
    }

    private void OnDestroy()
    {
        lavaBlock.OnLavaStay -= StayedInLava;
        lavaBlock.OnLavaExit -= LeftLava;
    }

    private void StayedInLava(GameObject gameObject)
    {
        Health healthComp = gameObject.GetComponent<Health>();
        healthComp.health -= 0.5f;
        //playermovement.acceleration = 50;
        playermovement.speedcap = 1;
        playermovement.jumppower = 150;
        

        CheckDeath(healthComp);
    }

    private void LeftLava(GameObject gameObject)
    {
        
    }

    private void CheckDeath(Health healthComp)
    {
        if (healthComp.health <= 0)
        {
            Destroy(healthComp.gameObject);
        }
    }
}
