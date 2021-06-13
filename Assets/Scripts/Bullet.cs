using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject impactEffect1;
    [SerializeField] private GameObject impactEffect2;
    [SerializeField] private GameObject impactEffect3;

    // Update is called once per frame
    void Update()
    {
        transform.position += this.transform.forward;
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy en = other.transform.GetComponent<Enemy>();

            if (en != null)
            {
                en.TakeDamage(damage);
            }
            
            GameObject impact = Instantiate(impactEffect3, transform.position, transform.rotation);
            Destroy(impact, 1);
        }
        else if (other.CompareTag("Box"))
        {
            BoxExplosion be = other.transform.GetComponent<BoxExplosion>();

            if (be != null)
            {
                be.isHit();
            }
            
            GameObject impact = Instantiate(impactEffect2, transform.position, transform.rotation);
            Destroy(impact, 1);
        }
        else
        {
            GameObject impact = Instantiate(impactEffect1, transform.position, transform.rotation);
            Destroy(impact, 1);
        }
        
        Destroy(this.gameObject);

    }
}
