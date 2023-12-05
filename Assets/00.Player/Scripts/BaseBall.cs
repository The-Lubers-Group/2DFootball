using DG.Tweening;
using DG.Tweening.Core.Easing;
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


public class BaseBall : MonoBehaviour
{

    [HideInInspector] public int ballID;

    [Header("Atributos: Base")]
    [Label("Image da Bola")]
    public Sprite icon;
    private SpriteRenderer imgIcon;

    [Label("Nome da Bola")]
    public string ballName;

    [Label("Preço da Bola")]
    public float ballPrice;

    [Label("Bola já foi comprada?")]
    public bool WasBought;

    [Space(5)]
    [Label("Velocidade máxima da Bola")]
    [SerializeField] private float ballVelocity;

    [Label("Peso da Bola")]
    [SerializeField] private float ballWeight;

    [Label("Força da Bola")]
    [SerializeField] private float force;

    [Space(5)]
    [Header("Animação")]
    public RuntimeAnimatorController animController;

    [Label("Animação de morte da bola")]
    [SerializeField] protected GameObject ballDeathAnim;

    //public string nameIcon;

    [SerializeField] private Canvas arrowCanvas;
    [SerializeField] private Image arrow;
    //[SerializeField] private Image arrowFull;
    protected GameInput gameInput;

    [HideInInspector] public float zRotate;
    //[HideInInspector] public bool shoot = false;
    public Rigidbody2D ballRigdbody2D;

    // Wall 
    private Transform leftWall;
    private Transform rightWall;

    // Default Data
    private Animator anim;

    protected bool startTimer = false;

    // TAG
    //const string TAG_ARROW = "Arrow";
    const string TAG_GI = "GameInput";
    const string TAG_PS = "StartPoint";

    const string TAG_LW = "LeftWall";
    const string TAG_RW = "RightWall";

    const string TAG_HIT = "hit";
    const string TAG_WIN = "win";


    private GameManager gameManager;
    //private bool isMoving = false;

    private void Awake()
    {
        // Set Anim
        anim = GetComponentInChildren<Animator>();
        anim.runtimeAnimatorController = animController;

        // Set Ball Icon
        imgIcon = GetComponentInChildren<SpriteRenderer>();
        imgIcon.sprite = icon;

        // Set GameInput
        gameInput = GameObject.Find(TAG_GI).GetComponent<GameInput>();

        //arrowCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        arrowCanvas.worldCamera = GameObject.FindObjectOfType<Camera>();
        //Debug.Log(GameObject.FindObjectOfType<Camera>());
        
        // Disable arrow 
        arrow.enabled = false;
        arrow.transform.GetChild(0).GetComponent<Image>().enabled = false;
        
        //arrowFull.enabled = false;


        // Set Left and Right Wall
        leftWall = GameObject.Find(TAG_LW).GetComponent<Transform>();
        rightWall = GameObject.Find(TAG_RW).GetComponent<Transform>();

        startTimer = true;
    }

    void Start()
    {
        // Force
        gameInput.OnForceStarted += Gameinput_OnForceStarted;
        gameInput.OnForcePerformed += Gameinput_OnForcePerformed;
        gameInput.OnKickPressed += Gameinput_OnKickPressed;
        ballRigdbody2D = GetComponent<Rigidbody2D>();


        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        SpecialUpdate();
        RotationArrow();

        if (ballRigdbody2D == null)
        {
            //ballRigdbody2D = GetComponent<Rigidbody2D>();
            ballRigdbody2D = GameObject.FindAnyObjectByType<Rigidbody2D>();
        }



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

    }

    // Posicionar a Img da Seta
    void OnSetArrow()
    {
        arrow.transform.position = transform.position;
    }

    // Controla a rotação usando comandos do teclado
    void RotationArrow()
    {
        arrow.rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    // Limite do ângulo que a flecha pode ir
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

    // Enquanto o botão de força esticar pressionado - carrega o medidor de força
    private void Gameinput_OnForceStarted(object sender, System.EventArgs e)
    {
        if (ballRigdbody2D != null)
        {
            //if (GameManager.instance.kick == 0 && ballRigdbody2D.velocity.magnitude == 0 && arrowGo)
            if (GameManager.instance.kick == 0 && arrow)
            {
                arrow.enabled = true;
                arrow.transform.GetChild(0).GetComponent<Image>().enabled = true;

                ArrowUpdateForce();
            }
        }
            
    }
    // Enquanto o botão é liberado - chute a bola
    private void Gameinput_OnForcePerformed(object sender, System.EventArgs e)
    {
        if (ballRigdbody2D != null)
        {
            //if (GameManager.instance.kick == 0 && ballRigdbody2D.velocity.magnitude == 0 && arrowGo)
            if (GameManager.instance.kick == 0 && arrow)
            {
                AudioManager.instance.SongsFXPlay(1);
                ApplyForce();
                //GameManager.instance.kick = 1;
                force = 0;

                arrow.enabled = false;
                arrow.transform.GetChild(0).GetComponent<Image>().enabled = false;
                arrow.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;

            }
        }
    }

    private void Gameinput_OnKickPressed(object sender, System.EventArgs e)
    {
        if (ballRigdbody2D == null)
        {
            //ballRigdbody2D = GetComponent<Rigidbody2D>();
            ballRigdbody2D = GameObject.FindAnyObjectByType<Rigidbody2D>();
        }

        if (ballRigdbody2D.velocity.magnitude != 0)
        {
            OnSpecialAttack();
        }
    }

    // Função - Que define a força total do chute de acordo com o tempo que o botão de força foi pressionado
    void ApplyForce()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (ballRigdbody2D != null)
        {
            ballRigdbody2D.AddForce(new Vector2(x, y));
        }
    }

    // Função - Que define a animação de carregar a flecha de acordo com o tempo que o botão de força foi pressionado
    void ArrowUpdateForce()
    {
        if (arrow)
        {
            arrow.transform.GetChild(0).GetComponent<Image>().fillAmount += 1f * Time.deltaTime;
            force = arrow.transform.GetChild(0).GetComponent<Image>().fillAmount * 1000;
        }
    }

    // Limite de tela se a bola passar a bola é destruída
    void Wall()
    {
        if (this.gameObject.transform.position.x > rightWall.position.x)
        {
            Respawn();
            /*
            Destroy(this.gameObject);
            GameManager.instance.ballInGame -= 1;
            GameManager.instance.ballNum -= 1;
            */
        }

        if (this.gameObject.transform.position.x < leftWall.position.x)
        {
            Respawn();
            /*
            Destroy(this.gameObject);
            GameManager.instance.ballInGame -= 1;
            GameManager.instance.ballNum -= 1;
            */
        }
    }

    // Função para verificar se a bola colidiu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se a bola atingir uma armadilha, ela sofre dano.
        if (collision.gameObject.CompareTag(TAG_HIT))
        {
            
            Instantiate(ballDeathAnim, transform.position, Quaternion.identity);

            //GameManager.instance.ballObject = ;
            BaseBall ball = GameManager.instance.ballObject;


            Respawn();
            //Destroy(this.gameObject);

            //GameManager.instance.ballInGame -= 1;
            //GameManager.instance.ballNum -= 1;
            
            //GameManager.instance.ballObject = ball;
            GameObject.FindObjectOfType<GameManager>().ballObject = ball;
            
            //Debug.Log("FindObjectOfType" + ball);
            //GameManager.instance.BallBorn();  
            //gameManager.BallBorn();
            //Debug.Log("hit");

        }

        if (collision.gameObject.CompareTag(TAG_WIN))
        {
            GameManager.instance.win = true;
            int temp = IdLevel.instance.level - 3 + 1;
            temp++;
            PlayerPrefs.SetInt("Level_" + temp, 1);
        }
    }
    
    public virtual void OnSpecialAttack() { }
    public virtual void SpecialUpdate() { }


    public void Respawn()
    {
       gameManager.RespawnBall();
       Destroy(this.gameObject);
    }
}

