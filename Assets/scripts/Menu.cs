using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene(1);
    
    }
    public void stopButton() 
    {
        Application.Quit();

    }
}

