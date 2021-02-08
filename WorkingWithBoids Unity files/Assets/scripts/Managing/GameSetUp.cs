using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public GameObject startUpPanel;
    public GameObject menu;
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject worldSphere;
    public GameObject cameraAndBackground;
    public GameObject boidController;
    public GameObject crossfade;
    public GameObject tutorial;

    static bool firstStartGame;

    void Awake()
    {
        tutorial.SetActive(true);
        startUpPanel.SetActive(true);
        menu.SetActive(true);
        spawner1.SetActive(true);
        spawner2.SetActive(true);
        worldSphere.SetActive(true);
        cameraAndBackground.SetActive(true);
        boidController.SetActive(true);
        crossfade.SetActive(true);

        if (firstStartGame)
        {
            startUpPanel.SetActive(false);
        }

        firstStartGame = true;
    }

}
