using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class ItemPickup : MonoBehaviour
{
    public float PickupRadius = 1.5f;
    public InventoryItemData ItemData;

    private CircleCollider2D coll;

    private float timer;

    private void Awake()
    {
        coll = gameObject.GetComponent<CircleCollider2D>();
        coll.isTrigger = true;
        coll.radius = PickupRadius;
        coll.enabled = false;
    }
    // On spawning pickup slowly fall down to the ground and then activate collider
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 0.5f)
        {
            coll.enabled = true;
        }
        if (timer < 0.8f)
        {
            transform.position += new Vector3(0, -0.5f * Time.deltaTime, 0);
        }
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
