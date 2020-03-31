using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexCoordConvert

{
    public int X { get { return x; } }
    public int Z { get { return z; } }
    public int Y { get {return -X - Z; } }

    [SerializeField]
    private int x, z;

    public HexCoordConvert(int x, int z)
    {
        //X = x;
        //Z = z;
        this.x = x;
        this.z = z;
    }
    public static HexCoordConvert FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordConvert(x - z/2, z); //ONLY FOR POINTY-TOP?  MUST FIX FOR FLAT (FOR POINTY, ADDED "-Z/2" TO LEFT)
    }
    public override string ToString()
    {
        return "(" + X.ToString() + "," + Y.ToString() + ", " + Z.ToString() + ")";
    }
    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }

    public static HexCoordConvert FromPosition(Vector3 position)
    {
        float x = position.x / (HexMetrics.innerRadius * 2f);
        float y = -x;

        float offset = position.z / (HexMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

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

        return new HexCoordConvert(iX, iZ);
    }
}
