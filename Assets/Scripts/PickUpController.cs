using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private int keyID;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioSource pickUpSound;
    public int getKeyId()
    {
        return keyID;
    }

    private void Start()
    {
    }
    private void Update()
    {
        if (pickUpSound != null)
        {
            pickUpSound = GetComponent<AudioSource>();
        }
        this.transform.Rotate(0,rotationSpeed * Time.deltaTime,0);
    }

    private void KeyPickup()
    {
        
        Player.player.addKey(keyID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            if (CompareTag("Key"))
            {
                KeyPickup();
            }

            else if (CompareTag("Ammo"))
            {
                Player.player.PickUpAmmo(6);
            }
            
            else if (CompareTag("AmmoEnemy"))
            {
                Player.player.PickUpAmmo(20);
            }
            
            else if (CompareTag("AmmoBigEnemy"))
            {
                Player.player.PickUpAmmo(150);
            }

            else if (CompareTag("Respawn"))
            {
                
                Player.player.SetRespawn(transform);

            }

            if (pickUpSound != null)
            {
                SoundManager.soundManager.PlaySoundAt(pickUpSound, transform);
            }
            Destroy(this.gameObject);
        }
    }
}


