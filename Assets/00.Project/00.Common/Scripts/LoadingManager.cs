using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;



public class LoadingManager : MonoBehaviour
{
    private UIManager uiManager;
    private GameManager gameManager;

    private const string TAG_LOADING_SCENE = "LoadingScene";

    public string level;

    [SerializeField] private float transitionTime = 5f;
    public UnityEvent OnTransitionDone;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image loadingProgressBar;
    //[SerializeField] private GameObject btn;



    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        //gameManager = FindObjectOfType<GameManager>();
        Time.timeScale = 1;
    }

    private void Update()
    {
        text.transform.DOScale(new Vector3(1.25f, 1.5f, 1), 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        //text.transform.DOScale(new Vector3(1.25f, 1.5f, 1), 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        //tweener = transform.DOScale(1.6f, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

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
            loadingProgressBar.fillAmount = Mathf.Clamp01(currentTime / transitionTime);
            yield return null;
        }
        //btn.SetActive(true);
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
