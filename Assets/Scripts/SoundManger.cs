using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SoundManger : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource aud;

    [SerializeField] private AudioSource PlayerHit;
    [SerializeField] private AudioSource PlayerJump;
    [SerializeField] private AudioSource PlayerDash;
    [SerializeField] private AudioSource PlayerDeath;
    [SerializeField] private AudioSource BlockBreak;
    [SerializeField] private AudioSource RubyCollect;
    [SerializeField] private AudioSource EmeraldCollect;
    [SerializeField] private AudioSource TopazCollect;
    [SerializeField] private AudioSource SlimeJump;
    [SerializeField] private AudioSource GolemCharge;
    [SerializeField] private AudioSource GolemLaunch;
    [SerializeField] private AudioSource CellingSpike;
    [SerializeField] private AudioSource WormHoleEnter;
    [SerializeField] private AudioSource Portal;
    
    private Movement Player;
    private Button btn;
    private Slime[] slimes;
    private Golem[] golems;
    private CellingSpike[] cellingSpikes;
    private WormHole[] wormholes;

    private RubyBlock[] RubyBlocks;
    private TopazBlock[] TopazBlocks;

    private EmeraldItem[] emeraldItems;
    private RubyItem[] rubyItems;
    private TopazItem topazItem;

    void Awake(){
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadedMode) 
    {

    }

    void PlaySound(AudioClip source){
        aud.clip = source;
        aud.Play();
    }

    void PlayMusic(AudioClip source){
        music.clip = source;
        music.Play();
    }
}
