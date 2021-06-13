using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    public float health = 100f;
    
    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0f)
        {
            Kill();
        }
    }

    void Kill()
    {
        if(explosion != null)
        {
            GameObject explo = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explo, 4f);
        }
        Destroy(gameObject);
    }
}
