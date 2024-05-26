using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        //if there is more than one instance, destory the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // set the static instance to this instance
            Instance = this;
        }
    }

    [Header("Tools")]
    // All tools in a set
    public ItemData[] tools = new ItemData[8];
    // the tool player equipped
    public ItemData equippedTool = null;


    [Header("Items")]
    // all items in a set
    public ItemData[] items = new ItemData[8];
    // the item player equipped
    public ItemData equippedItem = null;

    //Equipping

    //Handles movement of item from inventory to hand
    public void EquipItemInHand(int slotIndex, InventorySlotUI.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlotUI.InventoryType.Item)
        {
            //Cache the Inventory slot ItemData from InventoryManager
            ItemData itemToEquip = items[slotIndex];

            //change the inventory slot to the hand's
            items[slotIndex] = equippedItem;

            //change the hand's slot to the inventory slot's
            equippedItem = itemToEquip;
        }
        else
        {
            //Cache the Inventory slot ItemData from InventoryManager
            ItemData toolToEquip = tools[slotIndex];

            //change the inventory slot to the hand's
            tools[slotIndex] = equippedTool;

            //change the hand's slot to the inventory slot's
            equippedTool = toolToEquip;
        }

        //update UI
        UIManager.Instance.RenderInventory();
    }

    /// <summary>
    /// Move the item from hand to inventory
    /// </summary>
    /// <param name="inventory"></param>
    public void ItemBackToInventory(InventorySlotUI.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlotUI.InventoryType.Item)
        {
            // iterate inventory slot, and move the item back to the first emplty slot
            for(int i=0; i < items.Length; i++)
            {
                if(items[i] == null)
                {
                    items[i] = equippedItem;
                    //remove from hand
                    equippedItem = null;
                    break;
                }
            }
        }
        else
        {
            // iterate inventory slot, and move the item back to the first emplty slot
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] == null)
                {
                    tools[i] = equippedTool;
                    //remove from hand
                    equippedTool = null;
                    break;
                }
            }
        }

        //Update UI
        UIManager.Instance.RenderInventory();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
