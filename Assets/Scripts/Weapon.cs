using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    //Name der Waffe
    [SerializeField]
    private string name;
    
    [SerializeField]
    private Transform LaunchPosition;

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
    private TextMeshProUGUI uitext;

    [SerializeField]
    private AudioSource soundReloading;

    [SerializeField]
    private AudioSource outOfAmmo_sound;

    [SerializeField]
    private AudioSource shotSound;

    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject crosshair_red;
    

    //Muzzleflash
    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private ParticleSystem reloadCharge;
    [SerializeField]
    private ParticleSystem reloadFlash;
    private Animator anim;
    private bool autoFire;

    private void Start()
    {
        soundReloading = GetComponent<AudioSource>();
        if (cam != null)
            fov = cam.fieldOfView;
        anim = GetComponent<Animator>();
    }

    private bool test = false;

    // Update is called once per frame
    void Update()
    {
        if(uitext != null)
        uitext.text = bpm.ToString() + "/" + Player.player.getAmmunition().ToString();
        if (!Player.player.IsAlive())
            autoFire = false;

        if (!PauseMenu.isPaused && !isReloading)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                autoFire = true;
            }

            if (autoFire)
            {
                //Berechnung der Zeit bis der naechste Schuss getaetigt werden kann
                if (bpm > 0 && (Time.time - timeLastShot) > 1 / bps)
                {
                    if (test)
                    {
                        Debug.Log("SFX: "+PlayerPrefs.GetFloat("SFXVolume"));
                        test = false;
                    } 
                    else
                    {
                        Debug.Log("Music: "+PlayerPrefs.GetFloat("MusicVolume"));
                        test = true;
                    }

                    {
                        
                    }
                    Shoot();
                    timeLastShot = Time.time;
                    bpm--;
                }
                else if (bpm <= 0 && (Time.time - timeLastShot) > 1 / bps)
                {
                    crosshair.SetActive(false);
                    crosshair_red.SetActive(true);
                    outOfAmmo_sound.Play();
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                autoFire = false;
                timeLastShot = 0;
            }


            //if (Input.GetButton("Fire2"))
            //{
            //    isZooming = true;

            //    if (cam != null)
            //    {
            //        if(cam.fieldOfView > (fov - zoom))
            //            cam.fieldOfView -= 1f;
            //        Debug.Log("ZOOOOOM");
            //    }
            //}

            //if (Input.GetButtonUp("Fire2"))
            //{
            //    isZooming = false;
            //}

            //if(!isZooming && cam.fieldOfView != fov)
            //{
            //    cam.fieldOfView += 2;
            //    if (cam.fieldOfView > fov)
            //        cam.fieldOfView = fov;
            //}

            //Waffe nachladen, volles Magazin wenn der Player genug Munition hat, ansonsten den Rest des Players
            if ((Input.GetButtonDown("Fire2") || Input.GetKeyDown("r")) 
                && bpm != bpmmax && Player.player.getAmmunition() > 0)
            {
                if(reloadCharge != null)
                    reloadCharge.Play();
                isReloading = true;
                timeReloaded = Time.time;
                if (soundReloading != null)
                    soundReloading.Play();
                if(anim != null)
                {
                    anim.SetBool("Reloading", true);
                    //Debug.Log("hier");
                }
                crosshair.SetActive(true);
                crosshair_red.SetActive(false);
            }
        }
        
        else if((Time.time - timeReloaded) > timeReloading && isReloading)
        {
            if (Player.player.getAmmunition() >= bpmmax)
            {
                Player.player.setAmmunition(Player.player.getAmmunition() + bpm - bpmmax);
                bpm = bpmmax;
            }

            else
            {
                bpm = Player.player.getAmmunition();
                Player.player.setAmmunition(0);
            }

            isReloading = false;
        }
        
        else if(anim != null && (Time.time - timeReloaded) > timeReloading * 0.9f && isReloading)
        {
            anim.SetBool("Reloading", false);
            if(reloadFlash != null)
                reloadFlash.Play();
        }
        
    }

    void Shoot()
    {
        /*
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
    */
        
        //Kugel und Staub erzeugen
        GameObject bullet = Instantiate(prefShot, LaunchPosition.position, Quaternion.LookRotation(this.transform.forward));
        /*GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(bullet, 0.5f);
        Destroy(impact, 0.3f);*/

        //Rueckstoss
        if (playermovement != null)
            playermovement.getRecoil(1);

        muzzleFlash.Play();
        if(shotSound != null)
            shotSound.PlayOneShot(shotSound.clip);
    }
}
