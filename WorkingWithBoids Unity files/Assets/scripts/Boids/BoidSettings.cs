using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject
{
    public float minSpeed = 2f;          //speed damping
    public float maxSpeed = 5f;
    public float maxSteerForce = 3f;     //max steer circle

    public float perceptionRadius = 2.5f;  //how far can it look
    public float avoidanceRadius = 1f;   //

    public float alignWeight = 1f;       //align/match velocity (direction and speed)
    public float cohesionWeight = 1f;    //flock centering
    public float seperateWeight = 1f;    

    public float targetWeight = 1f;      //

    [Header("Collisions")]
    public LayerMask obstacleMask;

    public float boundsRadius = 0.27f;
    public float avoidCollisionWeight = 10f;
    public float collisionAvoidDistance = 5f;

    //public float maxAcceleration;
    //public float gravity;
}
