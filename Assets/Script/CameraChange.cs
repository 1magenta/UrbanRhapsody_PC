using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public int camMode;
    public GameObject cam3rd; // mode 0
    public GameObject cam1st; // mode 1
    

     
    void Update()
    {
        if (Input.GetButtonDown("Camera")) //which is c
        {
            if(camMode == 1)
            {
                camMode = 0;
            }
            else
            {
                camMode += 1;
            }
            StartCoroutine(CamChange());
        }

    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (camMode == 0)
        {
            cam3rd.SetActive(true);
            cam1st.SetActive(false);
        }
        if (camMode == 1)
        {
            cam1st.SetActive(true);
            cam3rd.SetActive(false);
        }
    }
}
