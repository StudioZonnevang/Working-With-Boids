using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float smoothTime = 0.5f;
    public Transform target;
    public Transform lookAtTarget;
    bool gameStarted;
    static bool firstStartGame;

    void Start()
    {
        //Debug.Log(firstStartGame);

        transform.localPosition = new Vector3(0f, 0f, -60f);
        gameStarted = false;
    }

    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        if (firstStartGame)
        {
            Transform();
        }

        if (gameStarted)
        {
            Transform();
        }
    }

    public void SetUpCamera()
    {
        //Debug.Log("set up camera");
        gameStarted = true;
        firstStartGame = true;
    }

    void Transform()
    {
        Vector3 targetPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
