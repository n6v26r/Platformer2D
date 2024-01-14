using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButton : MonoBehaviour
{
    public float Fin;
    public float counter = 0;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E)) {
            counter += Time.deltaTime;
            if (counter >= Fin)
                door.SetActive(false);
        }
    }
}
