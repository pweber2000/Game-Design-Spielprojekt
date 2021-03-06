using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    [SerializeField]
    private float health_max = 220f;
    private float health = 130f;
    
    [SerializeField]
    private  float stamina = 100f;

    private Weapon weapon;
    private int ammunition = 90;

    private bool[] keys;
    private int numberOfKeys = 0;

    [SerializeField]
    private PostProcessVolume volume;

    [SerializeField]
    private float regTimer = 3;
    private float regenerateTimer;
    [SerializeField] public GameObject blackscreen;
    [SerializeField] private GameObject die_text;
    [SerializeField] private AudioSource die_sound;

    private Vignette vign;
    private ColorGrading grading;
    private AudioSource heartbeat;

    private CharacterController charControl;

    private float[] pos;
    private Quaternion rot;
    private void Awake()
    { 
        if(player == null)
            player = this;

        charControl = GetComponent<CharacterController>();
        
        pos = new float[3];
        pos[0] = this.transform.position.x;
        pos[1] = this.transform.position.y;
        pos[2] = this.transform.position.z;
        rot = this.transform.rotation;
    }


    bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        health = health_max;
        keys = new bool[] {false, false, false, false}; //schwarz, rot, blau, grün
        stamina = 100f;

        volume.profile.TryGetSettings(out vign);
        volume.profile.TryGetSettings(out grading);
        heartbeat = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && health <= 0)
        {
            isAlive = false;
             Die();
        }

        regenerateTimer += Time.deltaTime;
        if(regenerateTimer > regTimer && health < health_max)
        {
            if(heartbeat != null)
            {
                //heartbeat.Stop();
                if (heartbeat.isPlaying && (health / health_max) > 0.5f)
                {
                    Debug.Log("Stoppe nun");
                    heartbeat.Pause();
                }
            }

            health += 10 * Time.deltaTime;
            if (volume != null)
            {
                vign.intensity.value = 1.1f - health / health_max;
                if (grading.saturation.value < 0)
                    grading.saturation.value = (1 - (health / (health_max / 2))) * -100;
                else if (grading.saturation.value > 0)
                    grading.saturation.value = 0;
            }
        }
    }

    public int getAmmunition()
    {
        return ammunition;
    }

    public void setAmmunition(int number)
    {
        ammunition = number;
    }

    public float getHealth()
    {
        return health;
    }
    public float getHealthMax()
    {
        return health_max;
    }

    public void setHealth(float number)
    {
        health = number;
    }

    public float getStamina()
    {
        return stamina;
    }

    public void setStamina(float number)
    {
        if (number > 100)
            stamina = 100f;
        
        else 
            stamina = number;
    }

    public void addKey(int keyID)
    {
        if (keyID > 0 && keyID < 5)
        {
            switch (keyID)
            {
                case 1:
                    keys[0] = true;
                    break;
                case 2:
                    keys[1] = true;
                    break;
                case 3:
                    keys[2] = true;
                    break;
                case 4:
                    keys[3] = true;
                    break;
            }
        }
    }

    public bool hasKey(int keyID)
    {
        if (keyID > 0 && keyID < 5)
        {
            return keys[keyID - 1];
        }
        else
        {
            return false;
        }
    }

    public void PickUpAmmo(int amount)
    {
        ammunition += amount;
    }

    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            health -= damage;
            regenerateTimer = 0f;
            if (!Cam.instance.IsShaking())
                Cam.instance.Shake(0.1f, 0.1f);

            if (volume != null)
            {
                float ratio = health / health_max;
                vign.intensity.value = 1.1f - ratio;

                if (ratio < 0.5f)
                {
                    grading.saturation.value = (1 - (health / 50)) * (-100);

                    if (heartbeat != null && !heartbeat.isPlaying)
                    {
                        heartbeat.Play();
                        //heartbeat.Play();
                    }
                }
            }
        }
    }

    public void Die()
    {


        blackscreen.SetActive(true);
        die_text.SetActive(true);
        PauseMenu.isPaused = true;
        health = health_max;
        charControl.enabled = false;
        this.transform.position = new Vector3(pos[0], pos[1], pos[2]);
        this.transform.rotation = Quaternion.LookRotation(rot.eulerAngles);
        charControl.enabled = true;
        
        if (volume != null) 
        {
            vign.intensity.value = 1 - health / health_max;
            grading.saturation.value = 0;
        }

        if (heartbeat != null)
        {
            if (heartbeat.isPlaying)
            {
                heartbeat.Pause();
            }
            //heartbeat.Stop();
        }

        die_sound.Play();
        StartCoroutine(waiting());

    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(3f);
        blackscreen.SetActive(false);
        die_text.SetActive(false);
        PauseMenu.isPaused = false;

        isAlive = true;
        
    }

    public void SetRespawn(Transform respawn)
    {
        pos[0] = respawn.position.x;
        pos[1] = respawn.position.y;
        pos[2] = respawn.position.z;

        rot = respawn.rotation;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
