using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    
    // PlayerPrefs.GetFloat("MusicVolume") <-- gibt Wert zwischen 0 und 1 zurück (Hintergrundmusik-Lautstärke)
    // PlayerPrefs.GetFloat("SFXVolume")   <-- gibt Wert zwischen 0 und 1 zurück (Sound Effects-Lautstärke)
    
    public static SoundManager soundManager = null;
    [SerializeField] private AudioMixer mixerMaster;
    public AudioMixerGroup mixerSFX;
    public AudioMixerGroup mixerMusic;
    [SerializeField] private AudioSource backgroundMusic;
    
    public enum MIXERGROUP {MUSIC = 0, SFX = 1, MASTER = 2};

    [SerializeField] private GameObject soundObject;

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

        backgroundMusic = GetComponent<AudioSource>();

    }

    private void Start()
    {
        float music = PlayerPrefs.GetFloat("volumeMusic");
        setVolume(MIXERGROUP.MUSIC, music);
        float sfx = PlayerPrefs.GetFloat("volumeSFX");
        setVolume(MIXERGROUP.SFX, sfx);
        float master = PlayerPrefs.GetFloat("volumeMaster");
        setVolume(MIXERGROUP.MASTER, master);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySoundAt(AudioSource audioSource, Transform targetTransform = null, MIXERGROUP mixerGroup = MIXERGROUP.SFX)
    {

        if(audioSource != null)
        {
            AudioMixerGroup setMixer = null;

            if (mixerGroup == MIXERGROUP.SFX && mixerSFX != null)
                setMixer = mixerSFX;
            
            if (mixerGroup == MIXERGROUP.MUSIC && mixerMusic != null)
                setMixer = mixerMusic;

            if (targetTransform != null)
            {
                GameObject instance = Instantiate(soundObject, targetTransform.position, targetTransform.rotation);
                AudioSource instanceAudio = instance.GetComponent<AudioSource>();

                if(instanceAudio != null)
                {
                    instanceAudio.clip = audioSource.clip;
                    instanceAudio.volume = audioSource.volume;
                    
                    if(setMixer != null)
                        instanceAudio.outputAudioMixerGroup = setMixer;
                }
            }
            else
            {
                GameObject instance = Instantiate(soundObject, Player.player.transform.position, Player.player.transform.rotation);
                AudioSource instanceAudio = instance.GetComponent<AudioSource>();

                if (instanceAudio != null)
                {
                    instanceAudio.clip = audioSource.clip;
                    instanceAudio.volume = audioSource.volume;

                    if (setMixer != null)
                        instanceAudio.outputAudioMixerGroup = setMixer;
                }
            }
        }
    }

    public void PlayClipAt(AudioClip clip, Transform targetTransform = null, MIXERGROUP mixerGroup = MIXERGROUP.SFX)
    {
        if (clip != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            PlaySoundAt(audioSource, targetTransform, mixerGroup);
            Destroy(audioSource);
        }
    }
    
    // PlayerPrefs.GetFloat("MusicVolume") <-- gibt Float-Wert zwischen 0 und 1 zurück (Hintergrundmusik-Lautstärke)
    // PlayerPrefs.GetFloat("SFXVolume")   <-- gibt Float-Wert zwischen 0 und 1 zurück (Sound Effects-Lautstärke)

    public void setVolume(MIXERGROUP mixer, float volume)
    {
        if (volume > 0 && volume <= 1)
        {
            if (mixer == MIXERGROUP.SFX)
            {
                mixerMaster.SetFloat("volumeSFX", Mathf.Log(volume) * 20);
                PlayerPrefs.SetFloat("volumeSFX", volume);
            }
            if (mixer == MIXERGROUP.MUSIC)
            {
                mixerMaster.SetFloat("volumeMusic", Mathf.Log(volume) * 20);
                PlayerPrefs.SetFloat("volumeMusic", volume);
            }
            if (mixer == MIXERGROUP.MASTER)
            {
                mixerMaster.SetFloat("volumeMaster", Mathf.Log(volume) * 20);
                PlayerPrefs.SetFloat("volumeMaster", volume);
            }
        }
    }

    public void disableBackGroundMusic()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying)
            backgroundMusic.Stop();
    }

    public void enableBackGroundMusic()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
            backgroundMusic.Play();
    }
}
