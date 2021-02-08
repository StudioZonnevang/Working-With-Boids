using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
    float rotationX;
    float rotationY;

    public float rotateSpeed = 1f;

    Menu menu;
    BackgroundSpriteToggle bgToggle;
    SphereSpawner respawn;
    // Start is called before the first frame update
    void Start()
    {
        GameObject menuFound = GameObject.Find("GameManager");
        menu = menuFound.GetComponent<Menu>();

        GameObject background = GameObject.Find("BG");
        bgToggle = background.GetComponent<BackgroundSpriteToggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
            menu.NextScene();

        if (Input.GetKeyUp(KeyCode.Q))
            menu.PreviousScene();

        if (Input.GetKeyUp(KeyCode.B))
            bgToggle.ChangeSprite();

        if (Input.GetKeyUp(KeyCode.R))
        {
            GameObject rerollBalls = GameObject.Find("Sphere Spawner");
            respawn = rerollBalls.GetComponent<SphereSpawner>();

            if (respawn != null)
                respawn.RerollSpheres();
        }


        float horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
        float verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * rotateSpeed;

        rotationX += verticalInput;
        rotationY -= horizontalInput;

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
