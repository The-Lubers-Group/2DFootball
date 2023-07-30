using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private const String TAG_TRANSITION_SCREAN = "TransitionScrean";

    [SerializeField] private Material screenTransitionMaterial;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private String propertyName = "_Progress";

    public UnityEvent OnTransitionDone;

    private void Start()
    {
        StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0;
        while(currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            screenTransitionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));
            yield return null;
        }
        OnTransitionDone?.Invoke();
        SceneManager.UnloadSceneAsync(TAG_TRANSITION_SCREAN);

    }
}
