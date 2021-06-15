using System;
using System.Collections;
using UnityEngine;

public class GeneratorExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject fire;


    public void isHit()
    {
        if (!LevelFinish.canEscape) //damit man nur einmal darauf schießen kann
        {
            LevelFinish.canEscape = true;
            fire.SetActive(true);
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.2f);
        explosion.SetActive(true);
    }
}