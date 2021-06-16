using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    public static DropOnDeath dropOnDeath;
    [SerializeField] private GameObject prefAmmo1;
    [SerializeField] private GameObject prefAmmo2;
    [SerializeField] private GameObject prefAmmo3;
    

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

    public void DropAmmo1(Transform transform, Quaternion rot, Vector3 offset)
    {
        if(prefAmmo1 != null)
            Instantiate(prefAmmo1, transform.position + offset, rot);
    }
    
    public void DropAmmo2(Transform transform, Quaternion rot, Vector3 offset)
    {
        if(prefAmmo2 != null)
            Instantiate(prefAmmo2, transform.position + offset, rot);
    }
    
    public void DropAmmo3(Transform transform, Quaternion rot, Vector3 offset)
    {
        if(prefAmmo3 != null)
            Instantiate(prefAmmo3, transform.position + offset, rot);
    }
}

