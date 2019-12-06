using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TileType
{
    walkable,
    obstacle,
    edge
}

public class GridManager : MonoBehaviour
{
    //public GameObject prefabTile;
    //public GameObject prefabEdgeTile;



    public static GridManager instance;
    public List<GameObject> prefabTiles;

    //public MapSO mapSO;
    public int[,] grid;

    public int mapSizeX = 10;
    public int mapSizeZ = 10;
    [Space(10)]
    public TileLocation[,] tilesLocationMap;
    private int currentMapSizeX;
    private int currentMapSizeZ;
    public List<GameObject> temporaryTiles;

    [Space(10)]
    [Header("Saved Content")]
    public SavedGridSO savedGridMap;


    private void Awake()
    {
        instance = this;
    }

    [ContextMenu("Clear All Tiles")]
    public void ClearAllTiles()
    {  // Clear all the tiles by DESTROYING it first than reseting the List spawnTiles
        if (temporaryTiles != null)
        {
            for (int x = temporaryTiles.Count - 1; x >= 0; x--)
            {
                DestroyImmediate(temporaryTiles[x]);
            }
            temporaryTiles.Clear();
        }
    }

    [ContextMenu("Create Map")]
    public void CreateTiles()
    {
        ClearAllTiles();

        savedGridMap.tilesInfo.Clear();
        savedGridMap.mapSizeX = mapSizeX;
        savedGridMap.mapSizeZ = mapSizeZ;

        grid = new int[mapSizeX, mapSizeZ];

        currentMapSizeX = mapSizeX;
        currentMapSizeZ = mapSizeZ;

        tilesLocationMap = new TileLocation[mapSizeX, mapSizeZ];

        //TilesLocation maps intitialization of the 2D Array
        //the information will be added inside when creating the tile object

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                if (isEdge(x, z))
                {
                    grid[x, z] = 0;
                }
                else
                {
                    grid[x, z] = Random.Range(1, prefabTiles.Count);
                }
                SpawnTile(x, z);
            }
        }

    }

    [ContextMenu("Load my Tiles")]
    public void LoadTiles()
    {
        ClearAllTiles();
        //This script only loads from the saved SO if there is data
        if (savedGridMap.tilesInfo.Count > 0)
        {
            Debug.Log("Loading Map Now : " + savedGridMap.mapSizeX + "x" + savedGridMap.mapSizeZ +
                "includes a total count of " + savedGridMap.tilesInfo.Count + "tiles");

            tilesLocationMap = new TileLocation[savedGridMap.mapSizeX, savedGridMap.mapSizeZ];
            currentMapSizeX = savedGridMap.mapSizeX;
            currentMapSizeZ = savedGridMap.mapSizeZ;

            foreach (TileInfo singleTile in savedGridMap.tilesInfo)
            {
                GameObject tile = Instantiate(prefabTiles[convertToValue(singleTile.tileType)]);
                tile.transform.position = singleTile.coordinates;
                tile.GetComponent<TileLocation>().tileInfo = singleTile;

                temporaryTiles.Add(tile);

                AddTileToMap(tile.GetComponent<TileLocation>(), (int)singleTile.coordinates.x, (int)singleTile.coordinates.z);
            }
        }

    }

    public void AddTileToMap(TileLocation singleTile, int x, int z)
    {
        tilesLocationMap[x, z] = singleTile;
    }

    [ContextMenu("Link My Neighbours")]
    public void LinkMyNeighbours()
    {
        if (tilesLocationMap != null)
        {
            int total = 0;
            for (int x = 0; x < tilesLocationMap.GetLength(0); x++)
            {
                for (int z = 0; z < tilesLocationMap.GetLength(1); z++)
                {
                    if (x > 0)
                    {
                        tilesLocationMap[x, z].neighbors.Add(tilesLocationMap[x - 1, z]);
                    }
                    if (x < currentMapSizeX - 1)
                    {
                        tilesLocationMap[x, z].neighbors.Add(tilesLocationMap[x + 1, z]);
                    }

                    if (z > 0)
                    {
                        tilesLocationMap[x, z].neighbors.Add(tilesLocationMap[x, z - 1]);
                    }
                    if (z < currentMapSizeZ - 1)
                    {
                        tilesLocationMap[x, z].neighbors.Add(tilesLocationMap[x, z + 1]);
                    }

                    print("total is: " + total);
                }
            }
        }
    }

    public void SpawnTile(int x, int z)
    {
        //GameObject singleTile = new GameObject("x: " + x + ". z: " + z);
        //if its an edge tile, use the edgetile prefab
        //GameObject tempObject = prefabTile;
        //if(isEdge(x,z))
        //{
        //    tempObject = prefabEdgeTile;
        //}
        GameObject singleTile = Instantiate(prefabTiles[grid[x, z]]);
        singleTile.transform.position = new Vector3(x, 0, z);
        singleTile.GetComponent<TileLocation>().SetLocation(x, z, convertToType(grid[x, z]));
        temporaryTiles.Add(singleTile);

        TileInfo newSpawnedTile = new TileInfo();
        newSpawnedTile.coordinates = new Vector3(x, 0, z);
        newSpawnedTile.tileType = convertToType(grid[x, z]);
        savedGridMap.tilesInfo.Add(newSpawnedTile);

        AddTileToMap(singleTile.GetComponent<TileLocation>(), x, z);

    }

    public bool isEdge(int x, int z)
    {
        if ((z == 0 || x == 0) || (z == mapSizeZ - 1 || x == mapSizeX - 1))
        {
            return true;
        }

        return false;
    }

    public void TellMyLocation(Vector3 tileLocation)
    {
        print("TileLocation: " + tileLocation);
    }

    //utility to convert my value to enum
    public TileType convertToType(int value)
    {
        TileType returnThisTile = new TileType();
        switch (value)
        {
            case 0:
                returnThisTile = TileType.edge;
                break;
            case 1:
                returnThisTile = TileType.walkable;
                break;
            case 2:
                returnThisTile = TileType.obstacle;
                break;
        }
        return returnThisTile;
    }

    //utility to convert my value to enum
    public int convertToValue(TileType value)
    {
        int returnThisTile = new int();
        switch (value)
        {
            case TileType.edge:
                returnThisTile = 0;
                break;
            case TileType.walkable:
                returnThisTile = 1;
                break;
            case TileType.obstacle:
                returnThisTile = 2;
                break;
        }
        return returnThisTile;
    }
}
