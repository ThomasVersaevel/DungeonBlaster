using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;
    public List<LootItem> commonItems;
    public List<LootItem> uncommonItems;
    public List<LootItem> rareItems;
    public List<LootItem> legendaryItems;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollLoot()
    {
        //Roll for rarity
        int rarityRoll = Random.Range(1, 101);
        if (rarityRoll <= 75)
        {
            //Common item
            int itemRoll = Random.Range(0, commonItems.Count);
            Debug.Log("You received a " + commonItems[itemRoll].itemName);
        }
        else if (rarityRoll <= 90)
        {
            //Uncommon item
            int itemRoll = Random.Range(0, uncommonItems.Count);
            Debug.Log("You received a " + uncommonItems[itemRoll].itemName);
        }
        else if (rarityRoll <= 98)
        {
            //Rare item
            int itemRoll = Random.Range(0, rareItems.Count);
            Debug.Log("You received a " + rareItems[itemRoll].itemName);
        }
        else
        {
            //Legendary item
            int itemRoll = Random.Range(0, legendaryItems.Count);
            Debug.Log("You received a " + legendaryItems[itemRoll].itemName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        anim.SetTrigger("Collision");
    }
}

[System.Serializable]
public class LootItem
{
    public string itemName;
    public Sprite itemSprite;
}
