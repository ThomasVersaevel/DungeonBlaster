using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour { 

    private Sprite currentTile;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tileSetup(Vector3 pos)
    {
        this.position = pos;

        this.transform.position = position;
    }
    public void setSolid()
    {
        gameObject.AddComponent<BoxCollider2D>();
    }
    public void AnimateTile()
    {
        Animator anim = gameObject.AddComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("EyeWall") as RuntimeAnimatorController;
        //anim.Play("CastleWallTileCorrupt_"+Random.Range(0, 11));
    }
}
