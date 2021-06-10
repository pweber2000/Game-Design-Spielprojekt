using UnityEngine;

public class BoxExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject key;

    [SerializeField]
    private bool isDestructible = true;
    
    [SerializeField]
    private bool spawnKey = false;

    [SerializeField]
    private float explosionDuration = 1.5f;

    public void isHit()
    {
        if(isDestructible)
        {
            if (explosion != null)
            {
                GameObject explo = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(explo, explosionDuration);
            }

            Destroy(gameObject);

            if (spawnKey && key != null)
            {
                Vector3 keypos = new Vector3(0, 1, 0);
                Instantiate(key, transform.position + keypos, transform.rotation);
            }
        }
    }

}
