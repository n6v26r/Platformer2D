using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class LiveManager : MonoBehaviour
{
    [SerializeField] private Movement Player;
    [SerializeField] private GameObject text;

    public int lives = 15;

    private void Awake(){
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        if(text!= null)
            text.GetComponent<TMP_Text>().text = "Lives: " + lives.ToString();
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadedMode) 
    {
        Player = FindObjectOfType<Movement>();
        text = GameObject.Find("Lives");
        if(text != null)
            text.GetComponent<TMP_Text>().text = "Lives: " + lives.ToString();
        if(Player != null)
            Player.death = OnDeath;
    }

    public void OnDeath(){
        lives--;
        if(lives<=0){
            Movement.score = 0;
            lives = 45;
        }
        if (text != null)
            text.GetComponent<TMP_Text>().text = "Lives: " + lives.ToString();
    }
}
