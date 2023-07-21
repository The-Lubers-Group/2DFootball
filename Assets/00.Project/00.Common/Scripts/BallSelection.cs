using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallSelection : MonoBehaviour
{
    public static BallSelection instance;

    [SerializeField] private List<BallObjectSO> ballList = new List<BallObjectSO>();
    
    [SerializeField] private Transform content;
    [SerializeField] private GameObject cardItens;

    [SerializeField] private GameObject ballSelectionPanel;
    [SerializeField] private BallManager ballManager;

    [SerializeField] private Transform ballNoJogo;

    public BallObjectSO selectBall;

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
    }

    void Start()
    {
        //ballManager =  GetComponent<BallManager>();


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
        foreach (BallObjectSO ball in ballList)
        {
            if (ball.WasBought)
            {
                GameObject cardItem = Instantiate(cardItens) as GameObject;
                cardItem.transform.SetParent(content, false);
                CardItem item = cardItem.GetComponent<CardItem>();
                item.ballId = ball.ballId;
                item.ballIcon.sprite = ball.imgIcon;
                item.ballName.text = ball.ballName;
                item.ballPrice.text = ball.ballPrice.ToString();

                //Debug.Log(ball);
                //item.btnBuy.onClick.AddListener(() => ClickLevel(ball));
                item.btnBuy.onClick.AddListener(() => ClickLevel(ball));


            }
        }
    }

    // Função responsável por enviar o jogador para o nível selecionado
    void ClickLevel(BallObjectSO ball)
    {
        Time.timeScale = 1;
        selectBall = ball;
        //Debug.Log(selectBall);




        if (selectBall)
        {
            Debug.Log(selectBall);
            //ballManager = GetComponent<BallManager>();
            ballNoJogo = GameObject.Find("BallPrefabs(Clone)").GetComponent<Transform>();
            Debug.Log(ballNoJogo);
            //ballManager = GetComponent<BallManager>();


            ballManager = GameObject.Find("BallPrefabs(Clone)").GetComponent<BallManager>();
            Debug.Log(ballManager);

            ballManager.BallSO = selectBall;
            Debug.Log(ballManager.BallSO);

        }
















        ballSelectionPanel.SetActive(false);
        //Destroy(gameObject);

        //ballManager.BallSO = ball.GetComponent<BallObjectSO>;
        //Debug.Log(ballManager);
        // BallManager balls;
        // balls.imgIcon = s
        //SceneManager.LoadScene(level);
    }

    public BallObjectSO GetSelectBall()
    {
        return selectBall;
    }

}

