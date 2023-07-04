using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public int KeysInInventory = 0;

    private KeyItem[] keyItems;
    private Door[] doors;

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
    }

    private void GotKey(GameObject gameObject)
    {
        if(gameObject.tag == "Player")
        {
            KeysInInventory++;
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
