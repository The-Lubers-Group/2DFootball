using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    const string TAG_COIN = "NCoins";
    const string TAG_BALL = "NBall";

    const string TAG_LOSE_PANEL = "PopUp_Lose";
    const string TAG_Win_PANEL = "PopUp_Win";
    const string TAG_PAUSE_PANEL = "PopUP_Pause";
    
    public static UIManager instance;
    private TMP_Text pointsUI;
    private TMP_Text ballUI;

    private GameObject losePanel;
    private GameObject winPanel;
    private GameObject pausePanel;


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
        SwitchPanel();
    }

    //Passar o método e procurar o objeto TEXT com o nome NCoins
    void Load(Scene scene, LoadSceneMode mode)
    {
        pointsUI = GameObject.Find(TAG_COIN).GetComponent<TMP_Text> ();
        ballUI = GameObject.Find(TAG_BALL).GetComponent<TMP_Text> ();

        // Menus
        losePanel = GameObject.Find(TAG_LOSE_PANEL);
        winPanel = GameObject.Find(TAG_Win_PANEL);
        //pausePanel = GameObject.Find(TAG_PAUSE_PANEL);
    }

    // Atualizar o UItexto com a quantidade do moeda do jogador 
    public void UpdateUI()
    {
        pointsUI.text = ScoreManager.instance.coins.ToString();
        ballUI.text = GameManager.instance.ballNum.ToString();
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

    // Desativar o panel depois de pegar as informções
    IEnumerator temp()
    {
        yield return new WaitForSeconds(0.001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }
}

