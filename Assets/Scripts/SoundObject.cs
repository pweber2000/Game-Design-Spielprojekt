using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] private AudioSource clip;
    private bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        clip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(clip != null && clip.clip != null && !clip.isPlaying && !played)
        {
            played = true;
            clip.Play();
        }

        if (!clip.isPlaying && played)
        {
            Debug.Log("SoundObject deleted");
            Destroy(gameObject);
        }
    }
}
