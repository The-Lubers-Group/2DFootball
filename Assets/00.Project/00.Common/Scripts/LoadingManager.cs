using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LoadingManager : MonoBehaviour
{
   

    private UIManager uiManager;
    private GameManager gameManager;

    private const string TAG_LOADING_SCENE = "LoadingScene";

    public string level;

    [SerializeField] private float transitionTime = 5f;
    public UnityEvent OnTransitionDone;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        Time.timeScale = 1;
    }

    private void Start()
    {
        if (uiManager != null)
        {
            Destroy(uiManager.gameObject);
        }
        if (gameManager != null)
        {
            Destroy(gameManager.gameObject);
        }

        StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0;
        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            //Debug.Log(currentTime);
            yield return null;
        }
       OnClose(); 
       //OnTransitionDone?.Invoke();
       //SceneManager.UnloadSceneAsync("LoadingScene");
       //SceneManager.LoadScene(level);
    }

    public void OnClose()
    {
        SceneManager.UnloadSceneAsync("LoadingScene");
        OnTransitionDone?.Invoke();
    }

}
