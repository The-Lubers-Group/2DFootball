using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuvaManager : MonoBehaviour
{
    private SliderJoint2D barrier;
    private JointMotor2D aux;

    // Start is called before the first frame update
    void Start()
    {
        barrier = GetComponent<SliderJoint2D>();

        aux = barrier.motor;
    }

    // Update is called once per frame
    void Update()
    {
        if(barrier.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = Random.Range(-1, -5);
            barrier.motor = aux;

        }

        if (barrier.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = Random.Range(1, 5);
            barrier.motor = aux;
        }
    }
}
