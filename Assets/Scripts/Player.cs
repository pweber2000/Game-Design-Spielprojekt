using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    private float health;
    private float stamina;
    private Weapon weapon;
    private float ammunition;

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
}
