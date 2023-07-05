using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject Player;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if(Player!=null)
            gameObject.transform.position = Player.transform.position - offset;
    }
}
