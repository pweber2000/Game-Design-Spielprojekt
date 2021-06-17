using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator DoorAnimator; //Immer der spezielle Animator pro Tür, weil sonst würden alle gleichzeitig aufgehen
    private static readonly int CloseDoor = Animator.StringToHash("closeDoor");
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");
    
    [SerializeField] private AudioClip openDoorWithoutKeySound;
    [SerializeField] private AudioClip closeDoorSound;

    private void Start()
    {
        DoorAnimator = this.GetComponentInChildren<Animator>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    //Immmer wenn der Spieler in den Triggerbereich kommt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(openDoorWithoutKeySound, Camera.main.transform.position, 3f);
            DoorAnimator.SetBool(CloseDoor, false);
            DoorAnimator.SetBool(OpenDoor, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //DoorAnimator.SetBool("NoKey", false); //if !Key
        
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(closeDoorSound, Camera.main.transform.position, 3f);
            DoorAnimator.SetBool(OpenDoor, false);
            DoorAnimator.SetBool(CloseDoor, true);
        }
    }
}
