using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GeneratorExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject fire;
    [SerializeField] private TextMeshProUGUI HintText;


    public void isHit()
    {
        if (!LevelFinish.canEscape) //damit man nur einmal darauf schießen kann
        {
            LevelFinish.canEscape = true;
            fire.SetActive(true);
            StartCoroutine(Explode());
            HintText.text = "Got it! Quick, escape with the helicopter!";

        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.1f);
        explosion.SetActive(true);
    }
}