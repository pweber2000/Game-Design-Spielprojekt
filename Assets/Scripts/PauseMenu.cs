using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject ammu_display;
    [SerializeField] private GameObject ammu_icon;

    public static Boolean isPaused = false;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                pauseGame();
            }
            else
            {
                resumeGame();
            }
        }
    }

    public void pauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshair.SetActive(false);
        ammu_display.SetActive(false);
        ammu_icon.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (!ElevatorTrigger.isMoving)
        {
            crosshair.SetActive(true);
            ammu_display.SetActive(true);
            ammu_icon.SetActive(true);
        }
    }

    public void restartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        isPaused = false;
    }
    public void goToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
