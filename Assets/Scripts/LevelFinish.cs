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

            AudioSource.PlayClipAtPoint(finishSound, Camera.main.transform.position, 1f);
            
            PauseMenu.isPaused = true;
            StartCoroutine(Waiting2());
            
          
        }
        else if(other.CompareTag("Player"))
        {
            HintText.text =
                "Zerstöre zuerst die Generatoren im Raum unter dem Helilandeplatz, um die Luftabwehrgeschütze auszuschalten.";
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(8f);
        HintText.text = "";
    }

    IEnumerator Waiting2()
    {
       yield return new WaitForSeconds(5f);
       float t = Time.time - startTime;
       float minutes = t / 60;
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
            
       if (minutes < 3)
       {
                
           SceneManager.LoadScene(3);
       }
       else if (minutes < 5)
       {
           SceneManager.LoadScene(4);
       }
       else
       {
           SceneManager.LoadScene(5);
       }
    }
}
