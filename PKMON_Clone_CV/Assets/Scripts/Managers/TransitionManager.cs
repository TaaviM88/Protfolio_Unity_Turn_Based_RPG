using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }
    [SerializeField] private Image fadeImage; // UI Image for fading
    [SerializeField] private float fadeDuration = 0.5f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void FadeToBattle(Action onFadeOut, Action onFadeIn)
    {
        Sequence fadeSequence = DOTween.Sequence();
        // Fade out (screen to black)
        fadeSequence.Append(fadeImage.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad));
        // Perform the onFadeOut action
        fadeSequence.AppendCallback(() => onFadeOut?.Invoke());

        // Fade in (black to transparent)
        fadeSequence.Append(fadeImage.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad));

        // Perform the onFadeIn action (e.g., set active scene)
        fadeSequence.AppendCallback(() => onFadeIn?.Invoke());
    }
}
