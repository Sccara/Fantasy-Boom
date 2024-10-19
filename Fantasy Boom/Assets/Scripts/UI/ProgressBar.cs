using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public delegate void OnProgressEvent(float progress);
    public delegate void OnCompletedEvent();

    public OnProgressEvent OnProgress;
    public OnCompletedEvent OnCompleted;

    [SerializeField] private TextMeshProUGUI barText;
    [SerializeField] private Image progressImage;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private float defaultSpeed;

    private Coroutine AnimationCoroutine;

    private void Start()
    {
        OnProgress += SetText;

        if (progressImage.type != Image.Type.Filled)
        {
            Debug.LogError("Image is not type \"Filled\"!");
            enabled = false;
        }
    }

    public void SetProgress(float progress)
    {
        SetProgress(progress, defaultSpeed);
    }

    public void SetProgress(float progress, float speed)
    {
        if (progress < 0 || progress > 1)
        {
            progress = Mathf.Clamp01(progress);
        }

        if (progress != progressImage.fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
        
            AnimationCoroutine = StartCoroutine(AnimateProgress(progress, speed));
        }
    }

    private IEnumerator AnimateProgress(float progress, float speed)
    {
        float time = 0;
        float initialProgress = progressImage.fillAmount;

        while (time < 1)
        {
            progressImage.fillAmount = Mathf.Lerp(initialProgress, progress, time);
            time += Time.deltaTime * speed;

            progressImage.color = colorGradient.Evaluate(1 - progressImage.fillAmount);

            OnProgress?.Invoke(progressImage.fillAmount);
            yield return null;
        }

        progressImage.fillAmount = progress;
        OnProgress?.Invoke(progress);
        OnCompleted?.Invoke();
    }

    private void SetText(float progress)
    {
        barText.text = $"{progress * 100}%";
    }
}
