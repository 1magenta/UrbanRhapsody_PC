using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandInventorySlotUI : InventorySlotUI
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        //move item from hand back to inventory
        InventoryManager.Instance.ItemBackToInventory(inventoryType);
    }
}
