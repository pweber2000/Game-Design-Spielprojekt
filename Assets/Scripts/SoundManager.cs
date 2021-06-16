using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager = null;

    private List <AudioSource> singleClips;
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

        singleClips = new List<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (singleClips.Count > 0)
        {
            for (int i = 0; i < singleClips.Count; i++)
            {
                if (!singleClips[i].isPlaying)
                {
                    Debug.Log("Audio wird geloescht");
                    Destroy(singleClips[singleClips.Count - 1]);
                    singleClips.RemoveAt(singleClips.Count - 1);
                }
            }
        }
        //if (singleClip != null && !singleClip.isPlaying && !played)
        //{
        //    singleClip.volume = 0.5f;
        //    Debug.Log("Audio wird abgespielt");
        //    singleClip.Play();
        //    played = true;
        //}    

    }


    public IEnumerator PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            singleClips.Add(gameObject.AddComponent<AudioSource>());
            Debug.Log(singleClips.Count);
            singleClips[singleClips.Count - 1].clip = audioSource.clip;
        }
        else
            Debug.Log("Audio konnte nicht in den Audiomanager geladen werden");

        if(singleClips != null)
        {
            float temp = Time.deltaTime;
            singleClips[singleClips.Count - 1].Play();
            while (singleClips[singleClips.Count - 1].isPlaying)
                {
                    yield return null;
                }
        }
    }
}
