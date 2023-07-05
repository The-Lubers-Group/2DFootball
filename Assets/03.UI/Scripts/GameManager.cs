using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Tag da posição de iniciar
    const string TAG = "StartPoint";

    // O índice do menu de seleção de fases  
    const int ID_MENU_LEVEL = 4;

    public static GameManager instance;

    //Ball Variables
    private bool isBallDeath = false;
    [SerializeField] private GameObject ball;

    public int kick = 1;
    public Transform pos;
    public int ballNum = 2;
    public int ballInGame = 0;
    public bool win;

    // Index das Fases
    //public int idLevel;
    public bool beginGame;

    private void Awake()
    {
        // Certifique-se de que não há duplicatas e mantenha os dados quando mudar de fases
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += Load;
    }

    //Passar o método e procurar o objeto TEXT com o nome NCoins
    void Load(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(IdLevel.instance.level);
        if(IdLevel.instance.level != ID_MENU_LEVEL)
        {
            pos = GameObject.Find(TAG).GetComponent<Transform>();
            StartGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.GameStartScoreM();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        BallBorn();

        if(ballNum <= 0)
        {
            GameOver();
        }

        if(win == true)
        {
            WinGame();
        }

    }

    // Cria o objeto bola no jogo
    void BallBorn()
    {
        if(ballNum > 0 && ballInGame == 0 )
        {
            Instantiate(ball, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
            ballInGame++;
            kick = 0;
        }
    }
    
    // O Player morreu e chama o panel de Game Over
    void GameOver()
    {
        UIManager.instance.GameOverUI();
        beginGame= false;
    }

    // O Player ganhou a fase
    void WinGame()
    {
        UIManager.instance.WinGameUI();
        beginGame = false;
    }

    //Start game
    void StartGame()
    {
        beginGame = true;
        ballNum = 2;
        ballInGame = 0;
        win = false;
        UIManager.instance.StartUI();
    }
}
