using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destructible : MonoBehaviour
{
    public int Type;

    private Tilemap tilemap;
    private TNT[] tnts;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tnts = FindObjectsOfType<TNT>();
        for(int i =0; i<tnts.Length; i++)
        {
            if (Type == 1)
                tnts[i].OnBlow += DestroyBlocks;
            else if (Type == 2)
                tnts[i].OnBlow += DestroySelf;
        }
    }

    private void DestroyBlocks(float x, float y)
    {
        int intx = (int)x;
        int inty = (int)y;
        for(int lin = -2; lin<=2; lin++)
        {
            for (int col = -2; col <= 2; col++)
            {
                if ((lin != -2 || col != -2) && (lin != -2 || col != 2) && (lin != 2 || col != -2) && (lin != 2 || col != 2))
                    tilemap.SetTile(new Vector3Int(intx + col, inty + lin, 0), null); // Remove tile
            }
        }
    }

    private void DestroySelf(float x, float y)
    {
        for (int lin = -2; lin <= 2; lin++)
        {
            for (int col = -2; col <= 2; col++)
            {
                if (gameObject != null)
                {
                    if ((lin != -2 || col != -2) && (lin != -2 || col != 2) && (lin != 2 || col != -2) && (lin != 2 || col != 2))
                    {
                        if (x + col == transform.position.x && y + lin == transform.position.y && gameObject != null)
                            Destroy(gameObject);
                    }
                }
            }
        }
    }
}
