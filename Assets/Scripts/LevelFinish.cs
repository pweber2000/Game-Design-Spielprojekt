using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private AudioClip finishSound;

    [SerializeField] private TextMeshProUGUI HintText;

    [SerializeField] private GameObject blackscreen;
    private float startTime;
    public static bool canEscape = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canEscape)
        {
            //float seconds = t % 60;

            SoundManager.soundManager.PlayClipAt(finishSound);
            //AudioSource.PlayClipAtPoint(finishSound, Camera.main.transform.position, 1f);
            
            PauseMenu.isPaused = true;
            blackscreen.SetActive(true);
            StartCoroutine(Waiting2());
            
          
        }
        else if(other.CompareTag("Player"))
        {
            HintText.text =
                "First destroy the generators behind the door with the red lock to disable the air defense.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HintText.text = "";
        }
    }

    IEnumerator Waiting2()
    {
       yield return new WaitForSeconds(3f);
       float t = Time.time - startTime;
       float minutes = t / 60;
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
            
       if (minutes < 4)
       {
                
           SceneManager.LoadScene(3);
       }
       else if (minutes < 7)
       {
           SceneManager.LoadScene(4);
       }
       else
       {
           SceneManager.LoadScene(5);
       }
    }
}
