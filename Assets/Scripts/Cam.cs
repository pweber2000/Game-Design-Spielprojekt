using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public static Cam instance;
    private Camera cam;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
    }


    // Update is called once per frame
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignialPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = orignialPos;
    }
}
