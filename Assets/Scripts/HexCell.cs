using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public byte terraintype;
    public RectTransform uiRect;
    public ChunkController chunk;

    //TODO--MAKE THIS WORK!
    public byte TerrainType
    {
        get
        {
            return terraintype;
        }
        set
        {
            if (terraintype == value)
            {
                Debug.Log("t2:" + terraintype + "value: " + value);
                return;
            }
            Debug.Log("t3:" + terraintype + "value: " + value);
            terraintype = value;
            //TerrainType = value;
            RefreshCell(); 
        }
    }
    public HexCoordinates Coordinates { get; protected set; }
    Color color;

    //TODO:  WILL NEED TO CHANGE TO SPRITEID ONCE TERRAIN SPRITES ARE ADDED-ACTUALLY, PROBABLY REMOVE AND USE TERRAINTYPE TO SELECT SPRITE
    public Color terraingfx;

    void RefreshCell()
    {
        if (chunk)
        {
            chunk.RefreshChunks();
        }
    }
}
