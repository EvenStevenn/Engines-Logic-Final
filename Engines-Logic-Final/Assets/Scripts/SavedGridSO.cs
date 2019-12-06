using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedMap", menuName = "Create Tiles Info", order = 1)]
public class SavedGridSO : ScriptableObject
{
    public int mapSizeX;
    public int mapSizeZ;
    public List<TileInfo> tilesInfo;
}
