using UnityEngine;
using UnityEngine.UI;
using DFTGames.Localization;
using NaughtyAttributes;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Public Methods

    public void SetEnglish()
    {
        Localize.SetCurrentLanguage(SystemLanguage.English);
        LocalizeImage.SetCurrentLanguage();
    }

    public void SetItalian()
    {
        Localize.SetCurrentLanguage(SystemLanguage.Italian);
        LocalizeImage.SetCurrentLanguage();
    }

    public void SetJapanese()
    {
        Localize.SetCurrentLanguage(SystemLanguage.Japanese);
        LocalizeImage.SetCurrentLanguage();
    }

    #endregion Public Methods
    
    protected UIManager uiManager;

    [Header("Atributos do Menu")]
    [Space(5)]
    
    [Header("Painel")]
    [Label("Painel do Menu")]
    [SerializeField] protected RectTransform rectTransform;

    [Label("Image do background")]
    [SerializeField] protected Image background;

    [Label("Tempo de transição")]
    [SerializeField] protected float fadeTime = 2;

    [Space(5)]
    [Label("Texto: Moedas ganhas")]
    [SerializeField] protected TextMeshProUGUI myPointsText;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        if (uiManager != null)
        {
            Destroy(uiManager.gameObject);
        }
        PanelFadeIn();
    }


   


    public virtual void PanelFadeIn() { }
    public virtual void PanelFadeOut() { }
}
