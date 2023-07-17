using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{

    public BallData BallSO;

    // TAG
    const string TAG_ARROW = "Arrow";
    const string TAG_GI = "GameInput";
    const string TAG_PS = "StartPoint";

    const string TAG_LW = "LeftWall";
    const string TAG_RW = "RightWall";

    const string TAG_HIT = "hit";
    const string TAG_WIN = "win";

    // Rotation
    //private Transform posStart;
    private GameObject arrowGo;
    private GameInput gameInput;

    public float zRotate;
    public bool shoot = false;

    // Force 
    [SerializeField] private float force = 0;
    private GameObject arrow2IMG;

    private Rigidbody2D ball;

    // Wall 
    private Transform leftWall;
    private Transform rightWall;

    // Default Data
    private Animator anim;
    private SpriteRenderer imgIcon;

    // Anima��o de morte da bola
    [SerializeField] private GameObject ballDeathAnim;

    private void Awake()
    {

        anim = GetComponentInChildren<Animator>();
        anim.runtimeAnimatorController = BallSO.animController;



        imgIcon = GetComponentInChildren<SpriteRenderer>();
        imgIcon.sprite = BallSO.imgIcon;

        //imgIcon = GetComponentInChildren<SpriteRenderer>();



        //anim = GetComponent<Animator>();
        //imgIcon = GetComponent<Sprite>();

        //Debug.Log("anim: " + anim + "imgIcon: " + imgIcon);

        //imgIcon = Data.imgIcon;








        //GetComponentInChildren<GameObject>(true);








        gameInput = GameObject.Find(TAG_GI).GetComponent<GameInput>();
        arrowGo = GameObject.Find(TAG_ARROW);
        arrow2IMG = arrowGo.transform.GetChild(0).gameObject;
        arrowGo.GetComponent<Image>().enabled = false;
        arrow2IMG.GetComponent<Image>().enabled = false;

        leftWall = GameObject.Find(TAG_LW).GetComponent<Transform>();
        rightWall = GameObject.Find(TAG_RW).GetComponent<Transform>();

    }

    void Start()
    {
        // Force
        gameInput.OnForceStarted += Gameinput_OnForceStarted;
        gameInput.OnForcePerformed += Gameinput_OnForcePerformed;
        ball = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RotationArrow();
        
        Vector2 inputVector = gameInput.GetArrowRotationNormalized();
        if (inputVector.y == 1 || zRotate > 90)
        {
            zRotate += inputVector.y;
        }
        else if (inputVector.y == -1 || zRotate > 0)
        {
            zRotate += inputVector.y;
        }

        RotationLimit();
        OnSetArrow();

        Wall();
    }

    // Posicionar a Img da Seta
    void OnSetArrow()
    {
        arrowGo.GetComponent<Image>().rectTransform.localPosition = transform.position;
    }

    // Controla a rota��o usando comandos do teclado
    void RotationArrow()
    {
        arrowGo.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    // Limite do �ngulo que a flecha pode ir
    void RotationLimit()
    {
        if (zRotate >= 90)
        {
            zRotate = 90;
        }
        if (zRotate <= 0)
        {
            zRotate = 0;
        }
    }

    // Enquanto o bot�o de for�a esticar pressionado - carrega o medidor de for�a
    private void Gameinput_OnForceStarted(object sender, System.EventArgs e)
    {
        if (GameManager.instance.kick == 0)
        {
            arrowGo.GetComponent<Image>().enabled = true;
            arrow2IMG.GetComponent<Image>().enabled = true;
            ArrowUpdateForce();
        }
    }
    // Enquanto o bot�o � liberado - chute a bola
    private void Gameinput_OnForcePerformed(object sender, System.EventArgs e)
    {
        // Verifica se o jogador tem mais tentativas
        if (GameManager.instance.kick == 0)
        {
            if (ball != null)
            {
                AudioManager.instance.SongsFXPlay(1);
                ApplyForce();
            }
            force = 0;
            //GameManager.instance.kick = 1;
            arrowGo.GetComponent<Image>().enabled = false;
            arrow2IMG.GetComponent<Image>().enabled = false;
            arrow2IMG.GetComponent<Image>().fillAmount = 0;
        }

    }

    // Fun��o - Que define a for�a total do chute de acordo com o tempo que o bot�o de for�a foi pressionado
    void ApplyForce()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);
        
        if (ball != null)
        {
            ball.AddForce(new Vector2(x, y));
        }
    }

    // Fun��o - Que define a anima��o de carregar a flecha de acordo com o tempo que o bot�o de for�a foi pressionado
    void ArrowUpdateForce()
    {
        //arrow2IMG.GetComponent<Image>().fillAmount += 0.08f * Time.deltaTime;
        arrow2IMG.GetComponent<Image>().fillAmount += 1f * Time.deltaTime;
        force = arrow2IMG.GetComponent<Image>().fillAmount * 1000;

    }

    // Limite de tela se a bola passar a bola � destru�da
    void Wall()
    {
        if(this.gameObject.transform.position.x > rightWall.position.x )
        {
            Destroy(this.gameObject);
            GameManager.instance.ballInGame -= 1;
            GameManager.instance.ballNum -= 1;
        }

        if (this.gameObject.transform.position.x < leftWall.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.ballInGame -= 1;
            GameManager.instance.ballNum -= 1;
        }
    }

    // Fun��o para verificar se a bola colidiu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se a bola atingir uma armadilha, ela sofre dano.
        if (collision.gameObject.CompareTag(TAG_HIT))
        {
            Instantiate(ballDeathAnim, transform.position,Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.ballInGame -= 1;
            GameManager.instance.ballNum -= 1;
        }

        if (collision.gameObject.CompareTag(TAG_WIN))
        {
            GameManager.instance.win = true;
            int temp = IdLevel.instance.level - 3 + 1;
            temp++;

            PlayerPrefs.SetInt("Level_" + temp, 1);
            /*
             * 
            if (temp <= 6)
            {
                PlayerPrefs.SetInt("Level_"+temp,1);
            }
            else if(temp > 6)
            {
                SceneManager.LoadScene(1);
            }
            */
        }
    }
}
