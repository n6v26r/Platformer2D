using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SoundManger : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource aud;

    [SerializeField] public AudioClip PlayerHit; // ok
    [SerializeField] public AudioClip PlayerJump; // ok
    [SerializeField] public AudioClip PlayerDash; // ok
    [SerializeField] public AudioClip PlayerDeath; // later
    [SerializeField] public AudioClip BlockBreak; // ok
    [SerializeField] public AudioClip RubyCollect; // ok
    [SerializeField] public AudioClip EmeraldCollect; // ok
    [SerializeField] public AudioClip TopazCollect; // ok
    [SerializeField] public AudioClip SlimeJump; // ok
    [SerializeField] public AudioClip GolemCharge; // ok
    [SerializeField] public AudioClip GolemLaunch; // later
    [SerializeField] public AudioClip CellingSpike; // ok
    [SerializeField] public AudioClip WormHoleEnter; // ok
    [SerializeField] public AudioClip Portal; // ok

    void Awake(){
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadedMode) 
    {
    }

    public void PlaySound(AudioClip source){
        aud.clip = source;
        aud.Play();
    }

    public void PlayMusic(AudioClip source){
        music.clip = source;
        music.Play();
    }
}
