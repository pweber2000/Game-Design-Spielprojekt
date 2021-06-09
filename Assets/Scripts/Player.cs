using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    private static float health = 110.5f;
    
    [SerializeField]
    private static float stamina = 100f;

    private static Weapon weapon;
    private static int ammunition = 90;

    private void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        stamina = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int getAmmunition()
    {
        return ammunition;
    }

    public static void setAmmunition(int number)
    {
        ammunition = number;
    }

    public static float getHealth()
    {
        return health;
    }

    public static void setHealth(float number)
    {
        health = number;
    }

    public static float getStamina()
    {
        return stamina;
    }

    public static void setStamina(float number)
    {
        if (number > 100)
            stamina = 100f;
        
        else 
            stamina = number;
    }

}
