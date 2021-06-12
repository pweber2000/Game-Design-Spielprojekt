using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyTrigger : MonoBehaviour
{
    private Animator DoorAnimator; //Immer der spezielle Animator pro Tür, weil sonst würden alle gleichzeitig aufgehen
    private static readonly int CloseDoor = Animator.StringToHash("closeDoor");
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");
    [SerializeField] private int KeyNumber;

    private void Start()
    {
        DoorAnimator = this.GetComponentInChildren<Animator>();
    }

    //Immmer wenn der Spieler in den Triggerbereich kommt
    private void OnTriggerEnter(Collider other)
    {
        //if (Player.hasKey(KeyNumber))
        if(true)
        {
            DoorAnimator.SetBool("gotKey", true);
        }
        else
        {
            DoorAnimator.SetBool("NoKey", true);
        }
        
        
        if (other.CompareTag("Player"))
        {
            DoorAnimator.SetBool(CloseDoor, false);
            DoorAnimator.SetBool(OpenDoor, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DoorAnimator.SetBool("NoKey", false);
        
        if (other.CompareTag("Player"))
        {
            DoorAnimator.SetBool(OpenDoor, false);
            DoorAnimator.SetBool(CloseDoor, true);
        }
    }
}
