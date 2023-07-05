using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private RubyItem[] rubys;


    private void Awake()
    {
        rubys = FindObjectsOfType<RubyItem>(true);
        for (int i = 0; i < rubys.Length; ++i)
            rubys[i].OnRubyEnter += RubyBuff;
    }


    private void RubyBuff(GameObject gameObject)
    {
        Health healthComp = gameObject.GetComponent<Health>();
        if (healthComp.health < 85)
            healthComp.health += 15;
        else
            healthComp.health = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
