using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilController : MonoBehaviour, ITimeTracker
{
    //-------------------switch status audio------------
    public AudioClip toFarmlandAudio;
    public AudioClip wateredAudio;
    
    //---------------------soil Status--------------
    public enum SoilStatus
    {
        WildLand, FarmLand, Watered
    }

    public SoilStatus soilStatus;
    public Material wildlandMat, farmlandMat, wateredMat;
    new Renderer renderer;

    //for the player to select a specifc land, set a selectio gameObj
    public GameObject selectLand;

    //cache the time the land was watered
    GameTimeStamp wateredDuration;

    [Header("Plants")]
    public GameObject plantPrefab; //input the plant prefab to instantiate
    PlantBehavior itemPlanted = null; //the plant currently planted on the land

    void Start()
    {
        // get renderer component
        renderer = GetComponent<Renderer>();
        // set default soil status
        SwitchSoilStatus(SoilStatus.WildLand);

        //deselect by default
        SelectLand(false);

        //Add soil controller to TimeManager's listenser
        TimeManager.Instance.RegisterTracker(this);

    }

    public void SwitchSoilStatus(SoilStatus statusToSwitch)

    {
        soilStatus = statusToSwitch;

        Material materialToSwitch = wildlandMat;

        switch (statusToSwitch)
        {
            case SoilStatus.WildLand:
                materialToSwitch = wildlandMat;
                break;
            case SoilStatus.FarmLand:
                AudioManager.instance.AudioPlay(toFarmlandAudio);
                materialToSwitch = farmlandMat;
                break;
            case SoilStatus.Watered:
                AudioManager.instance.AudioPlay(wateredAudio);
                materialToSwitch = wateredMat;

                //Cache the time the soil in the watered status
                wateredDuration = TimeManager.Instance.GetGameTimeStamp();
                break;
        }

        //get the renderer to apply the changes
        renderer.material = materialToSwitch;
    }

    /// <summary>
    /// UI for player to know which soil area is selected
    /// </summary>
    /// <param name="toggle"></param>
    public void SelectLand(bool toggle)
    {
        selectLand.SetActive(toggle);
    }

    public void Interact()
    {
        //SwitchSoilStatus(SoilStatus.FarmLand);
        
        //check player's tool slot
        ItemData toolSlot = InventoryManager.Instance.equippedTool;

        //if nothing equipped, return
        if(toolSlot == null)
        {
            return;
        }

        //cast the teh itemdata in the toolslot as equipmentData
        EquipmentData equipmentTool = toolSlot as EquipmentData;

        //check if it is a type of equipmentData
        if(equipmentTool != null)
        {
            // Get the tool type
            EquipmentData.ToolType toolType = equipmentTool.toolType;

            switch (toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchSoilStatus(SoilStatus.FarmLand);
                    break;
                case EquipmentData.ToolType.WateringCan:
                    SwitchSoilStatus(SoilStatus.Watered);
                    break;
            }

            //no need to check for seed if already confirmed the tool to be the equipment
            return;
        }

        SeedData seedTool = toolSlot as SeedData;
        ///To plant a seed
        ///1. Holding a tool of SeedData
        ///2. soil state is watered or is farmLand
        ///3. no crop planted on the land
        if(seedTool != null && soilStatus != SoilStatus.WildLand && itemPlanted == null)
        {
            //Instantiate the plant Obj parented to the soil prefab
            GameObject plantObj = Instantiate(plantPrefab, transform);
            //move the plant obj to the top of the soil
            plantObj.transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);

            //access the plantBehavior of the item going to plant
            itemPlanted = plantObj.GetComponent<PlantBehavior>();

            //plant it
            itemPlanted.Plant(seedTool);

        }

    }

    // the soil can only be watered once in 24 hours
    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        // check the watered status lasting time
        if(soilStatus == SoilStatus.Watered)
        {
            //passed time since the soil's last watered time
            int hoursElapsed = GameTimeStamp.CompareTimestamps(wateredDuration, timeStamp);
            Debug.Log(hoursElapsed + "since last watered");

            //everytime watered plant grow
            if(itemPlanted != null)
            {
                itemPlanted.Grow();
            }

            //switch the soil status back 24 h after watered 
            if (hoursElapsed > 24)
            {
                SwitchSoilStatus(SoilStatus.FarmLand);
            }
        }
    }

}
