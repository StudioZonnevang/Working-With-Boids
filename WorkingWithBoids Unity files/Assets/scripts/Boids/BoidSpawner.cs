using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public enum GizmoTypes { Never, SelectedOnly, Always}

    public BoidBehaviour prefab;
    public float spawnRadius;
    public int spawnAmount;
    public Color colour;
    public GizmoTypes showSpawnRegion;

    void Awake()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius; //takes random position within spawn circle
            BoidBehaviour boid = Instantiate(prefab);                                           //create model in space
            boid.transform.position = spawnPosition;                                            //sets the 'transform' of the boid to the randomized spawnposition
            boid.transform.forward = Random.insideUnitSphere;                                   //direction of boid is randomly selected

            boid.SetColour(colour);
        }
    }

    private void OnDrawGizmos()
    {
        if (showSpawnRegion == GizmoTypes.Always)
        {
            DrawGizmos();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showSpawnRegion == GizmoTypes.SelectedOnly)
        {
            DrawGizmos();
        }
    }

    void DrawGizmos()
    {
        Gizmos.color = new Color(colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }

}
