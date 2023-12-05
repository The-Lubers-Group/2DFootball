using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.ShaderGraph;
//using UnityEditor.ShaderGraph;
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
        coinsNumBefore = PlayerPrefs.GetInt("saveCoin");
    }

    private void Start()
    {

        LoadSelectBallMenu();
        //Carregar quantidade de moeda do jogador no começo do jogo
        coinsNumAfter = ScoreManager.instance.coins;
        winGameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        pointsUI.text = ScoreManager.instance.coins.ToString();
        ballUI.text = "x" + GameManager.instance.attempts.ToString();

        // Se o jogador morrer, as moedas que pegou na fase são descontadas.
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
        SceneManager.LoadScene(1);

        /*


        if (GameManager.instance.win)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);


            if (nextSceneIndex == 9)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
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

        */
    }

    public void SceneRetryLevel()
    {
        SceneManager.LoadScene(1);
        /*
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
        if(GameManager.instance.win)
        {
            // Se o jogador vencer, ele apenas recarrega a fase.
            coinsNumResult = 0;
        }
        else
        {
            // Se o jogador morrer, as moedas que pegou na fase são descontadas.
            coinsNumResult = coinsNumAfter - coinsNumBefore;
            ScoreManager.instance.LoseCoins(coinsNumResult);
            coinsNumResult = 0;
        }
        

        //pauseMenu.SetActive(false);
        //selectBallMenu.SetActive(true);
        GameManager.instance.attempts = GameManager.instance.maxAttempts;
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
