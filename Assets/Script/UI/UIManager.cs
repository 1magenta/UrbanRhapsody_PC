using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, ITimeTracker
{
    public GameObject inGameOverlayPanel;
    public Button confirmButton;
    private bool isDisplayed = false;
    public AudioClip victoryClip;

    [Header("Value Frame")]
    public TextMeshProUGUI pleasureValText;
    public TextMeshProUGUI comfortValText;
    int pleasureVal = 25;
    int comfortVal = 30;

    [Header("Status Frame")]
    // Tool equip slot on the status bar
    public Image toolEquipImg;

    //-------------------Time UI Varible------------------
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dateText;

    //----------------------inventory UI Varible-----------------
    [Header("Inventory System")]
    // Inventory Panel
    public GameObject inventoryPanel;

    //Equip tool and item slot UI on the Inventory Panel
    public HandInventorySlotUI toolHandSlot;
    public HandInventorySlotUI itemHandSlot;

    //slot UI for each element icon in the inventory panel
    public InventorySlotUI[] toolSlots;
    public InventorySlotUI[] itemSlots;

    //Item info panel
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemIntroText;
    public static UIManager Instance { get; private set; }

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

        // init value frame UI
        UpdatePleasureVal(0);
        UpdateComfortVal(0);
    }

    private void Start()
    {
        RenderInventory();
        AssignSlotIndexes();

        //add UIManager to the list of objects TimeManger will notify when the time updates
        TimeManager.Instance.RegisterTracker(this);
    }

    void Update()
    {
        DisplayInGameOverlayPanel();
    }

    /// <summary>
    /// Iterate the slot UI elements and assign its reference slot index
    /// </summary>
    public void AssignSlotIndexes()
    {
        for(int i=0; i<toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }

    public void RenderInventory()
    {
        //get inventory tool slots from the inventory manager
        ItemData[] inventoryToolSlots = InventoryManager.Instance.tools;

        //get the inventory item slots
        ItemData[] inventoryItemSlots = InventoryManager.Instance.items;

        //render the tool section
        RenderInventoryPanel(inventoryToolSlots, toolSlots);

        //render item section
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        //render equipped slot
        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
        itemHandSlot.Display(InventoryManager.Instance.equippedItem);

        //get equipped tool from InventoryManager
        ItemData equippedTool = InventoryManager.Instance.equippedTool;

        if (equippedTool != null)
        {
            //Display item on UI
            toolEquipImg.sprite = equippedTool.thumbnail;
            //this.toolEquipImg = equippedTool;

            toolEquipImg.gameObject.SetActive(true);
            return;
        }

        toolEquipImg.gameObject.SetActive(false);
    }

    /// <summary>
    /// Iterate the slot and display the slot UI
    /// </summary>
    /// <param name="slots"></param>
    /// <param name="uiSlots"></param>
    void RenderInventoryPanel(ItemData[] slots, InventorySlotUI[] uiSlots)
    {
        for(int i = 0; i < uiSlots.Length; i++)
        {
            //display UI for tool/item selction
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        //if the panel is hidden, show it and vice versa
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventory();
    }

    public void DisplayItemInfo(ItemData introData)
    {
        if(introData == null)
        {
            itemNameText.text = "";
            itemIntroText.text = "";
            return;
        }
        itemNameText.text = introData.name;
        itemIntroText.text = introData.description;

        
    }

    //-------------------------Time UI-----------------------------
    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //For timeText UI

        //get hours and minutes
        int hours = timeStamp.hour;
        int minutes = timeStamp.minute;

        //AM PM
        string prefix = "AM";

        //24 hours to 12 hours
        if(hours > 12)
        {
            //am to pm
            prefix = "PM";
            hours -= 12;
        }

        //update time text
        timeText.text = prefix + " " + hours + ":" + minutes.ToString("00");

        //For dateText UI
        int day = timeStamp.day;
        string season = timeStamp.season.ToString();
        string dayOfThWeek = timeStamp.GetDayOfTheWeek().ToString();

        dateText.text = season + " " + day + "(" + dayOfThWeek + ")";
    }

    public void UpdatePleasureVal(int addVal)
    {
        pleasureVal += addVal;
        pleasureValText.text = "Pleasure Value: " + pleasureVal;
    }

    public void UpdateComfortVal(int addVal)
    {
        comfortVal += addVal;
        comfortValText.text = "Comfort Value: " + comfortVal;
    }

    public void DisplayInGameOverlayPanel()
    {
        if (comfortVal >= 60 && pleasureVal >= 30 && isDisplayed == false)
        {
            AudioManager.instance.AudioPlay(victoryClip);
            inGameOverlayPanel.gameObject.SetActive(true);
        }
        
    }

    public void disablePanel()
    {
        inGameOverlayPanel.gameObject.SetActive(false);
        isDisplayed = true;
    }
}
