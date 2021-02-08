using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject moving;
    public GameObject background;
    public GameObject menu;
    public GameObject scene;

    CanvasGroup movingCanvas;
    CanvasGroup backgroundCanvas;
    CanvasGroup menuCanvas;
    CanvasGroup sceneCanvas;

    static bool hadMoving = false;
    static bool hadBackground = true;
    static bool hadMenu = true;
    static bool hadScene = true;

    public float counter1 = 0.1f;
    public float counter2 = 0.1f;
    public float duration = 1f;

    void Start()
    {
        moving.SetActive(true);
        background.SetActive(true);
        menu.SetActive(true);
        scene.SetActive(true);

        movingCanvas = moving.GetComponent<CanvasGroup>();
        backgroundCanvas = background.GetComponent<CanvasGroup>();
        menuCanvas = menu.GetComponent<CanvasGroup>();
        sceneCanvas = scene.GetComponent<CanvasGroup>();

        movingCanvas.alpha = 1f;
        backgroundCanvas.alpha = 0f;
        menuCanvas.alpha = 0f;
        sceneCanvas.alpha = 0f;

        if (hadMoving == true)
            moving.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hadMoving);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            if (hadMoving == false)
            {
                StartCoroutine(FadeOut(movingCanvas, moving, 1, 0));
                StartCoroutine(FadeIn(backgroundCanvas, background, 0, 1));
                hadBackground = false;
            }
            hadMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("background");
            if (hadBackground == false)
            {
                StartCoroutine(FadeOut(backgroundCanvas, background, 1, 0));
                StartCoroutine(FadeIn(menuCanvas, menu, 0, 1));
                hadMenu = false;
            }
            hadBackground = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (hadMenu == false)
            {
                StartCoroutine(FadeOut(menuCanvas, menu, 1, 0));
                StartCoroutine(FadeIn(sceneCanvas, scene, 0, 1));
                hadScene = false;
            }
            hadMenu = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            if (hadScene == false)
            {
                StartCoroutine(FadeOut(sceneCanvas, scene, 1, 0));
                hadScene = true;
            }

        }
    }

    
    public IEnumerator FadeIn(CanvasGroup canvasGroup, GameObject gameobject, float start, float end)
    {
        gameobject.SetActive(true);

        counter1 = 0f;

        //yield return new WaitForSeconds(duration);

        while (counter1 < duration)
        {
            counter1 += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter1 / duration);

            yield return null;
        }
    }

    public IEnumerator FadeOut(CanvasGroup canvasGroup, GameObject gameobject, float start, float end)
    {
        counter2 = 0f;

        while (counter2 < duration)
        {
            counter2 += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter2 / duration);

            yield return null;
        }

        yield return new WaitForSeconds(duration);

        gameobject.SetActive(false);
    }
}
