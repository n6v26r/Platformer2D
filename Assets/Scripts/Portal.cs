using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private SoundManger SoundManager;
    // Start is called before the first frame update
    void Awake(){
        SoundManager = FindAnyObjectByType<SoundManger>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            SoundManager.PlaySound(SoundManager.Portal);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
