using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedData : ItemData
{    
    public int daysToGrow; //Seed to plant growth time

    public ItemData plantToYield; //the plant for the seed to yield

    public GameObject seeding; // seeding gameobj
}
