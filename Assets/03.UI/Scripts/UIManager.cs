using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMP_Text pointsUI;
    const string TAG = "NCoins";


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
        pointsUI = GameObject.Find(TAG).GetComponent<TMP_Text> ();
    }

    // Atualizar o UItexto com a quantidade do moeda do jogador 
    public void UpdateUI()
    {
        pointsUI.text = ScoreManager.instance.coins.ToString();
    }
}
