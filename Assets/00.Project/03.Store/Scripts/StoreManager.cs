using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    
    //public List<BallManager> ballList = new List<BallManager>();
    public List<BallObjectSO> ballList = new List<BallObjectSO>();
    
    public GameObject cardItens;
    public Transform content;
    

    /*
    public int ballID;
    public string nameIcon;
    public int ballPrice;
    public bool WasBought;
    */

    private void Awake()
    {
        if(instance = null)
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
        
        foreach(BallObjectSO ball in ballList)
        {
            GameObject cardItem = Instantiate(cardItens) as GameObject;
            
            cardItem.transform.SetParent(content, false);
            
            CardItem item = cardItem.GetComponent<CardItem>();

            item.ballId = ball.ballId;
            item.ballIcon.sprite = ball.imgIcon;
            item.ballName.text = ball.ballName;
            item.ballPrice.text = ball.ballPrice.ToString();

            if(ball.WasBought)
            {
                item.btnBuy.interactable = false;
            }
        }
        
    }
}



[System.Serializable]
public class Slot
{
    public BallObjectSO ball;
}