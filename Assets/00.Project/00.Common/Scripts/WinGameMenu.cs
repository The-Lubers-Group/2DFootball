using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    /*
    [Header("Buttons")]
    [SerializeField] private Button menuButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    */
    
    
    [Space(10)]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI myPointsText;

    [Space(10)]
    [Header("Panels")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime = 2;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image background;

    [Space(10)]
    [Header("Stars")]
    [SerializeField] private Image starLeft;
    [SerializeField] private Image starCenter;
    [SerializeField] private Image starRight;

    private UIManager uiManager;
    private GameManager gameManager;

    //[SerializeField] private UIManager uIManager;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();

       //Destroy(GameObject.Find("UIManager(Clone)").GetComponent<UIManager>());
        //Destroy(GameObject.Find("GameManager(Clone)").GetComponent<GameManager>());
    }
    void Start()
    {
        if (uiManager != null)
        {
            Destroy(uiManager.gameObject);
        }
        if (gameManager != null)
        {
            Destroy(gameManager.gameObject);
        }

        PanelFadeIn();
        //canvasGroup.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }

    public void PanelFadeIn()
    {

        //canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, -10000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -60f), fadeTime, false).SetEase(Ease.InOutQuart);
        //canvasGroup.DOFade(1, fadeTime);
        background.DOFade(2, fadeTime);
    }

    public void PanelFadeOut()
    {

        //canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -10000f), fadeTime, false).SetEase(Ease.InOutQuint);
        background.DOFade(1, fadeTime);
        //canvasGroup.DOFade(1, fadeTime);
        //canvasGroup.gameObject.SetActive(false);
    }

    private void OnClickMenu()
    {

    }
}
