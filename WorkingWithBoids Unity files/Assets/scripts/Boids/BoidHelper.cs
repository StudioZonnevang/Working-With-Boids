using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoidHelper
{
    const int numberViewDirections = 300;
    public static readonly Vector3[] directions;

    static BoidHelper()
    {
        directions = new Vector3[BoidHelper.numberViewDirections];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio; //step size of angles

        for (int i = 0; i < numberViewDirections; i++)
        {
            float t = (float)i / numberViewDirections;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;             //azimuth = direction to object from observer

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            directions[i] = new Vector3(x, y, z);
        }
    }
}
