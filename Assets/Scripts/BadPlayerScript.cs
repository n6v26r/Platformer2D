using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPlayerScript : MonoBehaviour
{

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 10, transform.position.y), Speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 10, transform.position.y), Speed * Time.deltaTime);
        }
    }
}
