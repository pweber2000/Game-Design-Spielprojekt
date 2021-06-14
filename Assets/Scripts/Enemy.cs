using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    private Animator anim;

    public float health = 100f;
    [SerializeField]
    private float rangeTrigger = 25f;
    [SerializeField]
    private float rangeAttack = 20f;
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private GameObject[] eyes;
    [SerializeField]
    private GameObject prefShot;
    [SerializeField]
    private float bps = 2f;
    private float lastShot = 0f;

    private bool isTriggered = false;


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
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        Vector3 direction = this.transform.position - Player.player.transform.position;
        Quaternion rot = Quaternion.LookRotation(-direction);

        float distance = Vector3.Distance(Player.player.transform.position, this.transform.position);
        if (distance < rangeTrigger)
        {
            open();
            isTriggered = true;

            if (isTurning())
            {

            }

            else
            {
                anim.SetBool("Walk_Anim", false);
            }

            if (distance < rangeAttack)
            {
                Fire();
            }

            else
            {
                close();
            }

        }

        else
        {
            close();
        }
    }

    private void open()
    {
        if (anim != null)
        {
            anim.SetBool("Open_Anim", true);
        }

    }


    private void close()
    {
        if (anim != null)
        {
            anim.SetBool("Walk_Anim", false);
            anim.SetBool("Roll_Anim", false);
            anim.SetBool("Open_Anim", false);
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
                anim.SetBool("Walk_Anim", true);
                Quaternion rot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime * 2);

            }

            else
            {
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
        }

        else
            lastShot += Time.deltaTime;
    }
}
