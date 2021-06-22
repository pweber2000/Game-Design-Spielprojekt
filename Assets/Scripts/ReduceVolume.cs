using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReduceVolume : MonoBehaviour
{
    [SerializeField] private SoundManager.MIXERGROUP mixer;
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(slider != null)
        {
            SoundManager.soundManager.setVolume(mixer, slider.value);
        }
    }
}
