using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    ItemData itemToDisplay;

    public Image itemDisplayImg;

    public enum InventoryType
    {
        Item, Tool
    }
    //determine which inventory section
    public InventoryType inventoryType;

    int slotIndex;

    public void Display(ItemData itemToDisplay)
    {
        if(itemToDisplay != null)
        {
            //Display item on UI
            itemDisplayImg.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;

            itemDisplayImg.gameObject.SetActive(true);
            return;
        }

        itemDisplayImg.gameObject.SetActive(false);
    }

    /// <summary>
    /// click interact to select item to equip
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //equip item from inventory to hand
        InventoryManager.Instance.EquipItemInHand(slotIndex, inventoryType);
    }

    //set the slot index
    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(null);
    }
}
