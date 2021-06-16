using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager = null;

    private AudioSource singleClip;
    private bool played;

    private SoundManager()
    {

    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (soundManager == null)
            soundManager = this;
        else
            Debug.Log("Audiomanager konnte nicht geladen werden");
    }

    // Update is called once per frame
    void Update()
    {
        if(singleClip != null && played && !singleClip.isPlaying)
        {
            Debug.Log("Audio wird geloescht");
            Destroy(singleClip);
            singleClip = null;
            played = false;
        }
        if (singleClip != null && !singleClip.isPlaying && !played)
        {
            singleClip.volume = 0.5f;
            Debug.Log("Audio wird abgespielt");
            singleClip.Play();
            played = true;
        }    
        
    }


   public void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            singleClip = gameObject.AddComponent<AudioSource>();
            singleClip.clip = audioSource.clip;
        }
        else
            Debug.Log("Audio konnte nicht in den Audiomanager geladen werden");
    }
}
