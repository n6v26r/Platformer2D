using System;
using UnityEngine;

public class RubyBlock : MonoBehaviour
{
    public float timeRequired;
    public Action<GameObject> OnRubyMine;

    float timer;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            timer += Time.fixedDeltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer >= timeRequired)
        {
            if(OnRubyMine != null)
                OnRubyMine(collision.gameObject);

            Destroy(gameObject);
        }
    }
}
