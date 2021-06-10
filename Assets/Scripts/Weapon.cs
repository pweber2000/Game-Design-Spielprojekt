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
    private float fov;
    [SerializeField]
    private float zoom = 40f;
    private bool isZooming = false;

    //Kugelmodel
    [SerializeField]
    private GameObject prefShot;
    
    //Treffereffekt
    [SerializeField]
    private GameObject impactEffect;

    //Zeit des letzten Schusses
    private float timeLastShot;

    //Nachladezeit der Waffe
    [SerializeField]
    private float timeReloading = 1f;

    private bool isReloading = false;
    private float timeReloaded;

    [SerializeField]
    private PlayerMovement playermovement;

    [SerializeField]
    private Text uitext;

    [SerializeField]
    private AudioSource soundReloading;

    //Muzzleflash
    [SerializeField]
    private ParticleSystem muzzleFlash;

    private void Start()
    {
        soundReloading = GetComponent<AudioSource>();
        if (cam != null)
            fov = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if(uitext != null)
        uitext.text = bpm.ToString() + "/" + Player.getAmmunition().ToString();
        

        if(!isReloading)
        {
            if (Input.GetButton("Fire1"))
            {
                //Berechnung der Zeit bis der naechste Schuss getaetigt werden kann
                if (bpm > 0 && (Time.time - timeLastShot) > 1 / bps)
                {
                    Shoot();
                    timeLastShot = Time.time;
                    bpm--;


                }
            }

            if (Input.GetButton("Fire2"))
            {
                isZooming = true;

                if (cam != null)
                {
                    if(cam.fieldOfView > (fov - zoom))
                        cam.fieldOfView -= 1f;
                    Debug.Log("ZOOOOOM");
                }
            }

            if (Input.GetButtonUp("Fire2"))
            {
                isZooming = false;
            }

            if(!isZooming && cam.fieldOfView != fov)
            {
                cam.fieldOfView += 2;
                if (cam.fieldOfView > fov)
                    cam.fieldOfView = fov;
            }

            //Waffe nachladen, volles Magazin wenn der Player genug Munition hat, ansonsten den Rest des Players
            if (Input.GetKeyDown("r") && bpm != bpmmax && Player.getAmmunition() > 0)
            {
                isReloading = true;
                timeReloaded = Time.time;
                if(soundReloading != null)
                    soundReloading.Play();
            }
        }
        
        else if((Time.time - timeReloaded) > timeReloading && isReloading)
        {
            if (Player.getAmmunition() >= bpmmax)
            {
                Player.setAmmunition(Player.getAmmunition() + bpm - bpmmax);
                bpm = bpmmax;
            }

            else
            {
                bpm = Player.getAmmunition();
                Player.setAmmunition(0);
            }

            isReloading = false;
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

            if (en != null)
            {
                en.TakeDamage(damage);
            }

            BoxExplosion be = hit.transform.GetComponent<BoxExplosion>();

            if (be != null)
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
        if (playermovement != null)
            playermovement.getRecoil(1);

        muzzleFlash.Play();
    }
}
