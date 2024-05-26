using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    PlayerController playerController;

    SoilController selectedArea;


    // Start is called before the first frame update
    void Start()
    {
        //access player controller
        playerController = transform.parent.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
    }

    /// <summary>
    /// when interaction raycast hit the interactables
    /// </summary>
    /// <param name="hit"></param>
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;

        // when interact with the planting area, select the soil player standing on
        if(other.tag == "plantingLand")
        {
            // Get the planting land componenet
            SoilController soil = other.GetComponent<SoilController>();
            // select the planting area
            SelectPlantingArea(soil);
            return;
        }

        //deselect when player is not standing on any soil
        if(selectedArea != null)
        {
            selectedArea.SelectLand(false);
            selectedArea = null;
        }
    }

    /// <summary>
    /// to select and de-select the planting area when player interact with the soil
    /// </summary>
    /// <param name="soil"></param>
    void SelectPlantingArea(SoilController soil)
    {
        if (selectedArea != null)
        {
            selectedArea.SelectLand(false);
        }

        selectedArea = soil;
        soil.SelectLand(true);
    }

     /// <summary>
     /// when the player press the tool button
     /// </summary>
    public void Interact()
    {
        if(selectedArea != null)
        {
            selectedArea.Interact();
            return;
        }
        Debug.Log("Not on any land!");
    }
}
