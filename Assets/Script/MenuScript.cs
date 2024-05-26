using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void btn_StartTheGame()
    {
        Debug.Log("Game Launching");
        SceneManager.LoadScene("OutDoor");
    }

    public void btn_Credit()
    {
        Debug.Log("Credit");
        SceneManager.LoadScene("CreditPage");
    }

    public void btn_Tutorial()
    {
        Debug.Log("Tutorial");
        SceneManager.LoadScene("Tutorial");
    }

    public void btn_GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}
