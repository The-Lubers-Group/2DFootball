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
    // Tag da posição de iniciar
    const string TAG = "StartPoint";

    // O índice do menu de seleção de fases  
    const int ID_MENU_LEVEL = 1;

    public static GameManager instance;

    //Ball Variables
    //private bool isBallDeath = false;
    //[SerializeField] private GameObject GO;
    public BaseBall ballObject;

    public int kick = 1;
    public Transform pos;
    [SerializeField] public int ballNum = 2;
    [SerializeField] public int ballInGame = 0;
    public bool win;

    CardItem cardItem;

    // Index das Fases
    public bool beginGame;

    //List of the scenes to load from Main Menu
    //List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    AsyncOperation asyncUnload;


    [SerializeField] private BallSelection ballSelection;

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
        pos = GameObject.Find(TAG).GetComponent<Transform>();
    }

    //Passar o método e procurar o objeto TEXT com o nome NCoins
    void Load(Scene scene, LoadSceneMode mode)
    {
        if (IdLevel.instance.level != ID_MENU_LEVEL)
        {
            pos = GameObject.Find(TAG).GetComponent<Transform>();
            StartGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        //Chama a tela de seleção de bola
        if (IdLevel.instance.level >= 4)
        {
           asyncUnload = SceneManager.LoadSceneAsync("SelectBall", LoadSceneMode.Additive);

            //SceneManager.LoadSceneAsync("SelectBall", LoadSceneMode.Additive);
            //asyncUnload = SceneManager.UnloadSceneAsync("SelectBall");

           // Instantiate(BallSelection.instance, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);

            //scenesToLoad.Add(SceneManager.LoadSceneAsync("SelectBall", LoadSceneMode.Additive));

            //scenesToLoad.Remove(SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("SelectBall")));

            //AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("game"));

            //scenesToLoad.UnloadSceneAsync(GameObject, "SelectBall");
            //UnloadSceneOptions
            //ballSelection = GameObject.Find("BallSelectionPanel").GetComponent<BallSelection>();



          





            
        }
       // cardItem = GameObject.Find("CardItemS(Clone)").GetComponent<CardItem>();

        //ballObject = GetComponent<BaseBall>();

        /*
        if(ballSelection.selectBall)
        {
            Instantiate(ballSelection.selectBall, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);

        }
        */


         StartGame();
         ScoreManager.instance.GameStartScoreM();
    }

   

    // Update is called once per frame
    void Update()
    {
        /*
        if (asyncUnload.isDone)
        {
            Debug.Log("asyncUnload.isDone");
        }
        */


        /*
        if(BallSelection.instance)
        {
            Instantiate(BallSelection.instance.selectBall, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);

        }
        else
        {
            Debug.Log(" BallSelection.instance.selectBall -->" + BallSelection.instance);

        }

        */


        /*
        if (BallSelection.instance.selectBall)
        {
            Debug.Log("iiiiiiiiii ");
            //Debug.Log("A bola selecionada foi: " + ballSelection.selectBall.ballName);

        }
        else
        {
            Debug.Log(ballSelection);
        }
        */

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
           // ballObject.transform.parent = gameObject.transform;

            ballInGame += 1;
            kick = 0;
        }
        else
        {
            if (ballNum > 0 && ballInGame == 0)
            {
                Instantiate(ballObject, new Vector2(pos.position.x, pos.position.y), Quaternion.identity,pos);
                //ballObject.transform.parent = gameObject.transform;

                ballInGame++;
                kick = 0;
            }
        }

        // }


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
