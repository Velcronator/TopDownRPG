using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image _fadeScreen;
    [SerializeField] private float _fadeSpreed = 1f;

    private IEnumerator fadeRoutine;

    public void FadeToBlack()
    {
        if(fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }

    public void FadeToClear() 
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(_fadeScreen.color.a, targetAlpha ))
        {
            float alpha = Mathf.MoveTowards(_fadeScreen.color.a, targetAlpha, _fadeSpreed * Time.deltaTime);
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, alpha);
            yield return null;
        }
    }


}
