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
        this.x = x;
        this.z = z;
    }

    public static HexCoordConvert FromOffsetCoordinates(int x, int z)
    {
        byte mapType = HexGrid.mapType;
        //Debug.Log(mapType);
        switch (mapType)
        {
            case 0: //POINTY-TOP HEX
                    return new HexCoordConvert(x - z / 2, z);
                //break;
            case 1:  //FLAT-TOP HEX
                return new HexCoordConvert(x, z - x / 2);
                //break;
            default:
                return new HexCoordConvert(x, z);
                //break;
        }

        ////POINTY-TOP HEX^^^^
        //return new HexCoordConvert(x - z / 2, z);
        ////^^^^

        // FLAT TOP HEX??? ____
        //return new HexCoordConvert(x, z - x / 2);
        //____
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
        byte mapType = HexGrid.mapType;
        float x = 0;
        float y = 0;
        float z = 0;
        float offset = 0;
        int iX = 0;
        int iY = 0;
        int iZ = 0;


        switch(mapType)
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
        ////FOR POINTY-TOP^^^^^^^^^^^^^^^^^^
        //float x = position.x / (HexMetrics.innerRadius * 2f);

        //float y = -x;

        //float offset = position.z / (HexMetrics.outerRadius * 3f);
        ////Debug.Log("x: " + position.x + " y: " + position.y + " z: " + position.z);
        //x -= offset;
        //y -= offset;

        //int iX = Mathf.RoundToInt(x);
        //int iY = Mathf.RoundToInt(y);
        //int iZ = Mathf.RoundToInt(-x - y);

        //if (iX + iY + iZ != 0)
        //{
        //    float dX = Mathf.Abs(x - iX);
        //    float dY = Mathf.Abs(y - iY);
        //    float dZ = Mathf.Abs(-x - y - iZ);

        //    if (dX > dY && dX > dZ)
        //    {
        //        iX = -iY - iZ;
        //    }
        //    else if (dZ > dY)
        //    {
        //        iZ = -iX - iY;
        //    }
        //}
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        ////FLAT-TOP___________________________________
        //float x = position.x / (HexMetrics.outerRadius * 2f);
        //float z = position.z / (HexMetrics.innerRadius * 2f);
        //float y = -z;

        //float offset = position.x / (HexMetrics.outerRadius * 3f);
        ////Debug.Log("x: " + position.x + " y: " + position.y + " z: " + position.z);

        //z -= offset;
        //y -= offset;

        //int iX = Mathf.RoundToInt(-y - z);
        //int iY = Mathf.RoundToInt(y);
        //int iZ = Mathf.RoundToInt(z);

        //if (iX + iY + iZ != 0)
        //{
        //    float dX = Mathf.Abs(-y - z - iX);
        //    float dY = Mathf.Abs(y - iY);
        //    float dZ = Mathf.Abs(z - iZ);

        //    if (dX > dY && dX > dZ)
        //    {
        //        iX = -iY - iZ;
        //    }
        //    else if (dZ > dY)
        //    {
        //        iZ = -iX - iY;
        //    }
        //}

        return new HexCoordConvert(iX, iZ);
    }
}
