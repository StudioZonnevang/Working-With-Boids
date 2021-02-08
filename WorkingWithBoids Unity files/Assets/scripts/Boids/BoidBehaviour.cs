using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    BoidSettings settings;

    //state
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    //to update
    Vector3 acceleration;
    [HideInInspector]
    public Vector3 averageFlockHeading;
    [HideInInspector]
    public Vector3 averageAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numberPerceivedFlockmates;

    //cashed
    Material material;
    Transform cashedTransform;
    Transform target;

    void Awake()
    {
        material = transform.GetComponentInChildren<MeshRenderer>().material;   //get material from child
        cashedTransform = transform; 
    }

    public void Initialize (BoidSettings settings, Transform target)
    {
        this.target = target;
        this.settings = settings;

        position = cashedTransform.position;
        forward = cashedTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = cashedTransform.forward * startSpeed;
    }

    public void SetColour(Color col)                                        //weet niet hoe dit werkt, wordt gecalled in de boid spawner
    {
        if (material != null)
        {
            material.color = col;
        }
    }

    public void BoidUpdate()
    {
        Vector3 acceleration = Vector3.zero;

        if (target != null)
        {
            Vector3 offsetToTarget = (target.position - position);
            acceleration = SteerTowards(offsetToTarget) * settings.targetWeight;
        }

        if (numberPerceivedFlockmates != 0)
        {
            centreOfFlockmates /= numberPerceivedFlockmates; // means centre of flockmates = COF/Nr.OFM

            Vector3 offsetToCentreOfFlockmates = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards(averageFlockHeading) * settings.alignWeight;
            var cohesionForce = SteerTowards(offsetToCentreOfFlockmates) * settings.cohesionWeight;
            var seperationForce = SteerTowards(averageAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision())
        {
            Vector3 collisionAvoidDirection = ObstacleRays();
            Vector3 collisionAvoidanceForce = SteerTowards(collisionAvoidDirection) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidanceForce;
        }

        velocity            += acceleration * Time.deltaTime;
        float speed         = velocity.magnitude;
        Vector3 direction   = velocity / speed;
        speed               = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity            = direction * speed;

        cashedTransform.position    += velocity * Time.deltaTime;
        cashedTransform.forward     = direction;
        position                    = cashedTransform.position;
        forward                     = direction;
    }

    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDistance, settings.obstacleMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector3 ObstacleRays()
    {
        Vector3[] rayDirection = BoidHelper.directions;

        for (int i = 0; i < rayDirection.Length; i ++)
        {
            Vector3 directionForRay = cashedTransform.TransformDirection(rayDirection[i]);      //transforms space from local direction to global direction
            Ray ray = new Ray(position, directionForRay);
            if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDistance, settings.obstacleMask))
            {
                return directionForRay;
            }
        }
        return forward;
    }

    Vector3 SteerTowards (Vector3 vector)   //takes magnitude and direction
    {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;   //takes the direction, normalizes magnitude but multiplies by the max speed. also subtracts the velocity leaving a directional acceleration
        return Vector3.ClampMagnitude(v, settings.maxSteerForce);
    }
}
