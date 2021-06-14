using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void showCredits()
    {
        SceneManager.LoadScene(2);
    }
    
    
}
