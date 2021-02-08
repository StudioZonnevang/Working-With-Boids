using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    bool menuIsOpen;
    float fadeinDuration = 0.4f;

    public float minSpeed;
    public float maxSpeed;

    int minBuildIndex = 0;
    int maxBuildIndex = 5;

    public GameObject menu;
    public Animator transition;

    public float transitionTime = 10f;
    CanvasGroup canvasGroup;
   
    void Start()
    {
        canvasGroup = menu.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        menuIsOpen = false;
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            if(menuIsOpen == true)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void changeMinSpeed(float newMinSpeed)
    {
        Debug.Log(newMinSpeed);
    }

    public void changeMaxSpeed(float newMaxSpeed)
    {
        maxSpeed = newMaxSpeed;
        Debug.Log(newMaxSpeed);
    }

    public void ExitGame()
    {
        Debug.Log("Quiting Application");
        Application.Quit();
    }

    public void NextScene()
    {
        Debug.Log("Next Scene");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void PreviousScene()
    {
        Debug.Log("Previous Scene");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
    }

    void OpenMenu()
    {
        menu.SetActive(true);

        menuIsOpen = !menuIsOpen;
        Fade();

        GameObject movementScript = GameObject.Find("CameraBG");
        movementScript.GetComponent<MovementScript>().enabled = false;
    }

    void CloseMenu()
    {
        menuIsOpen = !menuIsOpen;
        Fade();

        GameObject movementScript = GameObject.Find("CameraBG");
        movementScript.GetComponent<MovementScript>().enabled = true;
   
    }

    public void Fade()
    {
        StartCoroutine(Fading(canvasGroup, canvasGroup.alpha, menuIsOpen ? 1 : 0));
    }

    public IEnumerator Fading(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;
        while (counter < fadeinDuration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / fadeinDuration);

            yield return null;
        }

        yield return new WaitForSeconds(fadeinDuration);

        if (menuIsOpen)
            menu.SetActive(true);

        if (!menuIsOpen)
            menu.SetActive(false);
    }

    public IEnumerator LoadScene(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        if (levelIndex == maxBuildIndex + 1)
        {
            levelIndex = minBuildIndex;
        }

        if (levelIndex == minBuildIndex - 1)
        {
            levelIndex = maxBuildIndex;
        }

        SceneManager.LoadScene(levelIndex);
    }
}
