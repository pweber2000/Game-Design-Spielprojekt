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

    private void Start()
    {
        DoorAnimator = this.GetComponentInChildren<Animator>();
    }

    //Immmer wenn der Spieler in den Triggerbereich kommt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !elevatorTrigger.getUnten())
        {
            DoorAnimator.SetBool(CloseDoor, false);
            DoorAnimator.SetBool(OpenDoor, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //DoorAnimator.SetBool("NoKey", false); //if !Key
        
        if (other.CompareTag("Player"))
        {
            DoorAnimator.SetBool(OpenDoor, false);
            DoorAnimator.SetBool(CloseDoor, true);
        }
    }
}
