using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorTriggerOben : MonoBehaviour
{
    private Animator DoorAnimator; //Immer der spezielle Animator pro Tür, weil sonst würden alle gleichzeitig aufgehen
    private static readonly int CloseDoor = Animator.StringToHash("closeDoor");
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");
    
    [SerializeField] private ElevatorTrigger elevatorTrigger;
    
    [SerializeField] private AudioClip openDoorWithoutKeySound;
    [SerializeField] private AudioClip closeDoorSound;
    [SerializeField] private AudioClip elevatorUntenSound;

    private void Start()
    {
        DoorAnimator = this.GetComponentInChildren<Animator>();
    }

    //Immmer wenn der Spieler in den Triggerbereich kommt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!elevatorTrigger.getUnten())
            {
                SoundManager.soundManager.PlayClipAt(openDoorWithoutKeySound, transform, SoundManager.MIXERGROUP.SFX);
                //AudioSource.PlayClipAtPoint(openDoorWithoutKeySound, Camera.main.transform.position, 3f);
                DoorAnimator.SetBool(CloseDoor, false);
                DoorAnimator.SetBool(OpenDoor, true);
            }
            else
            {
                //Wenn der Aufzug unten ist
                SoundManager.soundManager.PlayClipAt(elevatorUntenSound, transform, SoundManager.MIXERGROUP.SFX);
                //AudioSource.PlayClipAtPoint(elevatorUntenSound, Camera.main.transform.position, 3f);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!elevatorTrigger.getUnten())
            {
                SoundManager.soundManager.PlayClipAt(closeDoorSound, transform, SoundManager.MIXERGROUP.SFX);
                //AudioSource.PlayClipAtPoint(closeDoorSound, Camera.main.transform.position, 3f);
            }
            DoorAnimator.SetBool(OpenDoor, false);
            DoorAnimator.SetBool(CloseDoor, true);
        }
    }
}
