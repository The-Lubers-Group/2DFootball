using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    const string TAG_COIN = "NCoins";
    const string TAG_BALL = "NBall";
    
    public static UIManager Instance;
    private TMP_Text pointsUI;
    private TMP_Text ballUI;


    private void Awake()
    {
        // Certifique-se de que não há duplicatas e mantenha os dados quando mudar de fases
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;

    }

    //Passar o método e procurar o objeto TEXT com o nome NCoins
    void Load(Scene scene, LoadSceneMode mode)
    {
        pointsUI = GameObject.Find(TAG_COIN).GetComponent<TMP_Text> ();
        ballUI = GameObject.Find(TAG_BALL).GetComponent<TMP_Text> ();
    }

    // Atualizar o UItexto com a quantidade do moeda do jogador 
    public void UpdateUI()
    {
        pointsUI.text = ScoreManager.instance.coins.ToString();
        ballUI.text = GameManager.instance.ballNum.ToString();
    }
}
