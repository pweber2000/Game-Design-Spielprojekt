using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorTriggerUnten : MonoBehaviour
{
    private Animator DoorAnimator; //Immer der spezielle Animator pro Tür, weil sonst würden alle gleichzeitig aufgehen
    private static readonly int CloseDoor = Animator.StringToHash("closeDoor");
    private static readonly int OpenDoor = Animator.StringToHash("openDoor");
    [SerializeField] private int KeyNumber;

    [SerializeField] private ElevatorTrigger elevatorTrigger;
    
    [SerializeField] private AudioClip openDoorWithKeySound;
    [SerializeField] private AudioClip openDoorWithoutKeySound;
    [SerializeField] private AudioClip closeDoorSound;
    [SerializeField] private AudioClip elevatorObenSound;
    [SerializeField] private AudioClip NoKeySound;

    private bool opened = false;
    private static readonly int GotKey = Animator.StringToHash("gotKey");
    private static readonly int NoKey = Animator.StringToHash("NoKey");

    private void Start()
    {
        DoorAnimator = this.GetComponentInChildren<Animator>();
    }

    //Immmer wenn der Spieler in den Triggerbereich kommt
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player") && Player.hasKey(KeyNumber))
        if(other.CompareTag("Player") && true)
        {
            if (elevatorTrigger.getUnten())
            {
                DoorAnimator.SetBool(CloseDoor, false);
                DoorAnimator.SetBool(OpenDoor, true);
                
                if (!opened) //beim ersten mal wo man den Schlüssel braucht
                {
                    DoorAnimator.SetBool(GotKey, true);
                    opened = true;
                    AudioSource.PlayClipAtPoint(openDoorWithKeySound, Camera.main.transform.position, 3f);
                }
                else //nachdem Tür geöffnet wurde
                {
                    AudioSource.PlayClipAtPoint(openDoorWithoutKeySound, Camera.main.transform.position, 3f);
                }
            }
            else
            {
                //Wenn der Aufzug oben ist
                AudioSource.PlayClipAtPoint(elevatorObenSound, Camera.main.transform.position, 3f);
            }
        }
        else if (false) //if (other.CompareTag("Player") && !Player.hasKey(KeyNumber))
        {
            DoorAnimator.SetBool(NoKey, true);
            AudioSource.PlayClipAtPoint(NoKeySound, Camera.main.transform.position, 3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DoorAnimator.SetBool(NoKey, false);
        
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(closeDoorSound, Camera.main.transform.position, 3f);
            DoorAnimator.SetBool(OpenDoor, false);
            DoorAnimator.SetBool(CloseDoor, true);
        }
    }
}
