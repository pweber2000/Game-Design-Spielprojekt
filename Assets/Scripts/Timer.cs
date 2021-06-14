using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer_textField;
    private float startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString();
        string seconds = ((t % 60).ToString("f0"));

        timer_textField.text = minutes + ":" + seconds +" min";
    }
}
