using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    private SoundManger SoundManager;
    public int KeysInInventory = 0;
    public int RubysCollected = 0;
    public int EmeraldsCollected = 0;
    public int TopazsCollected = 0;

    private Door[] doors;
    private Items[] items;

    void Awake()
    {
        SoundManager = FindAnyObjectByType<SoundManger>();

        doors = FindObjectsOfType<Door>();
        foreach (var door in doors)
            door.OnDoorCollision += delegate (GameObject gameObject)
            {
                TriedDoor(door, gameObject);
            };

        items = FindObjectsOfType<Items>(true);
        for (int i = 0; i < items.Length; ++i)
            items[i].OnItemEnter += GotItem;
    }

    private void GotItem(int Type)
    {
        if (Type == 1)
        {
            RubysCollected++;
            if (SoundManager != null)
                SoundManager.PlaySound(SoundManager.RubyCollect);
        }
        else if (Type == 2)
        {
            EmeraldsCollected++;
            if (SoundManager != null)
                SoundManager.PlaySound(SoundManager.EmeraldCollect);
        }
        else if (Type == 3)
        {
            TopazsCollected++;
            if (SoundManager != null)
                SoundManager.PlaySound(SoundManager.TopazCollect);
        }
        else if( Type == 4)
        {
            KeysInInventory++;
            if (SoundManager != null)
                SoundManager.PlaySound(SoundManager.TopazCollect);
        }
    }

    private void TriedDoor(Door door, GameObject gameObject)
    {
        if (gameObject.tag == "Player")
        {
            if (KeysInInventory > 0)
            {
                KeysInInventory--;
                Destroy(door.gameObject);
            }
        }
    }
}
