using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCursor : MonoBehaviour
{
    Sprite hexSprite = null;
    // Start is called before the first frame update
    void Start()
    {
        byte mapType = 0;

        switch (mapType)
        {
            case 0:
                hexSprite = Resources.Load<Sprite>("Sprites/HexSpritePointy");
                break;
            case 1:
                hexSprite = Resources.Load<Sprite>("Sprites/HexSpriteFlat");
                break;
        }
        this.GetComponentInChildren<SpriteRenderer>().sprite = hexSprite;
    }
       
}
