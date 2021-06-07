using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        Destroy(gameObject);
    }
   
}
