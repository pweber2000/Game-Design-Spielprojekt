using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    private Animator anim;

    [SerializeField]
    private bool isActive = true;
    public float health = 100f;
    [SerializeField]
    private float rangeTrigger = 25f;
    [SerializeField]
    private float rangeAttack = 50f;
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private float turnSpeedMax = 5f;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private GameObject[] eyes;
    [SerializeField]
    private GameObject prefShot;
    [SerializeField]
    private float bps = 2f;
    private float lastShot = 0f;
    [SerializeField] private AudioSource transform_sound;
    [SerializeField] private AudioSource shotSound;
    [SerializeField] bool hasDrop = false;
    [SerializeField] bool bigEnemy = false;

    private bool canShoot = false;
    Vector3 direction;
    private float combatTimer = 0f;

    [SerializeField] bool hasSound = true;

    [SerializeField] float explosionRadius = 5;

    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        healthSlider.value = health;
    }


    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim != null && anim.GetBool("Open_Anim"))
            anim.SetBool("Open_Anim", false);

        lastShot = 0f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Kill();
        }
    }

    void Kill()
    {
        if (explosion != null)
        {
            GameObject explo = Instantiate(explosion, transform.position, transform.rotation);
            if(explosionRadius > Vector3.Distance(Player.player.transform.position, this.transform.position))
            {
                if(!Cam.instance.IsShaking())
                    Cam.instance.Shake(.20f, .3f);
                Player.player.TakeDamage(Player.player.getHealthMax() * 0.3f);
            }
        }

        if (hasDrop)
            if (!bigEnemy)
            {
                DropOnDeath.dropOnDeath.DropAmmo2(transform, new Quaternion(0,0,0,0), new Vector3(0,0.5f,0));
            }
            else
            {
                DropOnDeath.dropOnDeath.DropAmmo3(transform, new Quaternion(0,0,0,0), new Vector3(0,0.5f,0));
            }
        
        Destroy(gameObject);
    }

    private void Update()
    {
        if (PauseMenu.isPaused == false && isActive)
        {
            healthSlider.value = health;
            direction = this.transform.position - Player.player.transform.position;
            Quaternion rot = Quaternion.LookRotation(-direction);

            float distance = Vector3.Distance(Player.player.transform.position, this.transform.position);
            if (isTurning())
            {

            }

            else
            {
                anim.SetBool("Walk_Anim", false);
            }

            if (distance < rangeTrigger || PlayerInSight())
            {
                open();
                if (isTurning())
                {

                }

                else
                {
                    anim.SetBool("Walk_Anim", false);
                }

                if (distance < rangeAttack && canShoot && PlayerInSight())
                {
                    Fire();
                    combatTimer = 0f;
                }

        }

        //else if(combatTimer > 5f)
        //{
        //    close();
        //}
        }
    }

    private void open()
    {
        if (anim != null && !anim.GetBool("Open_Anim") )
        {
            if(transform_sound != null && hasSound)
                transform_sound.Play();
            anim.SetBool("Open_Anim", true);
            canShoot = true;
            lastShot = -0.5f;
        }

    }


    private void close()
    {
        if (anim != null)
        {
            anim.SetBool("Walk_Anim", false);
            anim.SetBool("Roll_Anim", false);
            anim.SetBool("Open_Anim", false);
            canShoot = false;
        }
    }

    private bool isTurning()
    {
        anim.SetBool("Roll_Anim", false);

        Vector3 direction = Player.player.transform.position - this.transform.position;
        float degree = Vector3.Angle(direction, transform.forward);
        if (degree >= 0)
        {
            if (degree > 30)
            {
                canShoot = false;
                if(anim.GetBool("Open_Anim"))
                    anim.SetBool("Walk_Anim", true);
                Quaternion rot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeedMax * Time.deltaTime);

            }

            else
            {
                canShoot = true;
                anim.SetBool("Walk_Anim", false);
                Quaternion rot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
            }

            
            return true;
        }


        else
            return false;
    }

    private void Fire()
    {
        Vector3 directionOne = Player.player.transform.position - eyes[0].transform.position;
        Vector3 directionTwo = Player.player.transform.position - eyes[1].transform.position + new Vector3(0,0.2f,0);
        if ((lastShot - Time.deltaTime) > (1 / bps))
        {
            GameObject bulletOne = Instantiate(prefShot, eyes[0].transform.position, Quaternion.LookRotation(directionOne));
            bulletOne.tag = "Enemy_Bullet";
            GameObject bulletTwo = Instantiate(prefShot, eyes[1].transform.position, Quaternion.LookRotation(directionTwo));
            bulletTwo.tag = "Enemy_Bullet";
            lastShot = Time.deltaTime;
            if (shotSound != null)
                shotSound.PlayOneShot(shotSound.clip);
        }

        else
        {
            lastShot += Time.deltaTime;
        }

    }

    private bool PlayerInSight()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, -direction, out hit))
        {
            if (hit.transform.GetComponent<Player>())
            {
                return true;
            }
        }
        combatTimer += Time.deltaTime;
        return false;
    }
}
