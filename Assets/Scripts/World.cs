using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private int levelSelector;

    //Array to keep current GameObjects
    public GameObject[] CastleList;
    public GameObject[] CaveList;
    public GameObject[] FieldList;
    public GameObject[] SwampList;
    private GameObject[] WallTiles;
    private GameObject[] FloorTiles;

    private GameObject currentTile;

    private int width;
    private int height;

    // Start is called before the first frame update
    void Start()
    {
        levelSelector = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventSystem>().getLevelSelector();
        Random.InitState(12345); // seed the random number generator
                                 // Lists to keep tilesets, 0, 1 are floor - 2 is regular wall - 3, 4 are corrupted, rest is arbitrary
                                 // CastleList = new GameObject[9]  {castleFloorTile,   castleMarbleFloorTile,  castleWallTile, castleWallTileCorrupted,    castleWallTileEye,  castleWallTileCurve,    null,                  null,            null};
        //FloorTiles = new GameObject[2] {castleFloorTile, castleFloorTile };
        // TODO: change to level selector
        if (levelSelector == 1)
        {
            WallTiles = FieldList;
            FloorTiles = FieldList;
        }
        else if (levelSelector == 2)
        {
            WallTiles = SwampList;
            FloorTiles = SwampList;
        } else if (levelSelector == 3)
        {
            WallTiles = CastleList;
            FloorTiles = CastleList; // todo wooden floor
        }

        width = 75;
        height = 75;

        //InitializeSetting();
        generateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeSetting()
    {

        currentTile = WallTiles[0]; //wall
        for (int i = -10; i <= 30; i++)
        {
            for (int j = -10; j <= 30; j++)
            {
                if (i > 0 && i < width && j > 0 && j < height) { }
                else {
                    if (Random.Range(0, 10) <= 1)
                    { //10% chance for corupt
                        currentTile = WallTiles[Random.Range(0, WallTiles.Length)];
                    } else
                    {
                        currentTile = WallTiles[0];
                    }
                    GameObject TheTile = Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }

    private GameObject SpritePicker()
    {
        return currentTile;
    }

    void generateLevel()
    {
        // create open map
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++) // collumn wise map gen
            {
                if (i == 0 || j == 0 || i == width - 2 || j == height - 2) //create walls around map
                {
                    if (Random.Range(0, 10) <= 1)
                    { //10% chance for corupt
                        currentTile = WallTiles[Random.Range(0, WallTiles.Length)];
                        GameObject t = (Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity));
                        t.GetComponent<Tile>().setSolid();
                    }
                    else
                    {
                        currentTile = WallTiles[0];
                        GameObject t = (Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity));
                        t.GetComponent<Tile>().setSolid();
                    }
                }
                else
                {
                    if (Random.Range(0, 25) <= 1) // slightly different tile 1/25 chance
                    {
                        currentTile = FloorTiles[Random.Range(1, FloorTiles.Length)];
                    }
                    else
                    {
                        currentTile = FloorTiles[0];
                    }
                    GameObject t = (Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity));
                }
            }
        }
    }

    void roomByLoop()
    {
        // create open map
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++) // collumn wise map gen
            {
                if (i == 0 || j == 0 || i == width-2 || j == height-2) //create walls around map
                {
                    if (Random.Range(0, 10) <= 1)
                    { //10% chance for corupt
                        currentTile = WallTiles[Random.Range(0, WallTiles.Length)];
                        GameObject t = (Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity));
                        t.GetComponent<Tile>().setSolid();
                    }
                    else
                    {
                        currentTile = WallTiles[0];
                        GameObject t = (Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity));
                        t.GetComponent<Tile>().setSolid();
                    }
                }
                else
                { 
                    if (Random.Range(0, 25) <= 1) // slightly different tile 1/25 chance
                    {
                        currentTile = FloorTiles[Random.Range(1, FloorTiles.Length)];
                    } else
                    {
                        currentTile = FloorTiles[0];
                    }
                    GameObject t = (Instantiate(currentTile, new Vector3(i, j, 0), Quaternion.identity));
                    //t.SendMessage("tileSetup", new Vector3(i, j, 0));
                }
            }
        }
    }

    // deprecated
    //void roomFromTxt(Sprite[] sprites) // 0 and 1 for floor, 2 is wall 3 4 are corrupted rest is arbitrary.
    //{
    //    int width = 25;
    //    TextAsset t1 = (TextAsset)Resources.Load("map4", typeof(TextAsset));
    //    string str = t1.text;
    //    string s = str.Replace("\n", null); 
    //    s = s.Replace("\r", null); //as it turns out this was the issue

    //    int column, row = 0;

    //    for (int i = 0; i < s.Length; i++)
    //    {
    //        if (i % width == 0)
    //        {
    //            row++;
    //        }
    //        column = i % width;
    //        //print("c: "+ column + "r: " + row + "i :" + s[i] + "\n");

    //        if (s[i] == '1') //create wall
    //        {
    //            if (Random.Range(0, 10) <= 1)
    //            { //10% chance for corupt
    //                currentTile = sprites[Random.Range(3, 6)];
    //                GameObject t = (Instantiate(Tile, new Vector3(column, row, 0), Quaternion.identity));
    //                t.GetComponent<SpriteRenderer>().sprite = currentTile;
    //                t.SendMessage("setSolid");
    //                t.SendMessage("AnimateTile");
    //            }
    //            else
    //            {
    //                currentTile = sprites[2];
    //                GameObject t = (Instantiate(Tile, new Vector3(column, row, 0), Quaternion.identity));
    //                t.GetComponent<SpriteRenderer>().sprite = currentTile;
    //                t.SendMessage("setSolid");
    //            }
                
                
                
    //        }
    //        else if (s[i] == '0') //create floor
    //        {
    //            currentTile = sprites[0];
    //            GameObject t = (Instantiate(Tile, new Vector3(column, row, 0), Quaternion.identity));
    //            //t.SendMessage("tileSetup", new Vector3(i, j, 0));
    //            t.GetComponent<SpriteRenderer>().sprite = currentTile;
    //        }
    //        else if (s[i] == '2')
    //        {
    //            //enemy portal or item
    //        }
    //        else { //if unknown symbol make a floor tile
    //            currentTile = sprites[0];
    //            GameObject t = (Instantiate(Tile, new Vector3(column, row, 0), Quaternion.identity));
    //            //t.SendMessage("tileSetup", new Vector3(i, j, 0));
    //            t.GetComponent<SpriteRenderer>().sprite = currentTile;
    //        }
    //    }
    //}

    //// deprecated
    //void generateSpawnRoom(Sprite[] sprites)
    //{
    //    int width = 25;
    //    int height = 25;

    //    for (int i = 0; i <= width; i++)
    //    {
    //        for (int j = 0; j <= height; j++)
    //        {
    //            if (i == 0 || i == width || j == 0 || j == height)
    //            {
    //                if (Random.Range(0, 10) < 1)
    //                {
    //                    currentTile = sprites[Random.Range(3, 6)];
    //                }
    //                else
    //                {
    //                    currentTile = sprites[2];
    //                }
    //                GameObject TheTile = Instantiate(Tile, transform) as GameObject;
    //                TheTile.SendMessage("tileSetup", new Vector3(i, j, 0));
    //                TheTile.GetComponent<SpriteRenderer>().sprite = currentTile;
    //                TheTile.SendMessage("setSolid");
    //            }
    //            else
    //            {
    //                currentTile = sprites[0];
    //                GameObject TheTile = Instantiate(Tile, transform) as GameObject;

    //                TheTile.SendMessage("tileSetup", new Vector3(i, j, 0));
    //                TheTile.GetComponent<SpriteRenderer>().sprite = currentTile;
    //            }
    //        }
    //    }   
    //}
}
