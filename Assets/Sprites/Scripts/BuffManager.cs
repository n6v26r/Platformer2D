using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private RubyItem[] rubys;
    private EmeraldItem[] emeralds;
    private TopazItem[] topazs;


    private void Awake()
    {

        rubys = FindObjectsOfType<RubyItem>(true);
        for (int i = 0; i < rubys.Length; ++i)
            rubys[i].OnRubyEnter += RubyBuff;

        emeralds = FindObjectsOfType<EmeraldItem>(true);
        for (int i = 0; i < emeralds.Length; ++i)
            emeralds[i].OnEmeraldEnter += EmeraldBuff;

        topazs = FindObjectsOfType<TopazItem>(true);
        for (int i = 0; i < topazs.Length; ++i)
            topazs[i].OnTopazEnter += TopazBuff;
    }


    private void RubyBuff(GameObject gameObject)
    {
        Health healthComp = gameObject.GetComponent<Health>();
        if (healthComp.health < 85)
            healthComp.health += 15;
        else
            healthComp.health = 100;
        Movement.score += 15;
    }

    private void EmeraldBuff(GameObject gameObject)
    {
        Movement.score += 100;
    }

    private void TopazBuff(GameObject gameObject)
    {
        gameObject.GetComponent<Movement>().dashing = true;
        Movement.score += 25;
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
