using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject Tile;
    //Castle
    public Sprite castleWallTile;
    public Sprite castleWallTileCorrupted;
    public Sprite castleWallTileEye;
    public Sprite castleWallTileCurve;
    public Sprite castleFloorTile;
    public Sprite castleMarbleFloorTile;
    //CAVE
    public Sprite caveFloorTile;
    public Sprite caveWallTile;
    public Sprite caveWallTileTentacle;
    public Sprite caveWallTileSpike;
    public Sprite caveRiverHorizontal;
    public Sprite caveRiverVertical;
    public Sprite caveRiverJunction;

    //Array to keep current sprites
  

    private Sprite[] CastleList;
    private Sprite[] CaveList;
    private int[] tileMatrix;
    private int width;
    private int height;

    private Sprite currentTile;

    // Start is called before the first frame update
    void Start()
    {
        width = 25;
        height = 25;
        //Lists to keep tilesets, 0, 1 are floor - 2 is regular wall - 3, 4 are corrupted, rest is arbitrary
        CastleList = new Sprite[9]  {castleFloorTile,   castleMarbleFloorTile,  castleWallTile, castleWallTileCorrupted,    castleWallTileEye,  castleWallTileCurve,    null,                  null,            null};
        CaveList = new Sprite[9]    { caveFloorTile,    caveFloorTile,          caveWallTile,   caveWallTileTentacle,       caveWallTileSpike,  caveWallTileSpike,      caveRiverHorizontal, caveRiverVertical, caveRiverJunction};
        InitializeSetting(CastleList);
        //generateSpawnRoom(CastleList);
        roomFromTxt(CastleList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeSetting(Sprite[] sprites)
    {
        currentTile = sprites[2];//wall
        for (int i = -10; i <= 30; i++)
        {
            for (int j = -10; j <= 30; j++)
            {
                if (i > 0 && i < width && j > 0 && j < height) { }
                else {
                    GameObject TheTile = Instantiate(Tile, transform) as GameObject;
                    TheTile.SendMessage("tileSetup", new Vector3(i, j, 0));
                    if (Random.Range(0, 10) <= 1)
                    { //10% chance for corupt
                        currentTile = sprites[Random.Range(3, 6)];
                    } else
                    {
                        currentTile = sprites[2];
                    }
                    TheTile.GetComponent<SpriteRenderer>().sprite = currentTile;
                }
            }
        }
    }

    private Sprite SpritePicker()
    {
        return currentTile;
    }

    void roomFromTxt(Sprite[] sprites) // 0 and 1 for floor, 2 is wall 3 4 are corrupted rest is arbitrary.
    {
        TextAsset t1 = (TextAsset)Resources.Load("map4", typeof(TextAsset));
        string str = t1.text;
        string s = str.Replace("\n", null); 
        s = s.Replace("\r", null); //as it turns out this was the issue

        int column, row = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (i % width == 0)
            {
                row++;
            }
            column = i % width;
            //print("c: "+ column + "r: " + row + "i :" + s[i] + "\n");

            if (s[i] == '1') //create wall
            {
                if (Random.Range(0, 10) <= 1)
                { //10% chance for corupt
                    currentTile = sprites[Random.Range(3, 6)];
                    GameObject t = (Instantiate(Tile, new Vector3(column, row, 0), Quaternion.identity));
                    t.GetComponent<SpriteRenderer>().sprite = currentTile;
                    t.SendMessage("setSolid");
                    t.SendMessage("AnimateTile");
                }
                else
                {
                    currentTile = sprites[2];
                    GameObject t = (Instantiate(Tile, new Vector3(column, row, 0), Quaternion.identity));
                    t.GetComponent<SpriteRenderer>().sprite = currentTile;
                    t.SendMessage("setSolid");
                }
                
                
                
            }
            else if (s[i] == '0') //create floor
            {
                currentTile = sprites[0];
                GameObject t = (Instantiate(Tile, new Vector3(column, row, 0), Quaternion.identity));
                //t.SendMessage("tileSetup", new Vector3(i, j, 0));
                t.GetComponent<SpriteRenderer>().sprite = currentTile;
            }
            else if (s[i] == '2')
            {
                //enemy portal or item
            }
            else { //if unknown symbol make a floor tile
                currentTile = sprites[0];
                GameObject t = (Instantiate(Tile, new Vector3(column, row, 0), Quaternion.identity));
                //t.SendMessage("tileSetup", new Vector3(i, j, 0));
                t.GetComponent<SpriteRenderer>().sprite = currentTile;
            }
        }
    }

    void generateSpawnRoom(Sprite[] sprites)
    {
        for (int i = 0; i <= width; i++)
        {
            for (int j = 0; j <= height; j++)
            {
                if (i == 0 || i == width || j == 0 || j == height)
                {
                    if (Random.Range(0, 10) < 1)
                    {
                        currentTile = sprites[Random.Range(3, 6)];
                    }
                    else
                    {
                        currentTile = sprites[2];
                    }
                    GameObject TheTile = Instantiate(Tile, transform) as GameObject;
                    TheTile.SendMessage("tileSetup", new Vector3(i, j, 0));
                    TheTile.GetComponent<SpriteRenderer>().sprite = currentTile;
                    TheTile.SendMessage("setSolid");
                }
                else
                {
                    currentTile = sprites[0];
                    GameObject TheTile = Instantiate(Tile, transform) as GameObject;

                    TheTile.SendMessage("tileSetup", new Vector3(i, j, 0));
                    TheTile.GetComponent<SpriteRenderer>().sprite = currentTile;
                }
            }
        }   
    }
}
