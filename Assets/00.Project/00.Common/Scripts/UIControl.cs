using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    const string TAG_COIN = "NCoins";
    public static UIControl instance;


    [SerializeField] private TMP_Text pointsUI;
    [SerializeField] private TMP_Text ballUI;

    [SerializeField] private GameObject selectBallMenu;
    [SerializeField] private GameObject winGameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    // Calcular a quantidade de moeda do jogador
    protected int coinsNumBefore;
    protected int coinsNumAfter;
    protected int coinsNumResult;

    private void Awake()
    {
        /*
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        */
        //pointsUI = GameObject.Find(TAG_COIN).GetComponent<TMP_Text>();
        coinsNumBefore = PlayerPrefs.GetInt("saveCoin");
    }

    private void Start()
    {
        //LoadSelectBallMenu();
        //Carregar quantidade de moeda do jogador no come�o do jogo
        coinsNumAfter = ScoreManager.instance.coins;
        
        pointsUI.text = ScoreManager.instance.coins.ToString();
        ballUI.text = "x" + GameManager.instance.ballNum.ToString();
    }

    private void Update()
    {
        // Se o jogador morrer, as moedas que pegou na fase s�o descontadas.
        coinsNumResult = coinsNumAfter - coinsNumBefore;
    }

    public void LoadSelectBallMenu()
    {
        selectBallMenu.SetActive(true);
    }

    public void LoadWinGameMenu()
    {
        winGameMenu.SetActive(true);
    }
    public void LoadGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void LoadPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SceneNextLevel()
    {
        if (GameManager.instance.win)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            //Debug.Log(SceneManager.GetSceneByBuildIndex(nextSceneIndex).name.Contains("Level_"));
            //Debug.Log(SceneManager.GetSceneByBuildIndex(nextSceneIndex).path);
            SceneManager.LoadScene(nextSceneIndex);
            if (SceneManager.GetSceneByBuildIndex(nextSceneIndex).name.Contains("Level_"))
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(1);
            }

        }
    }

    public void SceneRetryLevel()
    {
        if (GameManager.instance.win == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Se o jogador morrer, as moedas que pegou na fase s�o descontadas.
            coinsNumResult = coinsNumAfter - coinsNumBefore;
            ScoreManager.instance.LoseCoins(coinsNumResult);
            coinsNumResult = 0;
        }
        else
        {
            // Se o jogador vencer, ele apenas recarrega a fase.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            coinsNumResult = 0;
        }
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
        LoadSelectBallMenu();
     
        /*
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
        LoadSelectBallMenu();
        */
    }


    public void SceneListLevels()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
