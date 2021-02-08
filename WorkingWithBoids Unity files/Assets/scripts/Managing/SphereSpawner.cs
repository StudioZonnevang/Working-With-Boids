using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public enum GizmoTypes { Never, SelectedOnly, Always}

    public GameObject spherePrefab;
    public GameObject boidSpawner;
    public float spawnRadius;
    public float maxOrbitRadius;
    public int spawnMinAmount;
    public int spawnMaxAmount;
    public float minScaleMultiplier;
    public float maxScaleMultiplier;
    public float smallSphereTreshhold;

    int spawnAmount;
    public Color colour;
    public GizmoTypes showSpawnRegion;

    void Awake()
    {
        SpawnSpheres();
    }

    public void RerollSpheres()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject sphereObject in gameObjects)
        {
            Destroy(sphereObject);
        }

        SpawnSpheres();
    }

    void SpawnSpheres()
    {
        spawnAmount = Random.Range(spawnMinAmount, spawnMaxAmount);
        Debug.Log(spawnAmount);

        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            GameObject sphere = Instantiate(spherePrefab);

            float scaleMultiplier = Random.Range(minScaleMultiplier, maxScaleMultiplier);

            if (scaleMultiplier < smallSphereTreshhold)
            {
                Debug.Log("small sphere");
                GameObject[] allSpheres;
                allSpheres = GameObject.FindGameObjectsWithTag("Sphere");
                GameObject randomSphere = allSpheres[Random.Range(0, allSpheres.Length)];

                if (randomSphere == null)
                {
                    spawnAmount = spawnAmount + 1;
                    Debug.Log("plus een");
                    return;
                }
                //if randomsphere.scale < scalemultiplier
                //return;

                Vector3 minOrbitDistance = randomSphere.transform.localScale;
                sphere.transform.localScale = sphere.transform.localScale * scaleMultiplier;
                sphere.transform.position = randomSphere.transform.position + (Random.insideUnitSphere * (maxOrbitRadius - scaleMultiplier)) + minOrbitDistance;
            }
            else
            {
                sphere.transform.localScale = sphere.transform.localScale * scaleMultiplier;
                sphere.transform.position = spawnPosition;
            }

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
