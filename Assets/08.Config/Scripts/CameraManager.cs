using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //[SerializeField] private Transform ball;
    [SerializeField] private BaseBall ball;
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
                //ball = GameObject.Find("StartPoint").GetComponent<BaseBall>();
                ball = GameObject.Find("StartPoint").GetComponentInChildren<BaseBall>();
                //ball = GetComponent<BaseBall>();
            }
            if (ball && myCinemachine) { 
                myCinemachine.Follow = ball.transform;
            }
        }
    }
}
