using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    public float rotateSpeed;
    private void OnMouseDrag()
    {
        float Y = Input.GetAxis("Mouse Y") * rotateSpeed;
        float X = Input.GetAxis("Mouse X") * rotateSpeed;

        transform.Rotate(Vector3.right, -Y);
        transform.Rotate(Vector3.up, X);
    }
}
