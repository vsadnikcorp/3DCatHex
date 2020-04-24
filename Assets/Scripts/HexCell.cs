using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public byte terraintype;
    //TODO:  WILL NEED TO CHANGE TO SPRITEID ONCE TERRAIN SPRITES ARE ADDED-ACTUALLY, PROBABLY REMOVE AND USE TERRAINTYPE TO SELECT SPRITE
    public Color terraingfx;
}
