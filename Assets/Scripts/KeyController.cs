using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class KeyController : MonoBehaviour
{
    [SerializeField]
    private static int keyID = 0;

    [SerializeField] private float rotateSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        keyID++;
    }

    private void KeyPickup()
    {
        Player.addKey(keyID);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KeyPickup();

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotateSpeed,0);
    }
}
