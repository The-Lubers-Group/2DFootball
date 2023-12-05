using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.SceneTemplate;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Tag da posição de iniciar
    const string TAG = "StartPoint";

    // O índice do menu de seleção de fases  
    const int ID_MENU_LEVEL = 1;

    public static GameManager instance;

    //Ball Variables
    //private bool isBallDeath = false;
    //[SerializeField] private GameObject GO;
    [SerializeField] public BaseBall ballObject;

    public int kick = 1;
    [SerializeField] public Transform startPoint;
    [SerializeField] public int maxAttempts = 2;
    [SerializeField] public int attempts;
    [SerializeField] public int ballInGame = 0;
    public bool win;

    private UIControl uIControl;
    //[SerializeField] private UIControl interfaceUI;

    // Index das Fases
    public bool beginGame;

    AsyncOperation asyncUnload;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    [SerializeField] private BallSelection ballSelection;


    //Passar o método e procurar o objeto TEXT com o nome NCoins
   
    /*
    void Load(Scene scene, LoadSceneMode mode)
    {
        //if (IdLevel.instance.level != ID_MENU_LEVEL)
        if (SceneManager.GetActiveScene().name.Contains("Level_"))
        {
           
            //Instantiate(interfaceUI, new Vector2(pos.position.x, pos.position.y), Quaternion.identity, pos);
            //StartGame();
        }
    }
    */


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
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("LoadingScene"));
        //SceneManager.sceneLoaded += Load;
        //pos = GameObject.Find(TAG).GetComponent<Transform>();
        //uIControl.LoadSelectBallMenu();
    }

    // Start is called before the first frame update
    void Start()
    {
        attempts = maxAttempts;
        
        SelectBall();

        //print("void Start()");
        //print("sendo chamado" );
        //StartGame();
        //if (ballObject == null && ballNum > 0)
        //print("ballNum: " + ballNum);
        //print("ballObject: " + ballObject);
        //print("ballInGame: " + ballInGame);
        //CreateBall();
        //if (ballInGame == 0 && ballObject)
        //{
        //}
    }

    // Update is called once per frame
    void Update()
    {

        if (attempts > 0 && ballObject)
        {
            ScoreManager.instance.UpdateScore();
        }

        if (win == true)
        {
            WinGame();
        }




        /*
        if (!ballObject)
        {
            SelectBall();
        }
        */
        /*
        if (attempts <= 0)
        {
            GameOver();
        }
        */

        //Debug.Log(ballObject);
        //if (!uIControl)
        //{   
        // uIControl = GameObject.FindObjectOfType<UIControl>();
        //}

        //if (ballObject == null && ballNum > 0)
        //{
        //uIControl.LoadSelectBallMenu();
        //}

        /*
        if(ballInGame == 0 && ballNum > 0 && ballObject)
        {
            //BallBorn();
            CreateBall();
            ballInGame = 1;
        }
        */


        //if (ballNum > 0 && ballObject)
        //{
        //BallBorn();
        //}




        /*
        if (ballObject)
        {
            BallBorn();
        }
        */
    }

    // Cria o objeto bola no jogo
    public void CreateBall(BaseBall ball)
    {
        if (!startPoint)
        {
            startPoint = GameObject.Find(TAG).GetComponent<Transform>();
        }

        if (startPoint)
        {
            if (attempts > 0 && ballInGame == 0 && Camera.main.transform.position.x <= 0.05f)
            {
                ballObject = Instantiate(ball, new Vector2(startPoint.position.x, startPoint.position.y), Quaternion.identity, startPoint);
                ballInGame = 1;
                kick = 0;
            }
            else
            {
                if (attempts > 0 && ballInGame == 0)
                {
                    ballObject = Instantiate(ball, new Vector2(startPoint.position.x, startPoint.position.y), Quaternion.identity, startPoint);
                    ballInGame = 1;
                    kick = 0;
                }
            }
        }

       
    }


    
    // O Player ganhou a fase
    void WinGame()
    {
        //UIManager.instance.WinGameUI();
        uIControl.LoadWinGameMenu();
        beginGame = false;
    }

    public void SelectBall()
    {
       
        if(!uIControl)
        {
            if (GameObject.FindObjectOfType<UIControl>())
            {
                uIControl = GameObject.FindObjectOfType<UIControl>();
                uIControl.LoadSelectBallMenu();
            }
        }

        if(uIControl)
        {
            uIControl.LoadSelectBallMenu();

        }
    }

    //Start game
    public void StartGame(BaseBall ball)
    {
        beginGame = true;
        attempts = maxAttempts;
        ballInGame = 0;
        win = false;
        
        CreateBall(ball);
        
        
        
        //UIManager.instance.StartUI();
        // Criar a UI Control
        /*
        print("chegou aqui");
        print("startPoint: " + startPoint);
        print("attempts: " + attempts);
        print("maxAttempts: " + maxAttempts);
        print("ballInGame: " + ballInGame);
        */
    }


    // O Player morreu e chama o panel de Game Over
    void GameOver()
    {
        //Destroy(this.ballObject.gameObject);
        //Destroy(this.ballObject);
        //Debug.Log("GameOver");
        //UIManager.instance.GameOverUI();
        uIControl.LoadGameOverMenu();
        beginGame = false;
        ballInGame = 0;
        //ballObject.ballRigdbody2D.velocity = Vector3.zero;

    }


    public void RespawnBall(BaseBall ball)
    {
        attempts -= 1;
        ballInGame = 0;
        //ScoreManager.instance.UpdateScore();

        //win = false;

        if (attempts > 0)
        {
            CreateBall(ball);
            //ballObject.transform.position = new Vector2(startPoint.position.x, startPoint.position.y);
            //ballObject.ballRigdbody2D.velocity = Vector3.zero;
            //ballObject.ballRigdbody2D.AddForce(new Vector2(0, 0));
        }
        if(attempts <= 0 )
        {
            GameOver();
        }
        //BallBorn();
    }

}
