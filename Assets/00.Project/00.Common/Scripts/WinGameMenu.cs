using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinGameMenu : MenuManager
{
    [Space(10)]
    [Header("Stars")]
    [SerializeField] private Image starLeft;
    [SerializeField] private Image starCenter;
    [SerializeField] private Image starRight;
    public override void PanelFadeIn()
    {
        //myPointsText.text = "+ " + ScoreManager.instance.coins.ToString();
        //myPointsText.text = "+ " + coinsNumResult;
        //myPointsText.text = ScoreManager.instance.coins.ToString();

        rectTransform.transform.localPosition = new Vector3(0f, -10000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -60f), fadeTime, false).SetEase(Ease.InOutQuart);
        background.DOFade(2, fadeTime);
        background.transform.DOLocalRotate(new Vector3(0, 0, 360), 10, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }

    public override void PanelFadeOut()
    {
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, -10000f), fadeTime, false).SetEase(Ease.InOutQuint);
        background.DOFade(1, fadeTime);
    }

}
