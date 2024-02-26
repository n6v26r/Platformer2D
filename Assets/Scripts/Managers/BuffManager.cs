using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public GameObject player;

    private Items[] items;

    private void Awake()
    {
        items = FindObjectsOfType<Items>(true);
        for (int i = 0; i < items.Length; ++i)
        {
            items[i].OnItemEnter += ItemBuff;
        }
    }

    private void ItemBuff(int Type)
    {
        if (Type == 1)
        {
            Health healthComp = player.GetComponent<Health>();
            healthComp.AddHealth(25);

            Movement.score += 15;
        }
        else if (Type == 2)
        {
            Movement.score += 200;
        }
        else if (Type == 3)
        {
            player.GetComponent<Movement>().dashing = true;
            Movement.score += 25;
        }else if(Type == 6) {
            player.GetComponent<Movement>().extraJumps++;
        }
    }
}
