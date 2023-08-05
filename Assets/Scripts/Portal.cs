using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private SoundManger SoundManager;
   
    private void Awake(){
        SoundManager = FindAnyObjectByType<SoundManger>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            SoundManager?.PlaySound(SoundManager.Portal);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
