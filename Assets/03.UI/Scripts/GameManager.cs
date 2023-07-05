using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const string TAG = "StartPoint";

    public static GameManager instance;

    //Ball Variables
    private bool isBallDeath = false;
    [SerializeField] private GameObject ball;

    public int kick = 1;
    public Transform pos;
    public int ballNum = 2;
    public int ballInGame = 0;

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
    }

    //Passar o método e procurar o objeto TEXT com o nome NCoins
    void Load(Scene scene, LoadSceneMode mode)
    {
        pos = GameObject.Find(TAG).GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.GameStartScoreM();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        BallBorn();

        if(ballNum <= 0)
        {
            GameOver();
        }

    }

    // Cria o objeto bola no jogo
    void BallBorn()
    {
        if(ballNum > 0 && ballInGame == 0 )
        {
            Instantiate(ball, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
            ballInGame++;
            kick = 0;
        }
    }
    
    // O Player morreu e chama o panel de Game Over
    void GameOver()
    {
        UIManager.instance.GameOverUI();
    }
}
