using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public int zoomSpeed2d;
    public int minZoom2d;
    public int maxZoom2d;

    public int zoomSpeed3d;
    public int minZoom3d;
    public int maxZoom3d;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //----------------------------2D Camera-----------------------------------------
        if (Camera.main.orthographic)
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //Zoom in 
                Camera.main.orthographicSize -= zoomSpeed2d * Time.deltaTime;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //Zoom out
                Camera.main.orthographicSize += zoomSpeed2d * Time.deltaTime;
            }

            //Restrict the value
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom2d, maxZoom2d);
        }

        //----------------------------3D Camera-----------------------------------------
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //Zoom in 
                Camera.main.fieldOfView -= zoomSpeed3d * Time.deltaTime;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //Zoom out
                Camera.main.fieldOfView += zoomSpeed3d * Time.deltaTime;
            }

            //Restrict the value
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom3d, maxZoom3d);
        }
    }
}
