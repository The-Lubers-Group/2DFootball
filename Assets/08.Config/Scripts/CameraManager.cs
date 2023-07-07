using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    const string TAG_GOAL = "GoalObject";
    const string TAG_BALL = "Ball(Clone)";
    const string TAG_GAME_INPUT = "GameInput";
    
    [SerializeField]private Transform objLeft;
    [SerializeField] private Transform objRight;
    [SerializeField] private Transform ball;
    
    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.beginGame == true)
        {
            if(ball == null && GameManager.instance.ballInGame > 0)
            {
                ball = GameObject.Find(TAG_BALL).GetComponent<Transform>();
                objRight = GameObject.Find(TAG_GOAL).GetComponent<Transform>();
                objLeft = GameObject.Find(TAG_GAME_INPUT).GetComponent<Transform>();
            }
            else if(GameManager.instance.ballInGame > 0)
            {
                Vector3 posCam = transform.position;
                // Passo a posição da bola para camera
                posCam.x = ball.position.x;
                // Colocar limite para não deixar a camera fugir do centro
                posCam.x = Mathf.Clamp(posCam.x, objLeft.position.x - 2f, objRight.position.x - 7f );
                transform.position = posCam;
            }
        }    
    }
}
