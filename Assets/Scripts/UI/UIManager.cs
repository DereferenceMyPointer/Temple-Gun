using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [Header("Components")]
    public Image screenCover;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PController.Instance.enabled = false;
        FadeFromBlack(1f);
    }

    public void FadeFromBlack(float seconds)
    {
        StartCoroutine(FadeIn(seconds));
    }

    public IEnumerator FadeIn(float t)
    {
        float tCurrent = 0;
        while (tCurrent <= t)
        {
            tCurrent += Time.deltaTime;
            Color p = screenCover.color;
            p.a = (t - tCurrent) / t;
            if (tCurrent >= t)
            {
                p.a = 0;
            }
            screenCover.color = p;
            yield return new WaitForEndOfFrame();
        }
        PController.Instance.enabled = true;
    }

    public void FadeToBlack(float seconds)
    {
        StartCoroutine(FadeOut(seconds));
    }

    public IEnumerator FadeOut(float t)
    {
        float tCurrent = t;
        while (tCurrent >= 0)
        {
            tCurrent -= Time.deltaTime;
            Color p = screenCover.color;
            p.a = (t - tCurrent) / t;
            if (tCurrent <= 0)
            {
                p.a = 1;
            }
            screenCover.color = p;
            yield return new WaitForEndOfFrame();
        }
        PController.Instance.enabled = true;
    }

}
