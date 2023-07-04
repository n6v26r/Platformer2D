using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public int KeysInInventory = 0;

    private KeyItem[] keyItems;

    void Awake()
    {
        keyItems = FindObjectsOfType<KeyItem>();
        for (int i = 0; i < keyItems.Length; ++i)
            keyItems[i].OnKeyEnter += GotKey;
    }

    private void GotKey(GameObject gameObject)
    {
        if(gameObject.tag == "Player")
        {
            KeysInInventory++;
        }
    }
}
