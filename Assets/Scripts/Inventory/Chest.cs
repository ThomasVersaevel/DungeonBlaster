using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;
    public List<GameObject> commonItems;
    public List<GameObject> rareItems;
    public List<GameObject> epicItems;
    public List<GameObject> legendaryItems;
    public int rarityLevel = 0;
    private bool coll = false;
    private float timer;
    private bool locked = true;

    // Start is called before the first frame update
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (coll)
        {
            timer += Time.deltaTime;
            if (timer > 0.3f)
            {
                Instantiate(RollLoot(rarityLevel), transform.position, transform.rotation);
                timer = 0;
                coll = false;
                locked = false;
            }
        }
    }

    public GameObject RollLoot(int chanceMultiplier)
    {
        //Roll for rarity
        int rarityRoll = Random.Range(1, 101);
        if (rarityRoll <= 75 - chanceMultiplier)
        {
            //Common item
            int itemRoll = Random.Range(0, commonItems.Count);
            Debug.Log("You received a " + commonItems[itemRoll].name);
            return commonItems[itemRoll];
        }
        else if (rarityRoll <= 90 - chanceMultiplier)
        {
            //Uncommon item
            int itemRoll = Random.Range(0, rareItems.Count);
            Debug.Log("You received a " + rareItems[itemRoll].name);
            return rareItems[itemRoll];
        }
        else if (rarityRoll <= 98 - chanceMultiplier)
        {
            //Rare item
            int itemRoll = Random.Range(0, epicItems.Count);
            Debug.Log("You received a " + epicItems[itemRoll].name);
            return epicItems[itemRoll];
        }
        else
        {
            //Legendary item
            int itemRoll = Random.Range(0, legendaryItems.Count);
            Debug.Log("You received a " + legendaryItems[itemRoll].name);
            return legendaryItems[itemRoll];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player") && locked)
        {
            anim.SetTrigger("Collision");
            coll = true;
        }
    }
}
