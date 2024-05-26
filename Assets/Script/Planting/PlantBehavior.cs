using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public AudioClip seedPlantedAudio;
    public AudioClip ripeAudio;

    SeedData seedToGrow; //info the plant to grow

    [Header("Stages of life")]
    public GameObject seed;
    private GameObject seedling;
    private GameObject ripePlant;

    //the growth points of the crop
    int growthPoint;
    //total growth points for the plant to ripe
    int maxGrowthPoint;

    public enum PlantState
    {
        Seed, Seedling, ripePlant
    }

    public PlantState plantState; //curr state of a plant's growth

    //initi the plant gameObj
    //call when need a seed
    public void Plant(SeedData seedToGrow)
    {
        this.seedToGrow = seedToGrow;

        // instantiate the seedling and ripe gameObj
        seedling = Instantiate(seedToGrow.seeding, transform);

        // access the plant item data (store in the item "game model")
        ItemData plantToYield = seedToGrow.plantToYield;

        //instantiate the ripe plant
        ripePlant = Instantiate(plantToYield.gameModel, transform);

        //convert daysToGrow into hours
        int hoursToGrow = GameTimeStamp.DaysToHours(seedToGrow.daysToGrow);
        //convert to minutes
        maxGrowthPoint = GameTimeStamp.HoursToMinutes(hoursToGrow);

        //set the init state to seed
        SwitchGrowthState(PlantState.Seed);

    }


    public void Grow()
    {
        growthPoint++;

        //the seed sprout into a seedling when growthPoint is 50%
        if(growthPoint >= maxGrowthPoint / 2 && plantState == PlantState.Seed)
        {
            SwitchGrowthState(PlantState.Seedling);
        }

        //seedling state to ripe
        if (growthPoint == maxGrowthPoint)
        {
            SwitchGrowthState(PlantState.ripePlant);
        }
    }

    void SwitchGrowthState(PlantState stateToSwitch)
    {
        //Reset and set all GameObj to inactive
        seed.SetActive(false);
        seedling.SetActive(false);
        ripePlant.SetActive(false);

        switch (stateToSwitch)
        {
            case PlantState.Seed:
                AudioManager.instance.AudioPlay(seedPlantedAudio);
                seed.SetActive(true);
                break;
           
            case PlantState.Seedling:
                AudioManager.instance.AudioPlay(ripeAudio);
                seedling.SetActive(true);
                break;

            case PlantState.ripePlant:
                AudioManager.instance.AudioPlay(ripeAudio);
                ripePlant.SetActive(true);
                break;
        }

        //set the curr plant state to the state switching to 
        plantState = stateToSwitch;
    }

    //switch state for the plant to grow
}
