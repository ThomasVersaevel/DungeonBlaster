using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour
{
    private int levelSelector;

    //Array to keep current GameObjects
    public List<GameObject> CastleList;

    public List<GameObject> CaveList;

    public List<GameObject> commonFieldTiles;
    public List<GameObject> rareFieldTiles;

    // Swamp Level
    public List<GameObject> commonFloorSwampTiles;
    public List<GameObject> rareFloorSwampTiles;

    // runtime tile lists
    public List<GameObject> WallTiles;
    public List<GameObject> FloorTiles;


    private GameObject currentTile;


    private int commonFloorIndex;
    private int rareFloorIndex;
    private int commonWallIndex;
    private int rareWallIndex;

    private int width;
    private int height;

    // Start is called before the first frame update
    void Start()
    {
        levelSelector = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventSystem>().GetLevelSelector();
        Random.InitState(12345); // seed the random number generator

        if (levelSelector == 1)
        {
            commonFloorIndex = commonFieldTiles.Count;
            rareFloorIndex = commonFloorIndex + rareFieldTiles.Count;

            commonWallIndex = commonFieldTiles.Count;
            rareWallIndex = rareFieldTiles.Count + commonFieldTiles.Count;
            WallTiles = commonFieldTiles.Concat(rareFieldTiles).ToList();
            FloorTiles = commonFieldTiles.Concat(rareFieldTiles).ToList();
        }
        else if (levelSelector == 2)
        {
            commonFloorIndex = commonFloorSwampTiles.Count;
            rareFloorIndex = commonFloorIndex + rareFloorSwampTiles.Count;

            commonWallIndex = commonFloorSwampTiles.Count;
            rareWallIndex = rareFloorSwampTiles.Count + commonFloorSwampTiles.Count;

            WallTiles =  commonFloorSwampTiles.Concat(rareFloorSwampTiles).ToList();
            FloorTiles = commonFloorSwampTiles.Concat(rareFloorSwampTiles).ToList();
        }
        else if (levelSelector == 3)
        {
            WallTiles = CastleList;
            FloorTiles = CastleList; // todo wooden floor
        }

        width = 75;
        height = 75;

        // set tilesets as multiple arrays
        // First floortiles that are common, then rare then walltiles and save indices as lenghts of each array to use later for probability

        GenerateLevel();
    }


    void GenerateLevel()
    {
        // create open map
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++) // collumn wise map gen
            {
                if (i == 0 || j == 0 || i == width - 1 || j == height - 1) //create walls around map
                {
                    if (Random.Range(0, 25) <= 1)
                    { // Chance for rare tile
                        currentTile = WallTiles[Random.Range(commonWallIndex, rareWallIndex)];
                        GameObject t = (Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity));
                        t.GetComponent<Tile>().setSolid();
                    }
                    else // common tile
                    {
                        currentTile = WallTiles[Random.Range(0, commonWallIndex)];
                        GameObject t = (Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity));
                        t.GetComponent<Tile>().setSolid();
                    }
                }
                else
                {
                    if (Random.Range(0, 25) <= 1) // Chance for rare tile
                    {
                        currentTile = FloorTiles[Random.Range(commonFloorIndex, rareFloorIndex)];
                    }
                    else
                    {
                        currentTile = FloorTiles[Random.Range(0, commonFloorIndex)];
                    }
                    Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }
}