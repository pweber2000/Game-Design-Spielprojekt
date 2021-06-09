using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    //Name der Waffe
    [SerializeField]
    private string name;

    //Schuesse im Magazin
    [SerializeField]
    private int bpm;

    //Schuesse pro Magazin
    [SerializeField]
    private int bpmmax;

    //Schuesse pro Sekunde
    [SerializeField]
    private float bps;

    //Schaden der Waffe
    [SerializeField]
    private int damage;

    [SerializeField]
    private Camera cam;

    //Kugelmodel
    [SerializeField]
    private GameObject prefShot;
    
    //Treffereffekt
    [SerializeField]
    private GameObject impactEffect;

    //Zeit des letzten Schusses
    private float timeLastShot;

    [SerializeField]
    private PlayerMovement playermovement;

    [SerializeField]
    private Text uitext;

    // Update is called once per frame
    void Update()
    {
        if(uitext != null)
        uitext.text = bpm.ToString() + "/" + Player.getAmmunition().ToString();

        if (Input.GetButton("Fire1"))
        {
            //Berechnung der Zeit bis der naechste Schuss getaetigt werden kann
            if (bpm > 0 && (Time.time - timeLastShot) > 1/bps)
            {
                Shoot();
                timeLastShot = Time.time;
                bpm--;
                
            }
        }

        //Waffe nachladen, volles Magazin wenn der Player genug Munition hat, ansonsten den Rest des Players
        if (Input.GetKeyDown("r"))
        {
            if (Player.getAmmunition() >= bpmmax)
            {
                bpm = bpmmax;
                Player.setAmmunition(Player.getAmmunition() - bpmmax);
            }

            else 
            {
                bpm = Player.getAmmunition();
                Player.setAmmunition(0);
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        //Ray in Richtung der Kamera abfeueren und ueberpruefen ob was getroffen wurde
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            Enemy en = hit.transform.GetComponent<Enemy>();

            if(en != null)
            {
                en.TakeDamage(damage);
            }

            BoxExplosion be = hit.transform.GetComponent<BoxExplosion>();

            if(be != null)
            {
                be.isHit();
            }
        }

        //Kugel und Staub erzeugen
        GameObject bullet = Instantiate(prefShot, this.transform.position, Quaternion.LookRotation(this.transform.forward));
        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(bullet, 0.5f);
        Destroy(impact, 0.3f);
        
        //Rueckstoss
        if(playermovement != null)
            playermovement.getRecoil(1);
    }
}
