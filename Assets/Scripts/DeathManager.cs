using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private LavaBlock lavaBlock;

    void Awake()
    {
        lavaBlock.OnLavaStay += Stayed;
    }

    private void OnDestroy()
    {
        lavaBlock.OnLavaStay -= Stayed;
    }

    private void Stayed(GameObject gameObject)
    {
        Health healthComp = gameObject.GetComponent<Health>();
        healthComp.health -= 0.5f;

        CheckDeath(healthComp);
    }

    private void CheckDeath(Health healthComp)
    {
        if (healthComp.health <= 0)
        {
            Destroy(healthComp.gameObject);
        }
    }

    public void Die()
    {
        
    }
}
