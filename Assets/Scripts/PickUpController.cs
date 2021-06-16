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

            else if (other.gameObject.CompareTag("Player") && CompareTag("Ammo"))
            {
                int[] amount = { 30, 60, 90 };

                int rand = UnityEngine.Random.Range(0, 3);
                Player.player.PickUpAmmo(amount[rand]);
            }

            if (pickUpSound != null)
                SoundManager.soundManager.PlaySound(pickUpSound);
            Destroy(this.gameObject);
        }
    }
}
