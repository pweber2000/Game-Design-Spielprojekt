using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUIElement : MonoBehaviour
{
    [SerializeField] private GameObject hint_text;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hint_text.SetActive(true);
        }
    }
}
