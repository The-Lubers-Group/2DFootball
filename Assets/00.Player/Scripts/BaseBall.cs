using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public  class BaseBall : MonoBehaviour
{
    //public BallObjectSO BallSO;
    //public BallObjectSO ballAtual;



    [HideInInspector] public int ballID;

    [Header("Valores padr�es")]
    [Label("Image da Bola")]
    public Sprite icon;
    private SpriteRenderer imgIcon;

    [Label("Nome da Bola")]
    public string ballName;

    [Label("Pre�o da Bola")]
    public float ballPrice;

    [Label("Bola j� foi comprada?")]
    public bool WasBought;

    [Space(5)]
    [Label("Velocidade m�xima da Bola")]
    [SerializeField] private float ballVelocity;

    [Label("Peso da Bola")]
    [SerializeField] private float ballWeight;

    [Label("For�a da Bola")]
    [SerializeField] private float force;

    [Space(5)]
    [Header("Anima��o")]
    public RuntimeAnimatorController animController;

    [Label("Anima��o de morte da bola")]
    [SerializeField] private GameObject ballDeathAnim;

    //public string nameIcon;

    private GameObject arrowGo;
    private GameObject arrowFill;
    protected GameInput gameInput;

    [HideInInspector] public float zRotate;
    //[HideInInspector] public bool shoot = false;
    protected Rigidbody2D ballRigdbody2D;

    // Wall 
    private Transform leftWall;
    private Transform rightWall;

    // Default Data
    private Animator anim;


    public bool IsClone;


    // TAG
    const string TAG_ARROW = "Arrow";
    const string TAG_GI = "GameInput";
    const string TAG_PS = "StartPoint";

    const string TAG_LW = "LeftWall";
    const string TAG_RW = "RightWall";

    const string TAG_HIT = "hit";
    const string TAG_WIN = "win";
    const string TAG_GRID = "Grid";


    //[SerializeField] private BallSelection ballSelection;


    private void Awake()
    {
        //ballAtual = BallSO;
        /*
        if(ballSelection)
        {
            ballSO = ballSelection.selectBall;
            Debug.Log(ballSO);
        }
        */

        // Set Anim
        anim = GetComponentInChildren<Animator>();
        anim.runtimeAnimatorController = animController;

        // Set Ball Icon
        imgIcon = GetComponentInChildren<SpriteRenderer>();
        imgIcon.sprite = icon;



        // Set GameInput
        gameInput = GameObject.Find(TAG_GI).GetComponent<GameInput>();

        // Set Arrow
        arrowGo = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        arrowFill = arrowGo.transform.GetChild(0).gameObject;

        // Disable arrow 
        arrowGo.GetComponent<Image>().enabled = false;
        arrowFill.GetComponent<Image>().enabled = false;


        // Set Left and Right Wall
        leftWall = GameObject.Find(TAG_LW).GetComponent<Transform>();
        rightWall = GameObject.Find(TAG_RW).GetComponent<Transform>();
    }

    void Start()
    {
        // Force
        gameInput.OnForceStarted += Gameinput_OnForceStarted;
        gameInput.OnForcePerformed += Gameinput_OnForcePerformed;
        gameInput.OnKickPressed += Gameinput_OnKickPressed;
        ballRigdbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /*
        if(ballAtual != BallSO)
        {
            // Set Anim
            anim = GetComponentInChildren<Animator>();
            anim.runtimeAnimatorController = BallSO.animController;

            // Set Ball Icon
            imgIcon = GetComponentInChildren<SpriteRenderer>();
            imgIcon.sprite = BallSO.imgIcon;


            ballAtual = BallSO;
        }
        */


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

        //RotationLimit();
        OnSetArrow();

        Wall();

        Status();
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
        if (GameManager.instance.kick == 0 && arrowGo)
        {
            arrowGo.GetComponent<Image>().enabled = true;
            arrowFill.GetComponent<Image>().enabled = true;
            ArrowUpdateForce();
        }
    }
    // Enquanto o bot�o � liberado - chute a bola
    private void Gameinput_OnForcePerformed(object sender, System.EventArgs e)
    {
        // Verifica se o jogador tem mais tentativas
        if (GameManager.instance.kick == 0)
        {
            if (ballRigdbody2D != null)
            {
                AudioManager.instance.SongsFXPlay(1);
                ApplyForce();
            }
            force = 0;
            //GameManager.instance.kick = 1;
            if(arrowGo)
            {
                arrowGo.GetComponent<Image>().enabled = false;
                arrowFill.GetComponent<Image>().enabled = false;
                arrowFill.GetComponent<Image>().fillAmount = 0;
            }
        }

    }

    private void Gameinput_OnKickPressed(object sender, System.EventArgs e)
    {
        OnKick();

    }


    // Fun��o - Que define a for�a total do chute de acordo com o tempo que o bot�o de for�a foi pressionado
    void ApplyForce()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);
        
        if (ballRigdbody2D != null)
        {
            ballRigdbody2D.AddForce(new Vector2(x, y));
        }
    }

    // Fun��o - Que define a anima��o de carregar a flecha de acordo com o tempo que o bot�o de for�a foi pressionado
    void ArrowUpdateForce()
    {
        if(arrowGo)
        {
            arrowGo.transform.GetChild(0).GetComponent<Image>().fillAmount += 1f * Time.deltaTime;
            force = arrowFill.GetComponent<Image>().fillAmount * 1000;
            //force = arrowGO.transform.GetChild(0).GetComponent<Image>().fillAmount * 1000;
        }
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
                Instantiate(ballDeathAnim, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                OnDestroy();
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

    public void OnDestroy()
    {
        Destroy(this.gameObject); 
    }

    public virtual void OnKick()
    {
        Debug.Log(" OnKick ---> Bola Base 1 ");

    }
    public virtual void Status()
    {


    }
    //public void OnKick();
}
