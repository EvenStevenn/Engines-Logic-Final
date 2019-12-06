using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLocation : MonoBehaviour
{
    public TileInfo tileInfo;
    public List<TileLocation> neighbors;

    //public int locationX;
    //public int locationZ;

    public void SetLocation(int x, int z, TileType type)
    {
        tileInfo.coordinates = new Vector3(x, 0, z);
        tileInfo.tileType = type;
        //locationX = x;
        //locationZ = z;
    }
    public void GetLocation()
    {
        print("Location is " + tileInfo.coordinates);
        //return new Vector3(locationX,0,locationZ);
    }

}
