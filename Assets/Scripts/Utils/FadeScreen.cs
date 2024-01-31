using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FadeScreen : MonoSinglenton<FadeScreen>
{
    [SerializeField] CanvasGroup overlay;
    [SerializeField] float fadeSpeed = .8f;
    [SerializeField] UnityEvent onStateChanged;

    private float duration;

    public bool IsFading { private set; get; }
    public enum Type { FadeIn, FadeOut, FadeInOut }

    public void Fade(Type type, float duration = 1f, Action action = null)
    {
        if (IsFading)
            return;

        this.duration = duration;
        if (type == Type.FadeIn)
        {
            overlay.alpha = 0;
            StartCoroutine(Fade(true, action));
        }
        else if (type == Type.FadeOut)
            StartCoroutine(Fade(false, action));
        else
            StartCoroutine(FadeInOut(action));
    }

    IEnumerator Fade(bool fadeIn, Action action)
    {
        float progress = 0;
        IsFading = true;
        overlay.gameObject.SetActive(true);
        onStateChanged.Invoke();

        while (progress <= duration)
        {
            progress += fadeSpeed * Time.deltaTime;
            if (fadeIn)
                overlay.alpha = progress / duration;
            else
                overlay.alpha = 1f - (progress / duration);
            yield return new WaitForEndOfFrame();
        }

        IsFading = false;
        overlay.gameObject.SetActive(false);
        onStateChanged.Invoke();
        action?.Invoke();
    }

    IEnumerator FadeInOut(Action action)
    {
        yield return Fade(true, null);
        yield return Fade(false, action);
    }

    [ContextMenu("FADE IN")]
    void FadeIn() => Fade(Type.FadeIn);
    [ContextMenu("FADE OUT")]
    void FadeOut() => Fade(Type.FadeOut);
    [ContextMenu("FADE IN-OUT")]
    void FadeInOut() => Fade(Type.FadeInOut);
}
