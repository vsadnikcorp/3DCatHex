using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexCoordinates

{
    public int X { get { return x; } }
    public int Z { get { return z; } }
    public int Y { get {return -X - Z; } }

    [SerializeField]
    private int x, z;

    public HexCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static HexCoordinates FromOffsetCoordinates(int maptype, int x, int z)
    {
        switch (maptype)
        {
            case 0: //POINTY-TOP HEX
                    return new HexCoordinates(x - z / 2, z);
            case 1:  //FLAT-TOP HEX
                return new HexCoordinates(x, z - x / 2);
            default:
                return new HexCoordinates(x, z);
        }
    }

    public override string ToString()
    {
        return "(" + X.ToString() + "," + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }

    public static HexCoordinates FromPosition(int maptype, Vector3 position)
    {
        float x = 0;
        float y = 0;
        float z = 0;
        float offset = 0;
        int iX = 0;
        int iY = 0;
        int iZ = 0;

        switch(maptype)
        {
            case 0:  //POINTY-TOP HEX
                x = position.x / (HexMetrics.innerRadius * 2f);

                y = -x;

                offset = position.z / (HexMetrics.outerRadius * 3f);
                x -= offset;
                y -= offset;

                iX = Mathf.RoundToInt(x);
                iY = Mathf.RoundToInt(y);
                iZ = Mathf.RoundToInt(-x - y);

                if (iX + iY + iZ != 0)
                {
                    float dX = Mathf.Abs(x - iX);
                    float dY = Mathf.Abs(y - iY);
                    float dZ = Mathf.Abs(-x - y - iZ);

                    if (dX > dY && dX > dZ)
                    {
                        iX = -iY - iZ;
                    }
                    else if (dZ > dY)
                    {
                        iZ = -iX - iY;
                    }
                }
                break;

            case 1: //FLAT-TOP HEX
                z = position.z / (HexMetrics.innerRadius * 2f);
                y = -z;

                offset = position.x / (HexMetrics.outerRadius * 3f);

                z -= offset;
                y -= offset;

                iX = Mathf.RoundToInt(-y - z);
                iY = Mathf.RoundToInt(y);
                iZ = Mathf.RoundToInt(z);

                if (iX + iY + iZ != 0)
                {
                    float dX = Mathf.Abs(-y - z - iX);
                    float dY = Mathf.Abs(y - iY);
                    float dZ = Mathf.Abs(z - iZ);

                    if (dX > dY && dX > dZ)
                    {
                        iX = -iY - iZ;
                    }
                    else if (dZ > dY)
                    {
                        iZ = -iX - iY;
                    }
                }
                break;
        }
        return new HexCoordinates(iX, iZ);
    }

    public static Vector2 FromHexToScreen(int maptype, HexCoordinates coordinates)
    {
        Vector2 V2position;
        int hx = coordinates.X;
        int hy = coordinates.Y;
        int hz = coordinates.Z;
        float vx = 0f;
        float vy = 0f;

        switch(maptype)
        {
            case 0:  //POINTY-TOP HEXES
                vx = (HexMetrics.innerRadius * 2f) * hx + (HexMetrics.innerRadius) * hz;
                vy = (HexMetrics.outerRadius * 3f) / 2 * hz; 
                break;
            case 1:  //FLAT-TOP HEXES
                vx = HexMetrics.outerRadius * 3f / 2 * hx;
                vy = hz * HexMetrics.innerRadius * 2f + hx * (HexMetrics.innerRadius);
                break;
        }
        V2position = new Vector2(vx, vy);
        return V2position;
    }
}
