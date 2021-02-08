using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;
    BoidBehaviour[] boids;

    void Start()
    {
        boids = FindObjectsOfType<BoidBehaviour>();
        foreach (BoidBehaviour b in boids)
        {
            b.Initialize(settings, null);
        }            
    }

    void Update()
    {
        if (boids != null)
        {
            int amountBoids = boids.Length;
            var boidData = new BoidData[amountBoids];

            for (int i = 0; i < amountBoids; i++)
            {
                boidData[i].position = boids[i].position;
                boidData[i].direction = boids[i].forward;
            }

            var boidBuffer = new ComputeBuffer(amountBoids, BoidData.Size);
            boidBuffer.SetData(boidData);

            compute.SetBuffer(0, "boids", boidBuffer);
            compute.SetInt("amountBoids", boids.Length);
            compute.SetFloat("viewRadius", settings.perceptionRadius);
            compute.SetFloat("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt(amountBoids / (float)threadGroupSize);   //mathf.ceiltoint makes a float an int always providing an equalto or grater than nr. (10.3 -> 11)
            compute.Dispatch(0, threadGroups, 1, 1);

            boidBuffer.GetData(boidData);

            for (int i = 0; i < boids.Length; i++)
            {
                boids[i].averageFlockHeading = boidData[i].flockDirection;
                boids[i].centreOfFlockmates = boidData[i].flockCentre;
                boids[i].averageAvoidanceHeading = boidData[i].avoidanceDirection;
                boids[i].numberPerceivedFlockmates = boidData[i].numberOfFlockmates;

                boids[i].BoidUpdate();
            }

            boidBuffer.Release();
        }
    }

    public struct BoidData
    {
        //info individual boid
        public Vector3 position;
        public Vector3 direction;

        //info collective boids perceived from individual
        public Vector3 flockDirection;
        public Vector3 flockCentre;
        public Vector3 avoidanceDirection;
        public int numberOfFlockmates;

        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}
