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
    //[SerializeField] private GameInput gameinput;
    [SerializeField] private GameInput gameinput;
    [SerializeField] private float force = 0; //1000f

    private Rigidbody2D ball;
    private Rotation rotation;
    private float holdDownStartTime;
    public const float MAX_FORCE = 500f;

    // Start is called before the first frame update
    void Start()
    {
        gameinput.OnForceActions += Gameinput_OnForceActions;
        //gameinput.OnForceActions += Gameinput_OnForceActions<GameInput>();
        ball = GetComponent<Rigidbody2D>();
        rotation = GetComponent<Rotation>();
    }


    // Update is called once per frame 
    void Update()
    {
        holdDownStartTime = Time.time;
        //Timer();
    }

    //private void Gameinput_OnForceActions(object sender, System.EventArgs e)
    private void Gameinput_OnForceActions(object sender, GameInput.OnEventArgs e)
    {
        //Debug.Log("Gameinput_OnForceActions ----> " + e.currentTime);
        if (ball != null)
        {
            ForceControl(e.currentTime);
            ApplyForce();
        }
    }

    void ForceControl(float currentTime)
    {
        //arrowImg.fillAmount += 1 * Time.deltaTime;
        arrowImg.fillAmount += 1 * currentTime;
        force = arrowImg.fillAmount * 1000;
    }


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
