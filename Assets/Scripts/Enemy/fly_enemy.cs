using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly_enemy : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public int type;

    Vector3 dir;

    /* 
      var dir = target.position - transform.position;
      var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    */

    private void Awake() {
        if (player == null)
            player = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (type == 0) {
            dir = (transform.position - player.transform.position).normalized;
            transform.position -= dir * speed * Time.deltaTime;
        } else if (type == 1) {
           
        }
    }
}
