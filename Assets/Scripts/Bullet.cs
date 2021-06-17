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
    //Wird benoetigt, damit die Kugel auch nur eine Sache trifft und nicht durchfliegt
    [SerializeField] private CapsuleCollider col;
    [SerializeField] private AudioSource bulletSound;

    private void Start()
    {
        SoundManager.soundManager.PlaySoundAt(bulletSound, transform);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
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
        if(other.CompareTag("Enemy") && this.CompareTag("Enemy_Bullet"))
            return;
  
        if (other.CompareTag("Bullet") || other.CompareTag("Enemy_Bullet"))
            return;

        if (other.CompareTag("Enemy"))
        {
            Enemy en = other.transform.GetComponent<Enemy>();

            if (en != null)
            {
                en.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect3, transform.position, transform.rotation);
            Destroy(impact, 1f);
        }
        else if (other.CompareTag("Box"))
        {
            BoxExplosion be = other.transform.GetComponent<BoxExplosion>();

            if (be != null)
            {
                be.isHit();
            }

            GameObject impact = Instantiate(impactEffect2, transform.position, transform.rotation);
            Destroy(impact, .5f);
        }
        else if (other.CompareTag("Generator"))
        {
            GeneratorExplosion ge = other.transform.GetComponent<GeneratorExplosion>();

            if (ge != null)
            {
                ge.isHit();
            }

            GameObject impact = Instantiate(impactEffect2, transform.position, transform.rotation);
            Destroy(impact, .5f);
            Destroy(col);
        }
        else if (other.CompareTag("Player") && !this.CompareTag("Bullet"))
        {
            Player.player.TakeDamage(damage);
        }
        else if (this.CompareTag("Enemy_Bullet") || other.CompareTag("Transparent") || other.CompareTag("Respawn") ||
                 other.CompareTag("Ammo") || other.CompareTag("AmmoEnemy") || other.CompareTag("AmmoBigEnemy"))
        {
            return;
        }
        else
        {
            GameObject impact = Instantiate(impactEffect1, transform.position, transform.rotation);
            Destroy(impact, .5f);
        }

        Debug.Log(other.name);
        Destroy(gameObject);

    }
}
