using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem // fires event when item is added to a slot
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    // [SerializeField] private GameObject[] weaponArray;

    private Dictionary<int, GameObject> weapons;

    private GameObject weaponBox;

    public Dictionary<int, GameObject> Weapons => weapons;
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;


    public InventorySystem(int size, GameObject[] weaponArray, GameObject WeaponBox)
    {
        inventorySlots = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
        weapons = new Dictionary<int, GameObject>(weaponArray.Length);
        for (int i = 0; i < weaponArray.Length; i++)
        {
            weapons.Add(i, weaponArray[i]);
        }
        weaponBox = WeaponBox;
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlots)) // check item exists in inv
        {
            foreach (var slot in invSlots)
            {
                if(slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        if (HasFreeSlot(out InventorySlot freeSlot)) //get first available slot
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            // Instantiate weapon in the weapon box on player
            var _weapon = GameObject.Instantiate(weapons[itemToAdd.ID], weaponBox.transform);
            _weapon.transform.position += new Vector3(-0.3f, -0.1f, 0); // position weapon to hand
            return true;
        }
        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlots)
    {
        invSlots = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlots == null ? true : false;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}
