using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Tag da posi��o de iniciar
    const string TAG = "StartPoint";

    // O �ndice do menu de sele��o de fases  
    const int ID_MENU_LEVEL = 1;

    public static GameManager instance;

    //Ball Variables
    //private bool isBallDeath = false;
    //[SerializeField] private GameObject GO;
    public BaseBall ballObject;
    [SerializeField] private UIControl interfaceUI;

    public int kick = 1;
    [HideInInspector] public Transform pos;
    [SerializeField] public int ballNum = 2;
    [SerializeField] public int ballInGame = 0;
    public bool win;


    // Index das Fases
    public bool beginGame;

    AsyncOperation asyncUnload;

    private void Awake()
    {
        // Certifique-se de que n�o h� duplicatas e mantenha os dados quando mudar de fases
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
        //pos = GameObject.Find(TAG).GetComponent<Transform>();
    }


    //Passar o m�todo e procurar o objeto TEXT com o nome NCoins
    void Load(Scene scene, LoadSceneMode mode)
    {
        //if (IdLevel.instance.level != ID_MENU_LEVEL)
        if (SceneManager.GetActiveScene().name.Contains("Level_"))
        {
            pos = GameObject.Find(TAG).GetComponent<Transform>();
            //Instantiate(interfaceUI, new Vector2(pos.position.x, pos.position.y), Quaternion.identity, pos);
            StartGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        

        //Chama a tela de sele��o de bola
        if (IdLevel.instance.level >= 4)
        {
           asyncUnload = SceneManager.LoadSceneAsync("SelectBall", LoadSceneMode.Additive);
        }

         StartGame();
         ScoreManager.instance.GameStartScoreM();
    }

   

    // Update is called once per frame
    void Update()
    {
        if(ballObject)
        {

           BallBorn();
        }

        ScoreManager.instance.UpdateScore();
       UIManager.instance.UpdateUI();

        if (ballNum <= 0)
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

        // if(IdLevel.instance.level >= 3)
        // {
        if (ballNum > 0 && ballInGame == 0 && Camera.main.transform.position.x <= 0.05f)
        {
            Instantiate(ballObject, new Vector2(pos.position.x, pos.position.y), Quaternion.identity,pos);

            ballInGame += 1;
            kick = 0;
        }
        else
        {
            if (ballNum > 0 && ballInGame == 0)
            {
                Instantiate(ballObject, new Vector2(pos.position.x, pos.position.y), Quaternion.identity,pos);

                ballInGame++;
                kick = 0;
            }
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