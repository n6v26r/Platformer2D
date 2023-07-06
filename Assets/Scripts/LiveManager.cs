using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class LiveManager : MonoBehaviour
{
    [SerializeField] private Movement Player;
    [SerializeField] private GameObject text;

    public int lives = 10;
    // Start is called before the first frame update

    void Awake(){
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        if(text!= null)
            text.GetComponent<TMP_Text>().text = "Lives left: " + lives.ToString();
    }

    void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadedMode) 
    {
        Player = FindObjectOfType<Movement>();
        text = GameObject.Find("Lives");
        if(text != null)
            text.GetComponent<TMP_Text>().text = "Lives left: " + lives.ToString();
        if(Player != null){
            Player.death = OnDeath;
            Movement.score = 0;
        }
    }


    // Update is called once per frame
    void OnDeath(){
        lives--;
        if(text != null)
            text.GetComponent<TMP_Text>().text = "Lives left: " + lives.ToString();
        if(lives<=0){ lives = 10; UnityEngine.SceneManagement.SceneManager.LoadScene(1);}
    }
}
