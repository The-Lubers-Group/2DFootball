using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BallSelection : MonoBehaviour
{
    public static BallSelection instance;

    //[SerializeField] private List<BallObjectSO> ballList = new List<BallObjectSO>();
    [SerializeField] private List<BaseBall> ballList = new List<BaseBall>();


    [SerializeField] private GameManager gameManager;


    [SerializeField] private Transform content;
    [SerializeField] private GameObject cardItens;

    [SerializeField] private GameObject ballSelectionPanel;
   

    private BaseBall ballManager;
    private Transform ballNoJogo;
    [HideInInspector] public BaseBall selectBall;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }

       // DontDestroyOnLoad(selectBall);
    }

    void Start()
    {
        //ballManager =  GetComponent<BallManager>();

        gameManager = GameObject.Find("GameManager(Clone)").GetComponent<GameManager>();

       // Debug.Log(ballManager);
        
        FillList();



        //ballManager = GetComponent<BallManager>();
        //ballList
    }

    void Update()
    {
       
    }



    void FillList()
    {
        Time.timeScale = 0;
        foreach (BaseBall ball in ballList)
        {
            if (ball.WasBought)
            {
                GameObject cardsItem = Instantiate(cardItens) as GameObject;
                cardsItem.transform.SetParent(content, false);
                CardItem cardItem = cardsItem.GetComponent<CardItem>();


                cardItem.ballId = ball.ballID;
                cardItem.ballIcon.sprite = ball.icon;
                cardItem.ballName.text = ball.ballName;
                cardItem.ballPrice.text = ball.ballPrice.ToString();

                cardItem.btnBuy.onClick.AddListener(() => ClickLevel(ball));


                /*
                item.ballId = ball.ballId;
                item.ballIcon.sprite = ball.imgIcon;
                item.ballName.text = ball.ballName;
                item.ballPrice.text = ball.ballPrice.ToString();

                //Debug.Log(ball);
                //item.btnBuy.onClick.AddListener(() => ClickLevel(ball));
                item.btnBuy.onClick.AddListener(() => ClickLevel(ball));

                */


            }
        }
    }

    // Função responsável por enviar o jogador para o nível selecionado
    void ClickLevel(BaseBall ball)
    {
        Time.timeScale = 1;
        selectBall = ball;
        gameManager.ballObject = ball;



        //PlayerPrefs.("ball", ball);

        //Debug.Log(selectBall);




        if (selectBall)
        {
            //Debug.Log("A bola selecionada foi: "+ selectBall.ballName);
            //ballNoJogo = GameObject.Find("BallPrefabs(Clone)").GetComponent<Transform>();



            /*
            Debug.Log(selectBall);
            //ballManager = GetComponent<BallManager>();
            ballNoJogo = GameObject.Find("BallPrefabs(Clone)").GetComponent<Transform>();
            Debug.Log(ballNoJogo);
            //ballManager = GetComponent<BallManager>();


            ballManager = GameObject.Find("BallPrefabs(Clone)").GetComponent<BaseBall>();
            Debug.Log(ballManager);

            ballManager.BallSO = selectBall;
            Debug.Log(ballManager.BallSO);

            */





        }














       SceneManager.UnloadSceneAsync("SelectBall");

        //ballSelectionPanel.SetActive(false);
        //Destroy(gameObject);

        //ballManager.BallSO = ball.GetComponent<BallObjectSO>;
        //Debug.Log(ballManager);
        // BallManager balls;
        // balls.imgIcon = s
        //SceneManager.LoadScene(level);
    }

    public BaseBall GetSelectBall()
    {
        return selectBall;
    }

}

