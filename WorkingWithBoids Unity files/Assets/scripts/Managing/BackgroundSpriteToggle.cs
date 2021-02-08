using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpriteToggle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public List<Sprite> sprites = new List<Sprite>();
    static int currentSprite;
    static bool firstStart;
    static int nextSprite;
    float fadeDuration = 0.2f;

    public Sprite BG01;
    public Sprite BG02;
    public Sprite BG03;
    public Sprite BG04;
    public Sprite BG05;

    void Start()
    {
        sprites.Add(BG01);
        sprites.Add(BG02);
        sprites.Add(BG03);
        sprites.Add(BG04);
        sprites.Add(BG05);

        //Debug.Log(firstStart);

        if (currentSprite != null)
            spriteRenderer.sprite = sprites[currentSprite];
        else
            currentSprite = 0;
    }

    public void ChangeSprite()
    {
        StartCoroutine(Fading(spriteRenderer, 1, 0));

        nextSprite = currentSprite + 1;

        if (nextSprite == 5)
            nextSprite = 0;

        currentSprite = nextSprite;

        Debug.Log("the current background nr is: " + currentSprite);


    }

    public IEnumerator Fading(SpriteRenderer SPR, float start, float end)
    {
        float counter = 0f;
        while (counter < fadeDuration)
        {
            var SPRColor = SPR.color;
            counter += Time.deltaTime;
            SPRColor.a = Mathf.Lerp(start, end, counter / fadeDuration);
            SPR.color = SPRColor;

            yield return null;
        }

        yield return new WaitForSeconds(fadeDuration);

        spriteRenderer.sprite = sprites[nextSprite];

        counter = 0f;
        while (counter < (fadeDuration + 0.1f))
        {
            var SPRColor = SPR.color;
            counter += Time.deltaTime;
            SPRColor.a = Mathf.Lerp(end, start, counter / (fadeDuration + 0.1f));
            SPR.color = SPRColor;

            yield return null;
        }
    }
}
