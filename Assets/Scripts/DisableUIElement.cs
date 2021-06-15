using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUIElement : MonoBehaviour
{
    [SerializeField] private GameObject hint_text;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hint_text.SetActive(false);
        }
    }
}
