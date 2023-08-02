using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // O índice do menu de seleção de fases  
    const int ID_START_LEVEL = 0;
    const int ID_MENU_LEVEL = 1;
    const int ID_STORE_LEVEL = 2;

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
    const string TAG_PAUSE_BTN_MENU = "BtnMenuPause";
    const string TAG_PAUSE_BTN_RETRY = "BtnRetryPause";

    // Tag dos botões de Game Over
    const string TAG_GAMEOVER_BTN_MENU = "BtnMenuGameOver";
    const string TAG_GAMEOVER_BTN_RETRY = "BtnRetryGameOver";

    // Tag dos botões de WIN
    const string TAG_WIN_BTN_NEXT = "BtnNextWin";
    const string TAG_WIN_BTN_RETRY = "BtnRetryWin";
    const string TAG_WIN_BTN_MENU = "BtnMenuWin";

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
    private Button menuBtnPause;
    private Button retryBtnPause;

    // Button ref btn game over
    private Button menuBtnGameOver;
    private Button retryBtnGameOver;

    // Button ref btn win
    private Button levelBtnWin;
    private Button retryBtnWin;
    private Button nextBtnWin;

    // Calcular a quantidade de moeda do jogador
    public int coinsNumBefore;
    public int coinsNumAfter;
    public int coinsNumResult;


    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();


    private void Awake()
    {
        // Certifique-se de que não há duplicatas e mantenha os dados quando mudar de fases
       /*
        if (SceneManager.GetActiveScene().name == "WinGameUI")
        {
            Destroy(gameObject);
        }
       */

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;
        //LoadData();
    }

    //Passar o método e procurar o objeto TEXT com o nome NCoins
    void Load(Scene scene, LoadSceneMode mode)
    {
        //LoadData();
    }

    void LoadData()
    {

        //if (IdLevel.instance.level != ID_START_LEVEL && IdLevel.instance.level != ID_MENU_LEVEL && IdLevel.instance.level != ID_STORE_LEVEL)
        if (SceneManager.GetActiveScene().name.Contains("Level_"))
        {
            pointsUI = GameObject.Find(TAG_COIN).GetComponent<TMP_Text>();
            ballUI = GameObject.Find(TAG_BALL).GetComponent<TMP_Text>();

            // Menus
            winPanel = GameObject.Find(TAG_Win_PANEL);
            losePanel = GameObject.Find(TAG_LOSE_PANEL);
            pausePanel = GameObject.Find(TAG_PAUSE_PANEL);

            // Botões
            pauseBtn = GameObject.Find(TAG_PAUSE_BTN).GetComponent<Button>();
            pauseBtnReturn = GameObject.Find(TAG_PAUSE_BTN_RETURN).GetComponent<Button>();
            menuBtnPause = GameObject.Find(TAG_PAUSE_BTN_MENU).GetComponent<Button>();
            retryBtnPause = GameObject.Find(TAG_PAUSE_BTN_RETRY).GetComponent<Button>();

            menuBtnGameOver = GameObject.Find(TAG_GAMEOVER_BTN_MENU).GetComponent<Button>();
            retryBtnGameOver = GameObject.Find(TAG_GAMEOVER_BTN_RETRY).GetComponent<Button>();

            nextBtnWin = GameObject.Find(TAG_WIN_BTN_NEXT).GetComponent<Button>();
            retryBtnWin = GameObject.Find(TAG_WIN_BTN_RETRY).GetComponent<Button>();
            levelBtnWin = GameObject.Find(TAG_WIN_BTN_MENU).GetComponent<Button>();



            pauseBtn.onClick.AddListener(Pause);
            pauseBtnReturn.onClick.AddListener(PauseReturn);
            retryBtnPause.onClick.AddListener(PlayAgain);
            menuBtnPause.onClick.AddListener(Levels);

            retryBtnGameOver.onClick.AddListener(PlayAgain);
            menuBtnGameOver.onClick.AddListener(Levels);

            nextBtnWin.onClick.AddListener(NextLevel);
            retryBtnWin.onClick.AddListener(PlayAgain);
            levelBtnWin.onClick.AddListener(Levels);

            coinsNumBefore = PlayerPrefs.GetInt(TAG_PLAYER_COINS);
        }
    }

    public void StartUI()
    {
        SwitchPanel();
    }

    // Atualizar o UItexto com a quantidade do moeda do jogador 
    public void UpdateUI()
    {
        //pointsUI.text = ScoreManager.instance.coins.ToString();
        //ballUI.text = GameManager.instance.ballNum.ToString();

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
        //winPanel.SetActive(true);
        //
        SceneManager.LoadSceneAsync("WinGameUI");
    }

    // Ativa e desliga o Panel de Game Over
    void SwitchPanel()
    {
        //StartCoroutine(temp());
    }

    // Pausa o jogo e chama o menu de pause
    void Pause()
    {
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("MenuWinUI", LoadSceneMode.Additive));
        //SceneManager.LoadScene("MenuWinUI");
        //SceneManager.LoadScene("WinModal");
        //SceneManager.LoadScene(ID_MENU_LEVEL);

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
        if(GameManager.instance.win == false)
        {
            SceneManager.LoadScene(IdLevel.instance.level);
            // Se o jogador morrer, as moedas que pegou na fase são descontadas.
            coinsNumResult = coinsNumAfter - coinsNumBefore;
            ScoreManager.instance.LoseCoins(coinsNumResult);
            coinsNumResult = 0;
        } else
        {
            // Se o jogador vencer, ele apenas recarrega a fase.
            SceneManager.LoadScene(IdLevel.instance.level);
            coinsNumResult = 0;
        }
    }

    // Chama o menu de seleção de fases  
    void Levels()
    {
        if(GameManager.instance.win == false)
        {
            coinsNumResult = coinsNumAfter - coinsNumBefore;
            ScoreManager.instance.LoseCoins(coinsNumResult);
            coinsNumResult = 0;
            SceneManager.LoadScene(ID_MENU_LEVEL);
        }
        else
        {
            coinsNumResult = 0;
            SceneManager.LoadScene(ID_MENU_LEVEL);
        }
    }

    // Proxima fase
    void NextLevel()
    {
        if(GameManager.instance.win)
        {
            int temp = IdLevel.instance.level +1;
            SceneManager.LoadScene (temp);
        }
    }

    void SelectBall()
    {
        SceneManager.LoadSceneAsync("SelectBall", LoadSceneMode.Additive);
    }
}

