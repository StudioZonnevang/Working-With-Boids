using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public float duration;
    private bool isFaded;
    float counter = 0f;

    public GameObject panel;

    void Start()
    {
        panel.SetActive(true);
    }
    
    public void Fade()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(Fading(canvasGroup, canvasGroup.alpha, isFaded ? 1 : 0));

        isFaded = !isFaded;

        Destroy(panel, duration);
    }

    public IEnumerator Fading(CanvasGroup canvasGroup, float start, float end)
    {
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }
}
