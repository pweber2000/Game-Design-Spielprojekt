using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    [SerializeField]
    private float health_max = 200f;
    private float health = 110.5f;
    
    [SerializeField]
    private  float stamina = 100f;

    private Weapon weapon;
    private int ammunition = 999;

    private bool[] keys;
    private int numberOfKeys = 0;
    [SerializeField]
    private GameObject spawner;
    private Transform spawnPoint;


    private CharacterController charControl;
    private void Awake()
    { 
        if(player == null)
            player = this;

        charControl = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = health_max;
        keys = new bool[] {false, false, false, false}; //schwarz, rot, blau, grün
        stamina = 100f;

        if (spawner != null)
            spawnPoint = Instantiate<GameObject>(spawner, this.transform.position, this.transform.rotation).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Die();
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

    public  bool hasKey(int keyID)
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

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Die()
    {
        charControl.enabled = false;
        this.transform.position = spawnPoint.position;
        charControl.enabled = true;
        health = health_max;

    }
}
