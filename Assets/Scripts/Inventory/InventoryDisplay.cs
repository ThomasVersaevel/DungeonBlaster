using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    //[SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDict;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDict => slotDict;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDict)
        {
            if (slot.Value == updatedSlot) // Slot value = the slot backend
            {
                slot.Key.UpdateUISlot(updatedSlot); // slot key = UI frontend slot
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        Debug.Log("Slot clicked");
    }
}
