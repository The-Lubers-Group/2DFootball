using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Force : MonoBehaviour
{
    [SerializeField] private Image arrowImg;
    [SerializeField] private float flt_force;
    [SerializeField] private InputAction action;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float force = 0;

    private Rigidbody2D ball;
    private Rotation rotation;
    private float holdDownStartTime;
    public const float MAX_FORCE = 500f;

    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnForceActions += Gameinput_OnForceActions;
        //gameinput.OnForceActions += Gameinput_OnForceActions<GameInput>();
        ball = GetComponent<Rigidbody2D>();
        rotation = GetComponent<Rotation>();
    }

    // Update is called once per frame 
    void Update()
    {
        holdDownStartTime = Time.time;
    }

    //private void Gameinput_OnForceActions(object sender, System.EventArgs e)
    private void Gameinput_OnForceActions(object sender, GameInput.OnEventArgs e)
    {
        if (ball != null)
        {
            ForceControl(e.currentTime);
            AudioManager.instance.SongsFXPlay(1);
            ApplyForce();
        }
    }


    // Função - Que define a animação de carregar a flecha de acordo com o tempo que o botão de força foi pressionado
    void ForceControl(float currentTime)
    {
        arrowImg.fillAmount += 1 * currentTime;
        force = arrowImg.fillAmount * 1000;
    }

    // Função - Que define a força total do chute de acordo com o tempo que o botão de força foi pressionado
    void ApplyForce()
    {
        float x = force * Mathf.Cos(rotation.zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(rotation.zRotate * Mathf.Deg2Rad);

        // ----------------------------------------------------------
        //float holdDownTime = Time.time - holdDownStartTime;

        //float maxForceHoldDownTime = 2f;
        //float holdTimeNormalized = Mathf.Clamp01(holdDownTime / maxForceHoldDownTime);
        //force = holdTimeNormalized * MAX_FORCE;

        if (ball != null)
        {
            ball.AddForce(new Vector2(x, y));
            //ball.AddForce(new int(force));
        }
    }
}
