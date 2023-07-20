using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallSelection : MonoBehaviour
{
    public static BallSelection instance;

    public List<BallObjectSO> ballList = new List<BallObjectSO>();

    public GameObject cardItens;
    public Transform content;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        FillList();
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
            }
        }
    }
}

