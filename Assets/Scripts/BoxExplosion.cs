using System;
using UnityEngine;

public class BoxExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject key;

    [SerializeField]
    private bool isDestructible = true;

    [SerializeField] private bool dropsAmmo = false;
    

    public void isHit()
    {
        if(isDestructible)
        {
            if (explosion != null)
            {
                GameObject explo = Instantiate(explosion, transform.position, transform.rotation);
            }

            Destroy(gameObject);

            if (key != null && !Player.player.hasKey(key.GetComponent<PickUpController>().getKeyId()))
            {
                Vector3 keypos = new Vector3(0, 1.5f, 0);
                DropOnDeath.dropOnDeath.Drop(key, transform, keypos);
                //Instantiate(key, transform.position + keypos, new Quaternion(0,0,0,0));
            }

            else if (key == null && dropsAmmo)
                DropOnDeath.dropOnDeath.DropAmmo(transform, new Quaternion(0, 0, 0, 0), new Vector3(0, 0.5f, 0));
        }
    }

}
