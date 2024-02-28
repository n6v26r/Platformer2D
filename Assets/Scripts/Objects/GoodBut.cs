using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBut : MonoBehaviour
{
    public GameObject Button;
    public GameObject Door;
    public float end = 0f;
    float counter = 0f;
    int inrange = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inrange == 1) {
            if (Input.GetKey(KeyCode.E)) {
                counter += Time.deltaTime;
                Button.SetActive(false);
            } else {
                Button.SetActive(true);
            }
        } else { 
            Button.SetActive(true);
        }

        if (counter >= end)
            Door.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            inrange = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            inrange = 0;
        }
    }
}
