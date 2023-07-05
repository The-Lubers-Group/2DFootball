using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Tag dos labels
    const string TAG_COIN = "NCoins";
    const string TAG_BALL = "NBall";

    // Tag dos Modal
    const string TAG_LOSE_PANEL = "PopUp_Lose";
    const string TAG_Win_PANEL = "PopUp_Win";
    const string TAG_PAUSE_PANEL = "PopUP_Pause";
    
    // Tag dos botões de Pause
    const string TAG_PAUSE_BTN = "PauseButton";
    const string TAG_PAUSE_BTN_RETURN = "BtnResume";

    // Tag dos botões de Game Over
    const string TAG_GAMEOVER_BTN_MENU = "BtnMenu";
    const string TAG_GAMEOVER_BTN_RETRY = "BtnRetry";

    // Tag das Animações
    const string TAG_PAUSE_ANIM = "PauseAnim";
    const string TAG_PAUSE_CLOSE_ANIM = "PauseCloseAnim";

    // Tag da Moeda
    const string TAG_PLAYER_COINS = "saveCoin";

    // Variáveis para gerenciar moedas
    public static UIManager instance;
    private TMP_Text pointsUI;
    private TMP_Text ballUI;


    // GameObject dos Modais Panel
    private GameObject losePanel;
    private GameObject winPanel;
    private GameObject pausePanel;

    // Button ref pause
    private Button pauseBtn;
    private Button pauseBtnReturn;

    // Button ref btn game over
    private Button menuBtn;
    private Button retryBtn;

    // Calcular a quantidade de moeda do jogador
    public int coinsNumBefore;
    public int coinsNumAfter;
    public int coinsNumResult;


    private void Awake()
    {
        // Certifique-se de que não há duplicatas e mantenha os dados quando mudar de fases
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;
    }

    //Passar o método e procurar o objeto TEXT com o nome NCoins
    void Load(Scene scene, LoadSceneMode mode)
    {
        pointsUI = GameObject.Find(TAG_COIN).GetComponent<TMP_Text> ();
        ballUI = GameObject.Find(TAG_BALL).GetComponent<TMP_Text> ();

        // Menus
        winPanel = GameObject.Find(TAG_Win_PANEL);
        losePanel = GameObject.Find(TAG_LOSE_PANEL);
        pausePanel = GameObject.Find(TAG_PAUSE_PANEL);

        // Botões
        pauseBtn = GameObject.Find(TAG_PAUSE_BTN).GetComponent<Button>();
        pauseBtnReturn = GameObject.Find(TAG_PAUSE_BTN_RETURN).GetComponent<Button>();

        menuBtn = GameObject.Find(TAG_GAMEOVER_BTN_MENU).GetComponent<Button>();
        retryBtn = GameObject.Find(TAG_GAMEOVER_BTN_RETRY).GetComponent<Button>();


        pauseBtn.onClick.AddListener (Pause);
        pauseBtnReturn.onClick.AddListener (PauseReturn);

        retryBtn.onClick.AddListener(PlayAgain);

        coinsNumBefore = PlayerPrefs.GetInt(TAG_PLAYER_COINS);
    }

    public void StartUI()
    {
        SwitchPanel();
    }

    // Atualizar o UItexto com a quantidade do moeda do jogador 
    public void UpdateUI()
    {
        pointsUI.text = ScoreManager.instance.coins.ToString();
        ballUI.text = GameManager.instance.ballNum.ToString();

        //Carregar quantidade de moeda do jogador
        coinsNumAfter = ScoreManager.instance.coins;
    }

    // O Player morreu e chama o panel de Game Over
    public void GameOverUI()
    {
        losePanel.SetActive(true);
    }

    // O Player ganhou a fase
    public void WinGameUI()
    {
        winPanel.SetActive(true);
    }

    // Ativa e desliga o Panel de Game Over
    void SwitchPanel()
    {
        StartCoroutine(temp());
    }

    // Pausa o jogo e chama o menu de pause
    void Pause()
    {
        pausePanel.SetActive(true);
        pausePanel.GetComponent<Animator>().Play(TAG_PAUSE_ANIM);
        Time.timeScale = 0;
    }

    // Retorna o jogo e chama o menu de pause
    void PauseReturn()
    {
        pausePanel.GetComponent<Animator>().Play(TAG_PAUSE_CLOSE_ANIM);
        Time.timeScale = 1;
        StartCoroutine(WaitPause());
    }

    // Desativar o panel depois de pegar as informções
    IEnumerator WaitPause()
    {
        yield return new WaitForSeconds(0.8f);
        pausePanel.SetActive(false);
    }

    // Desativar o panel depois de pegar as informções
    IEnumerator temp()
    {
        yield return new WaitForSeconds(0.001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    // Jogar novamente - a fase usando o id 
    void PlayAgain()
    {
        SceneManager.LoadScene(GameManager.instance.idLevel);

        // Se o jogador morrer, as moedas que pegou na fase são descontadas.
        coinsNumResult = coinsNumAfter - coinsNumBefore;
        ScoreManager.instance.LoseCoins(coinsNumResult);
        coinsNumResult = 0;
    }
}

