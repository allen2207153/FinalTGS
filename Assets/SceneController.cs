using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    
    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void option()
    {
        Debug.Log("OPTION");

    }
    public void quitGame()
    {
        Debug.Log("QUIT");
    }
}