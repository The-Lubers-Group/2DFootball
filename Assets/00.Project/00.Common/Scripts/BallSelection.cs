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

    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject cardItens;
    [SerializeField] private GameObject ballSelectionPanel;
    [SerializeReference] private List<BaseBall> ballList = new List<BaseBall>();
   

    private BaseBall ballManager;
    private Transform ballNoJogo;
    [SerializeField] public BaseBall selectBall;

    /*
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
    */

    void Start()
    {
        //gameManager = GameObject.Find("GameManager(Clone)").GetComponent<GameManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();


        FillList();
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
            }
        }
    }

    // Função responsável por enviar o jogador para o nível selecionado
    void ClickLevel(BaseBall ball)
    {
        Time.timeScale = 1;
        selectBall = ball;

        GameObject.FindObjectOfType<GameManager>().ballObject = ball;
        //gameManager.ballObject = ball;
        
        //Debug.Log(gameManager.ballObject);
        //Debug.Log(ball);


        //GameObject.setV
        this.gameObject.SetActive(false);

        //SceneManager.UnloadSceneAsync("SelectBall");
    }

    public BaseBall GetSelectBall()
    {
        return selectBall;
    }

}

