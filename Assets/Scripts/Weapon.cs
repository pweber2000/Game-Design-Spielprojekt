using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int bpm;
    [SerializeField]
    private int bpmmax;
    [SerializeField]
    private float bps;
    [SerializeField]
    private int damage;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject prefShot;
    [SerializeField]
    private GameObject impactEffect;

    private float timeLastShot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (bpm > 0 && (Time.time - timeLastShot) > 1/bps)
            {
                Shoot();
                timeLastShot = Time.time;
                
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            Enemy en = hit.transform.GetComponent<Enemy>();

            if(en != null)
            {
                en.TakeDamage(damage);
            }
        }

        GameObject bullet = Instantiate(prefShot, this.transform.position, Quaternion.LookRotation(this.transform.forward));
        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(bullet, 0.5f);
        Destroy(impact, 0.3f);
    }
}
