using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public byte terrainType;
    public RectTransform uiRect;
    public ChunkController chunk;
    public Color terraingfx;
    Color color;

    public byte TerrainType
    {
        get
        {
            return terrainType;
        }
        set
        {
            if (terrainType == value)
            {
                return;
            }
            terrainType = value;
            RefreshCell(); 
        }
    }
    public HexCoordinates Coordinates { get; protected set; }
   

    void RefreshCell()
    {
        if (chunk)
        {
            chunk.RefreshChunks();
        }
    }

    public void SaveCell(BinaryWriter binwriter)
    {
        binwriter.Write(terrainType);
    }

    public void LoadCell(BinaryReader binreader)
    {
        terrainType = binreader.ReadByte();
    }
}
