using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class ItemPickup : MonoBehaviour
{
    public float PickupRadius = 1.5f;
    public InventoryItemData ItemData;

    private CircleCollider2D coll;

    private void Awake()
    {
        coll = gameObject.GetComponent<CircleCollider2D>();
        coll.isTrigger = true;
        coll.radius = PickupRadius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
