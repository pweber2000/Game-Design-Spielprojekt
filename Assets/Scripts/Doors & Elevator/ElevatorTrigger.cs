using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private Animator ElevatorAnimator; //Immer der spezielle Animator pro Tür, weil sonst würden alle gleichzeitig aufgehen
    private static readonly int UpPressed = Animator.StringToHash("UpPressed");
    private static readonly int DownPressed = Animator.StringToHash("DownPressed");
    private bool unten = true;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Elevator;
    [SerializeField] private GameObject Pointer;
    [SerializeField] private GameObject Munition;
    [SerializeField] private AudioSource elevator_music;
    private PlayerMovement playerMovement;

    public static bool isMoving = false;


    private void Start()
    {
        ElevatorAnimator = this.GetComponentInParent<Animator>();
        playerMovement = Player.GetComponent<PlayerMovement>();
    }

    //Immmer wenn der Spieler in den Triggerbereich kommt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.parent = Elevator.transform; //Player als Child von Elevator machen, damit es beim hochfahren nicht ruckelt
            
            if (unten)
            {
                ElevatorAnimator.SetBool(UpPressed, true);
                ElevatorAnimator.SetBool(DownPressed, false);
            }
            else
            {
                ElevatorAnimator.SetBool(UpPressed, false);
                ElevatorAnimator.SetBool(DownPressed, true);
            }
            
            isMoving = true;
            unten = !unten;
            StartCoroutine(CanvasState());
        }
    }

    IEnumerator CanvasState()
    {
        yield return new WaitForSeconds(1.5f);
        SoundManager.soundManager.PlaySound(elevator_music);
        Pointer.SetActive(false);
        Munition.SetActive(false);
        playerMovement.enabled = false;
        yield return new WaitForSeconds(6.0f);
        isMoving = false;
        Pointer.SetActive(true);
        Munition.SetActive(true);
        SoundManager.soundManager.StopSound(elevator_music);
        playerMovement.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.parent = null; //Player wieder ohne Parent machen
        }
    }

    public bool getUnten()
    {
        return unten;
    }
}
