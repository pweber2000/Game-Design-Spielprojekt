using System;
using UnityEngine;

public class BoxExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private float explosionRadius = 4f;

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
                float distance = Vector3.Distance(Player.player.transform.position, this.transform.position);
                if (explosionRadius >= distance)
                {
                    if (!Cam.instance.IsShaking())
                        Cam.instance.Shake(.20f, .3f);
                    Player.player.TakeDamage(Player.player.getHealthMax() * 0.1f);
                }
            }

            Destroy(gameObject);

            if (key != null && !Player.player.hasKey(key.GetComponent<PickUpController>().getKeyId()))
            {
                Vector3 keypos = new Vector3(0, 1.5f, 0);
                DropOnDeath.dropOnDeath.Drop(key, transform, keypos);
                //Instantiate(key, transform.position + keypos, new Quaternion(0,0,0,0));
            }

            else if (key == null && dropsAmmo)
            {
                bool[] randomizer = { false, true, false };

                int rand = UnityEngine.Random.Range(0, 2);

                if (randomizer[rand])
                {
                    DropOnDeath.dropOnDeath.DropAmmo1(transform, new Quaternion(0, 0, 0, 0), new Vector3(0, 0.5f, 0));
                }
            }
        }
    }

}
