using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    private SoundManger SoundManager;
    [HideInInspector]
    public int RubysCollected = 0;
    [HideInInspector]
    public int EmeraldsCollected = 0;
    [HideInInspector]
    public int TopazsCollected = 0;
    [HideInInspector]
    public int SilverKeysInInventory = 0;
    [HideInInspector]
    public int GoldenKeysInInventory = 0;

    private Door[] doors;
    private Items[] items;

    void Awake()
    {
        SoundManager = FindAnyObjectByType<SoundManger>();

        doors = FindObjectsOfType<Door>();
        foreach (var door in doors)
            door.OnDoorCollision += delegate (GameObject gameObject, int DoorType) /// this doesn`t work...
            {
                TriedDoor(door, gameObject, DoorType);
            };

        items = FindObjectsOfType<Items>(true);
        for (int i = 0; i < items.Length; ++i)
            items[i].OnItemEnter += GotItem;
    }

    private void TriedDoor(Door door, GameObject WhoTouched, int doorType)
    {
        if (WhoTouched.tag == "Player")
        {
            if (doorType == 1 && SilverKeysInInventory > 0)
            {
                SilverKeysInInventory--;
                Destroy(door.gameObject);
            }
            else if (doorType == 2 && GoldenKeysInInventory > 0)
            {
                GoldenKeysInInventory--;
                Destroy(door.gameObject);
            }
        }
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
            SilverKeysInInventory++;
            if (SoundManager != null)
                SoundManager.PlaySound(SoundManager.EmeraldCollect);
        }
        else if( Type == 5)
        {
            GoldenKeysInInventory++;
            if (SoundManager != null)
                SoundManager.PlaySound(SoundManager.TopazCollect);
        }
    }
}
