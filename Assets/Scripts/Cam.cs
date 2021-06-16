using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public static Cam instance;
    private Camera cam;
    
    private bool isShaking = false;
    private Vector3 orignialPos;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("Instance Cam erstellt");
        }

        else
            Debug.Log("Instance Cam konnte nicht erstellt werden");
    }

    private void Start()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
            Debug.Log("Camera gefunden");
        }
        else
        {
            Debug.Log("Camera nicht gefunden");
        }
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(Shaking(duration, magnitude));
    }

    // Update is called once per frame
    private IEnumerator Shaking(float duration, float magnitude)
    {
        Debug.Log("Shaking");
        if(!isShaking)
            orignialPos = transform.localPosition;
        //Debug.Log(orignialPos);
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            isShaking = true;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition += new Vector3(x, y, orignialPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
        cam.transform.localPosition = orignialPos;
    }

    public bool IsShaking()
    {
        return isShaking;
    }
}
