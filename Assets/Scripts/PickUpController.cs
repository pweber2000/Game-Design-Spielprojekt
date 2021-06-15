using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private int keyID;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioClip collect_sound;
    
    public int getKeyId()
    {
        return keyID;
    }
    private void Update()
    {
        this.transform.Rotate(0,rotationSpeed * Time.deltaTime,0);
    }

    private void KeyPickup()
    {
        if(collect_sound != null)
            AudioSource.PlayClipAtPoint(collect_sound, Camera.main.transform.position, 8f);
        Player.player.addKey(keyID);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CompareTag("Key"))
        {
            Debug.Log("hit");
            KeyPickup();
        }

        else if(other.gameObject.CompareTag("Player") && CompareTag("Ammo"))
        {
            int[] amount = {30,60,90};

            int rand = UnityEngine.Random.Range(0, 3);
            Player.player.PickUpAmmo(amount[rand]);
        }
    }
}
