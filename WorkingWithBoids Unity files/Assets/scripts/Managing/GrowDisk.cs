using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowDisk : MonoBehaviour
{
    private Vector3 scale;
    private Vector3 desiredScale;
    public Vector3 desiredScaleMultiplier;
    public float growSpeed;

    void Start()
    {
        scale = transform.localScale;
        desiredScale = scale + desiredScaleMultiplier;
    }

    void Update()
    {
        float max = desiredScale.y;

        if (transform.localScale.y < max)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, growSpeed * Time.deltaTime);
        }
    }

}

