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
    //static bool[] keyDropped = { false, false, false, false};
    int keyID;

    [SerializeField]
    private bool isDestructible = true;

    [SerializeField] private bool dropsAmmo = false;

    private void Start()
    {
        if(key != null)
            keyID = key.GetComponent<PickUpController>().getKeyId();
    }

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
                    //if (!Cam.instance.IsShaking())
                    //    Cam.instance.Shake(.20f, .05f);
                    Player.player.TakeDamage(Player.player.getHealthMax() * 0.1f);
                }
            }

            Destroy(gameObject);

            if (key != null && !Player.player.hasKey(keyID))
            {
                Vector3 keypos = new Vector3(0, 1.5f, 0);
                DropOnDeath.dropOnDeath.Drop(key, transform, keypos);
                //keyDropped[keyID - 1] = true;
                //Instantiate(key, transform.position + keypos, new Quaternion(0,0,0,0));
            }

            else if (key == null && dropsAmmo)
            {
                //bool[] randomizer = { false, true, false };

                int rand = UnityEngine.Random.Range(0, 100);

                if (rand > 66)
                {
                    DropOnDeath.dropOnDeath.DropAmmo1(transform, new Quaternion(0, 0, 0, 0), new Vector3(0, 0.5f, 0));
                }
            }
        }
    }

}
