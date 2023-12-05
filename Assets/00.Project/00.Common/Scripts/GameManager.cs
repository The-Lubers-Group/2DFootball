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
    [SerializeField] public BaseBall ballObject;

    public int kick = 1;
    [SerializeField] public Transform startPoint;
    [SerializeField] public int maxAttempts = 2;
    [SerializeField] public int attempts;
    [SerializeField] public int ballInGame = 0;
    public bool win;

    private UIControl uIControl;

    // Index das Fases
    public bool beginGame;
    AsyncOperation asyncUnload;
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    //[SerializeField] private BallSelection ballSelection;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        attempts = maxAttempts;
        SelectBall();
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
    }

    // Cria o objeto bola no jogo
    public void AddBall()
    {
        if (!startPoint)
        {
            startPoint = GameObject.Find(TAG).GetComponent<Transform>();
        }

        if (startPoint)
        {
            //Debug.Log("ballObject 1 : " + ballObject);

            if (attempts > 0 && ballInGame == 0 && Camera.main.transform.position.x <= 0.05f)
            {
                ballObject = Instantiate(ballObject, new Vector2(startPoint.position.x, startPoint.position.y), Quaternion.identity, startPoint);
                //Instantiate(ballObject, new Vector2(startPoint.position.x, startPoint.position.y), Quaternion.identity, startPoint);

                ballInGame = 1;
                kick = 0;
            }
            //Debug.Log("ballObject depois do IF : " + ballObject);
            else
            {
                if (attempts > 0 && ballInGame == 0)
                {
                    ballObject = Instantiate(ballObject, new Vector2(startPoint.position.x, startPoint.position.y), Quaternion.identity, startPoint);
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

        if (!uIControl)
        {
            if (GameObject.FindObjectOfType<UIControl>())
            {
                uIControl = GameObject.FindObjectOfType<UIControl>();
                uIControl.LoadSelectBallMenu();
            }
        }

        if (uIControl)
        {
            uIControl.LoadSelectBallMenu();

        }
    }

    //Start game
    public void StartGame(BaseBall ball)
    {
        ballObject = ball;

        //SelectBall();

        beginGame = true;
        attempts = maxAttempts;
        ballInGame = 0;
        win = false;

        AddBall();
    }


    // O Player morreu e chama o panel de Game Over
    void GameOver()
    {
        uIControl.LoadGameOverMenu();
        beginGame = false;
        ballInGame = 0;
    }


    public void RespawnBall()
    {
        //Destroy(this.ballObject.gameObject);

        attempts -= 1;
        ballInGame = 0;

        if (attempts > 0)
        {
            //print("respawn");
            AddBall();
            //ballObject.transform.position = new Vector2(startPoint.position.x, startPoint.position.y);
            //ballObject.ballRigdbody2D.velocity = Vector3.zero;
            //ballObject.ballRigdbody2D.AddForce(new Vector2(0, 0));

        }
        if (attempts <= 0)
        {
            GameOver();
        }
    }

}