using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour {
    public float offTime;
    public float onTime;
    private int state;//0 - off; 1 - on;
    private float timer;
    public SpriteRenderer sp;

    private void Awake() {
        sp = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start() {
        timer = 0;
        state = 0;
    }

    // Update is called once per frame
    void Update() {
        if ((timer >= offTime && state == 0) || (timer >= onTime && state == 1)) {
            timer = 0;
            sp.enabled = (state == 0);
            state = 1 - state;
        }

        timer += Time.deltaTime;
    }
}
