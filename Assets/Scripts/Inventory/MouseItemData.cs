using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    private Camera camera;
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;

    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot);
        ItemSprite.sprite = invSlot.ItemData.Icon;
        ItemCount.text = invSlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = new Vector3 (camera.ScreenToWorldPoint(Input.mousePosition).x, 
                camera.ScreenToWorldPoint(Input.mousePosition).y, 0 );
  


        }
    }

    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    //public static bool IsPointerOverUIObject(Camera camera)
    //{
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
    //    eventDataCurrentPosition.position = new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x,
    //            camera.ScreenToWorldPoint(Input.mousePosition).y, 0);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //    return results.Count > 0;
    //}
}
