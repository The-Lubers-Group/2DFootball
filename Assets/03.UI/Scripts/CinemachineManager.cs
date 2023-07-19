using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    //[SerializeField] BallManager ball;
    [SerializeField] Transform ball;

    public CinemachineVirtualCamera myCinemachine;
    //public GameObject otherGameObject;
    const string TAG_BALL = "Ball(Clone)";
    private void Update()
    {
        if (GameManager.instance.beginGame == true)
        {
            if (ball == null)
            {
                ball = GameObject.Find(TAG_BALL).GetComponent<Transform>();
                Debug.Log(GameObject.Find("ball"));
            }

        }
        //Debug.Log(otherGameObject);

            //BallManager ball = otherGameObject.GetComponent<BallManager>();
            //ball = GameObject.Find("ball").GetComponent<BallManager>();
           
            //myCinemachine.Follow = ball.transform;
    }
}
