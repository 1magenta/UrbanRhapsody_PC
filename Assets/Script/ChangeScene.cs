using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string level2Load;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("paintingTool"))
        {
            SceneManager.LoadScene(level2Load);
        }    
    }
}
