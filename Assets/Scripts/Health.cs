using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect // cool stuff! :)
{
    public float InflictDmg;
    public float iterations;
    public float delay;
    public string type;

    public DamageEffect(float InflictDmg, float iterations, float delay,
                        string type = "Custom")
    {
        this.InflictDmg = InflictDmg;
        this.iterations = iterations;
        this.delay = delay;
        this.type = type;
    }
}

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float HitSoundCooldownTime = 0.3f;

    public float SpawnProtection = 0.5f;
    public float MaxHealth = 100f;

    public string[] ImuneTo_Serializable;
    public Hashtable ImuneTo;

    private SoundManger SoundManager;
    private float SpawnProtectionActive;
    private float HitSoundCooldown;

    private List<DamageEffect> PendingDmgEff;

    private void Awake()
    {
        PendingDmgEff = new List<DamageEffect>();
        SoundManager = FindAnyObjectByType<SoundManger>();
    }
    private void Start() { RegenHashtable(ImuneTo_Serializable); }
    private void Update()
    {
        // Cooldowns
        if (HitSoundCooldown > 0)
            HitSoundCooldown -= Time.deltaTime;

        if (SpawnProtection > 0)
            SpawnProtection -= Time.deltaTime;

        if (health <= 0)
        { // Player died
            PendingDmgEff.Clear();
            StopAllCoroutines(); // Cancel all working damage effects
            gameObject.transform.position = FindObjectOfType<Spawnpoint>().transform.position; // Move player to spawnpoint
            ActivateSpawnProtection();
            ResetHealth();
        }
    }

    private void ResetHealth() { health = MaxHealth; }

    public void RegenHashtable(string[] Keys)
    {
        ImuneTo = new Hashtable();
        for (int i = 0; i < Keys.Length; i++)
        {
            ImuneTo.Add(Keys[i], 100);
        }
    }

    public void ActivateSpawnProtection()
    {
        SpawnProtectionActive = SpawnProtection;
    }

    public void InflictDamage(float damage)
    {
        if (SpawnProtectionActive <= 0)
            health -= damage;
        if (gameObject.tag == "Player" && HitSoundCooldown <= 0)
        {
            SoundManager?.PlaySound(SoundManager.PlayerHit);
            HitSoundCooldown = HitSoundCooldownTime;
        }
    }

    public float GetHealth() { return health; }

    public void AddHealth(float buff)
    {
        health += buff;
        if (health > MaxHealth)
            ResetHealth();
    }

    public bool SetImunity(string effect, int val)
    {
        if (ImuneTo.Contains(effect))
            ImuneTo[effect] = val;
        else
            return false;
        return true;
    }

    public void ApplyEffect(DamageEffect effect)
    {
        // If another instance of the same effect is already present, stack them;
        for (int i = 0; i < PendingDmgEff.Count; i++)
        {
            if (PendingDmgEff[i].type == effect.type)
            { // Already has this effect
              // Stack the 2 effects:
                PendingDmgEff[i].InflictDmg +=Mathf.Abs(PendingDmgEff[i].InflictDmg - effect.InflictDmg);
                PendingDmgEff[i].iterations += effect.iterations;
                PendingDmgEff[i].delay =Mathf.Min(PendingDmgEff[i].delay, effect.delay);
                return;
            }
        }
        StartCoroutine(DmgEffect(effect));
    }

    IEnumerator DmgEffect(DamageEffect effect)
    {
        PendingDmgEff.Add(effect);
        while (PendingDmgEff[PendingDmgEff.FindIndex(a => a==effect)].iterations > 0)
        {
            if (ImuneTo.Contains(PendingDmgEff[PendingDmgEff.FindIndex(a => a==effect)].type)) // If the enity has imunity to this dmg type
                InflictDamage(PendingDmgEff[PendingDmgEff.FindIndex(a => a==effect)].InflictDmg*(100 - (int)ImuneTo[PendingDmgEff[PendingDmgEff.FindIndex(a => a==effect)].type]));
            else
                InflictDamage(PendingDmgEff[PendingDmgEff.FindIndex(a => a==effect)].InflictDmg);
            yield return new WaitForSeconds(PendingDmgEff[PendingDmgEff.FindIndex(a => a==effect)].delay);
            PendingDmgEff[PendingDmgEff.FindIndex(a => a==effect)].iterations--;
        }
        PendingDmgEff.RemoveAt(PendingDmgEff.FindIndex(a => a==effect));
    }
}
