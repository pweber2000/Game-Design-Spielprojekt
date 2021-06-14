using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private int keyID;

    [SerializeField] private float rotationSpeed;
    //[SerializeField] private AudioClip collect_sound;

    private void Update()
    {
        this.transform.Rotate(0,rotationSpeed * Time.deltaTime,0);
    }

    private void KeyPickup()
    {
        //AudioSource.PlayClipAtPoint(collect_sound, Camera.main.transform.position, 8f);
        Player.player.addKey(keyID);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KeyPickup();
        }
    }
}
