using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    const string TAG = "saveCoin";
    public int coins;


    private void Awake()
    {
        // Certifique-se de que não há duplicatas e mantenha os dados quando mudar de fases
        if (instance == null)
        {
            instance = new ScoreManager();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //Chamado no gerenciador do jogo 
    public void GameStartScoreM()
    {
        if(PlayerPrefs.HasKey(TAG))
        {
            coins = PlayerPrefs.GetInt(TAG);
        }
        else
        {
            coins = 100;
            PlayerPrefs.SetInt(TAG, coins);
        }

    }

    //Capaz de garantir que as informações sejam sempre exibidas
    public void UpdateScore()
    {
        coins = PlayerPrefs.GetInt(TAG);
    }

    //Quando estiver coletando as moedas
    public void CollectCoins(int coin)
    {
        coins += coin;
        SaveCoins(coins);
    }

    //Quando estiver comprando itens
    public void LoseCoins(int coin)
    {
        coins -= coin;
        SaveCoins(coins);
    }

    //Salvar os dados da moeda
    public void SaveCoins(int coin) 
    { 
        PlayerPrefs.SetInt(TAG, coin);
    }
}
