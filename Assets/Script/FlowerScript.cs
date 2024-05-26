using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    private int pleasureRate = 1;
    public AudioClip flowerClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Lily");
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            Debug.Log("yes Lily");
            AudioManager.instance.AudioPlay(flowerClip);
            if(pleasureRate != 0)
            {
                UIManager.Instance.UpdatePleasureVal(1);
                pleasureRate--;
            }

        }
    }
}
