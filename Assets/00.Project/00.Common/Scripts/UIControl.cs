using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    const string TAG_COIN = "NCoins";
    private TMP_Text pointsUI;

    [SerializeField] private GameObject selectBallMenu;
    [SerializeField] private GameObject WinGameMenu;

    private void Awake()
    {
        pointsUI = GameObject.Find(TAG_COIN).GetComponent<TMP_Text>();
    }
    public void LoadSelectBallMenu()
    {
        selectBallMenu.SetActive(true);
    }

    public void LoadWinGameMenu()
    {
        WinGameMenu.SetActive(true);
    }
}
