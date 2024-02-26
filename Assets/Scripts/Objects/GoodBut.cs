using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBut : MonoBehaviour
{
    public GameObject Button;
    public GameObject Door;
    public float end = 0f;
    float counter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= end)
            Door.SetActive(false);
        Debug.Log(counter);
    }


    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (Input.GetKey(KeyCode.E)) {
                Debug.Log("pressing");
                Button.SetActive(false);
                counter += Time.deltaTime;
            } else {
                Button.SetActive(true);
            }
        } else {
            Button.SetActive(true);
        }
    }
}
