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
    // Start is called before the first frame update

    void Awake(){
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        if(text!= null)
            text.GetComponent<TMP_Text>().text = "Lives: " + lives.ToString();
    }

    void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadedMode) 
    {
        Player = FindObjectOfType<Movement>();
        text = GameObject.Find("Lives");
        if(text != null)
            text.GetComponent<TMP_Text>().text = "Lives: " + lives.ToString();
        if(Player != null)
            Player.death = OnDeath;
    }


    // Update is called once per frame
    void OnDeath(){
        lives--;
        if(lives<=0){
            Movement.score = 0;
            lives = 15;
        }
        if (text != null)
            text.GetComponent<TMP_Text>().text = "Lives: " + lives.ToString();
    }
}
