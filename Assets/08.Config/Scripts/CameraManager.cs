using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    const string TAG_GOAL = "GoalObject";
    const string TAG_BALL = "BallPrefabs(Clone)";
    const string TAG_GAME_INPUT = "GameInput";
    
    private Transform objLeft;
    private Transform objRight;
    //[SerializeField] private Transform ball;
    [SerializeField] private BaseBall ball;
   
    public GameObject otherGameObject;
    
    private float t = 1;

    [SerializeField] private CinemachineVirtualCamera myCinemachine;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.beginGame == true)
        {
            if(myCinemachine)
            {
                myCinemachine.m_Lens.OrthographicSize = 5f;
            }

            if (ball == null && GameManager.instance.ballInGame > 0)
            {
                //ball = GameObject.Find(TAG_BALL).GetComponent<Transform>();

                ball = GetComponent<BaseBall>();

            }

            if (ball && myCinemachine) { 
                myCinemachine.Follow = ball.transform;
            }



            /*
            if (ball == null && objLeft == null && objRight == null && GameManager.instance.ballInGame > 0)
            {
                ball = GameObject.Find(TAG_BALL).GetComponent<Transform>();
                objRight = GameObject.Find(TAG_GOAL).GetComponent<Transform>();
                objLeft = GameObject.Find(TAG_GAME_INPUT).GetComponent<Transform>();
            }
            
            // Controlar o retorno suave de camera para o ponto inicial
            if (objLeft != null && transform.position.x != objLeft.position.x)
            {
                t -= .08f * Time.deltaTime;
               
                //update the position
                transform.position = new Vector3(Mathf.SmoothStep(objLeft.position.x, Camera.main.transform.position.x, t), this.transform.position.y, transform.position.z);
            }

            if (ball != null && GameManager.instance.ballInGame > 0)
            {
                Vector3 posCam = transform.position;
                // Passo a posição da bola para camera
                posCam.x = ball.position.x;
                // Colocar limite para não deixar a camera fugir do centro
                posCam.x = Mathf.Clamp(posCam.x, objLeft.position.x, objRight.position.x - 7f );
                transform.position = posCam;
            }

            */
        }
    }
}
