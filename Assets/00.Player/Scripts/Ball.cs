using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    // TAG
    const string TAG_ARROW = "Arrow";
    const string TAG_GI = "GameInput";
    const string TAG_PS = "StartPoint";

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

    private void Awake()
    {
        gameInput = GameObject.Find(TAG_GI).GetComponent<GameInput>();
        arrowGo = GameObject.Find(TAG_ARROW);
        arrow2IMG = arrowGo.transform.GetChild(0).gameObject;
        //arrowGo.SetActive(true);
        arrowGo.GetComponent<Image>().enabled = false;
        arrow2IMG.GetComponent<Image>().enabled = false;
    }

    void Start()
    {
        // Force
        //gameInput.OnForcePerformed += Gameinput_OnForcePerformed;
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
    }

    // Posicionar a Img da Seta
    void OnSetArrow()
    {
        arrowGo.GetComponent<Image>().rectTransform.localPosition = transform.position;
    }

    // Controla a rotação usando comandos do teclado
    void RotationArrow()
    {
        arrowGo.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
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

    private void Gameinput_OnForceStarted(object sender, System.EventArgs e)
    {
        arrowGo.GetComponent<Image>().enabled = true;
        arrow2IMG.GetComponent<Image>().enabled = true;

        ArrowUpdateForce();

    }

    //private void Gameinput_OnForceActions(object sender, System.EventArgs e)
    private void Gameinput_OnForcePerformed(object sender, System.EventArgs e)
    {

        //Destroy(this.gameObject);
        //GameManager.instance.ballInGame -= 1;

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

    // Função - Que define a força total do chute de acordo com o tempo que o botão de força foi pressionado
    void ApplyForce()
    {
        
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);
        
        if (ball != null)
        {
            ball.AddForce(new Vector2(x, y));
        }

    }

    // Função - Que define a animação de carregar a flecha de acordo com o tempo que o botão de força foi pressionado
    void ArrowUpdateForce()
    {
        //arrow2IMG.GetComponent<Image>().fillAmount += 0.08f * Time.deltaTime;
        arrow2IMG.GetComponent<Image>().fillAmount += 1f * Time.deltaTime;
        force = arrow2IMG.GetComponent<Image>().fillAmount * 1000;

    }
}
