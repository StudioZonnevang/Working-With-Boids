using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyGlow : MonoBehaviour
{
    public Light fireflyLight;
    float lightIntensity;
    public float lightTime;
    public float darkTime;

    void Start()
    {

    }

    void Update()
    {
        StartCoroutine(LightIntenstityUpdate());
    }

    IEnumerator LightIntenstityUpdate()
    {
        lightIntensity = 2f;
        fireflyLight.intensity = lightIntensity;

        yield return new WaitForSeconds(lightTime);

        lightIntensity = 0f;
        fireflyLight.intensity = lightIntensity;

        yield return new WaitForSeconds(darkTime);
    }
}
