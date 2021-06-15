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
            float t = Time.time - startTime;
            float minutes = t / 60;
            //float seconds = t % 60;

            //AudioSource.PlayClipAtPoint(finishSound, Camera.main.transform.position, 1f);
            PauseMenu.isPaused = true;
            
            if (minutes < 2)
            {
                SceneManager.LoadScene(3);
            }
            else if (minutes < 4)
            {
                SceneManager.LoadScene(4);
            }
            else
            {
                SceneManager.LoadScene(5);
            }
          
        }
        else
        {
            HintText.text =
                "Zerstöre zuerst die Generatoren im Raum unter dem Helilandeplatz, um die Luftabwehrgeschütze auszuschalten.";
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(6f);
        HintText.text = "";
    }
}
