using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    //Ball Variables
    [SerializeField]private GameObject ball;
    private int BallNum = 2;
    private bool IsBallDeath = false;
    private int BallInGame = 0;
    private Transform pos;
    const string TAG = "StartPoint";


    private void Awake()
    {
        // Certifique-se de que não há duplicatas e mantenha os dados quando mudar de fases
        if (Instance == null)
        {
            Instance = this;
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
        UIManager.Instance.UpdateUI();
        BallBorn();
    }

    void BallBorn()
    {
        if(BallNum > 0 && BallInGame == 0 )
        {
            Instantiate(ball, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
            BallInGame++;
        }
    }
}
