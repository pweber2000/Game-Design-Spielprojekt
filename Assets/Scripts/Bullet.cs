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
    [SerializeField] private GameObject model;

    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("Enemy_Bullet"))
            transform.position += this.transform.forward;
        else
            transform.position += this.transform.forward;
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(!this.CompareTag("Enemy_Bullet"))
            Destroy(model);
        if (other.CompareTag("Enemy") && !this.CompareTag("Enemy_Bullet"))
        {
            Enemy en = other.transform.GetComponent<Enemy>();

            if (en != null)
            {
                en.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect3, transform.position, transform.rotation);
            Destroy(impact, 0.5f);
        }
        else if (other.CompareTag("Box"))
        {
            BoxExplosion be = other.transform.GetComponent<BoxExplosion>();

            if (be != null)
            {
                be.isHit();
            }

            GameObject impact = Instantiate(impactEffect2, transform.position, transform.rotation);
            Destroy(impact, 0.5f);
        }
        else if (other.CompareTag("Player"))
        {
            Player.player.TakeDamage(damage);
            if(model != null)
                Destroy(model);

        }
        else if (this.CompareTag("Enemy_Bullet"))
        {

        }
        else 
        {
            GameObject impact = Instantiate(impactEffect1, transform.position, transform.rotation);
            Destroy(impact, 0.5f);
        }

        Destroy(this.gameObject, 1f);

    }
}
