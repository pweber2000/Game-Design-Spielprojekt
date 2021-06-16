using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    public static DropOnDeath dropOnDeath;
    [SerializeField] private GameObject prefAmmo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        dropOnDeath = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop(GameObject pref, Transform transform)
    {
        if (pref != null)
            Instantiate(pref, transform.position, new Quaternion(0, 0, 0, 0));
    }

    public void Drop(GameObject pref, Transform transform, Quaternion rot)
    {
        if (pref != null)
            Instantiate(pref, transform.position, rot);
    }

    public void Drop(GameObject pref, Transform transform, Vector3 offset)
    {
        if (pref != null)
            Instantiate(pref, transform.position + offset, new Quaternion(0, 0, 0, 0));
    }

    public void Drop(GameObject pref, Transform transform, Quaternion rot, Vector3 offset)
    {
        if(pref != null)
            Instantiate(pref, transform.position + offset, rot);
    }

    public void DropAmmo(Transform transform, Quaternion rot, Vector3 offset)
    {
        if(prefAmmo != null)
            Instantiate(prefAmmo, transform.position + offset, rot);
    }
}

