using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public int KeysInInventory = 0;
    public int RubysCollected = 0;
    public int EmeraldsCollected = 0;
    public int TopazsCollected = 0;

    private KeyItem[] keyItems;
    private Door[] doors;
    private RubyItem[] rubys;
    private EmeraldItem[] emeralds;
    private TopazItem[] topazs;

    void Awake()
    {
        keyItems = FindObjectsOfType<KeyItem>();
        for (int i = 0; i < keyItems.Length; ++i)
            keyItems[i].OnKeyEnter += GotKey;

        doors = FindObjectsOfType<Door>();
        foreach (var door in doors)
            door.OnDoorCollision += delegate (GameObject gameObject)
            {
                TriedDoor(door, gameObject);
            };

        rubys = FindObjectsOfType<RubyItem>(true);
        for (int i = 0; i < rubys.Length; ++i)
            rubys[i].OnRubyEnter += GotRuby;

        emeralds = FindObjectsOfType<EmeraldItem>(true);
        for (int i = 0; i < emeralds.Length; ++i)
            emeralds[i].OnEmeraldEnter += GotEmerald;

        topazs = FindObjectsOfType<TopazItem>(true);
        for (int i = 0; i < topazs.Length; ++i)
            topazs[i].OnTopazEnter += GotTopaz;
    }

    private void GotKey(GameObject gameObject)
    {
        KeysInInventory++;
    }

    private void GotRuby(GameObject gameObject)
    {
        RubysCollected++;
    }

    private void GotEmerald(GameObject gameObject)
    {
        EmeraldsCollected++;
    }

    private void GotTopaz(GameObject gameObject)
    {
        TopazsCollected++;
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
