using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [Space(10)]
    [Header("Stars")]
    [SerializeField] private Image starLeft;
    [SerializeField] private Image starCenter;
    [SerializeField] private Image starRight;
     
    void Start()
    {
        PanelFadeIn();
        //canvasGroup.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }

    public void PanelFadeIn()
    {

        canvasGroup.alpha = 0f;
        rectTransform.transform.localPosition = new Vector3(0f, -10000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -60f), fadeTime, false).SetEase(Ease.InOutQuart);
        canvasGroup.DOFade(1, fadeTime);
    }

    public void PanelFadeOut()
    {

        canvasGroup.alpha = 1f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -10000f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(1, fadeTime);
        //canvasGroup.gameObject.SetActive(false);
    }

    private void OnClickMenu()
    {

    }
}
