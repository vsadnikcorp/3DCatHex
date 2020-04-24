using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCursor : MonoBehaviour
{
    Sprite hexSprite = null;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<SpriteRenderer>().sprite = hexSprite;
    }

    public void Init(int mapType, HexCursor hexCursor)
    //public void Init(int mapType)
    {
        hexCursor.tag = "HexCursor";
        switch (mapType)
        {
            case 0:
                hexSprite = Resources.Load<Sprite>("Sprites/HexSpritePointy");
                break;
            case 1:
                hexSprite = Resources.Load<Sprite>("Sprites/HexSpriteFlat");
                break;
        }
        hexCursor.GetComponentInChildren<SpriteRenderer>().sprite = hexSprite;
    }

    //public static implicit operator GameObject(HexCursor v)
    //{
    //    throw new NotImplementedException();
    //}
}
