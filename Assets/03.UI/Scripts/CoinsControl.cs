using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsControl : MonoBehaviour
{

    const string TAG = "ball";
    const int NCOIN = 10;

    //Verifica se a bola colidiu com a moeda
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(TAG))
        {
            ScoreManager.instance.CollectCoins(NCOIN);
            Destroy(this.gameObject);
        }
    }
}
