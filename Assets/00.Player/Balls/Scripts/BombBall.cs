using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BombBall : BaseBall
{
    public override void OnKick()
    {
        //base.OnKick();
        Debug.Log(" OnKick ---> Bola Bomba 2");
    }
}
