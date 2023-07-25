using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using NaughtyAttributes;

public class BombBall : BaseBall
{
    private Vector3 originalScale;
    private Vector3 scaleTo;

    [Space(5)]
    [Header("Atributos: Bola Bomba")]
    [Label("Tempo para explosão da bomba")]
    [SerializeField] private int timeToWait = 3;


    public override void OnSpecialAttack()
    {
        Debug.Log(transform);
        if(transform != null)
        {
            originalScale = transform.localScale;
            scaleTo = originalScale * 2;

            //this.transform.DOScale(2f, 0.5f).SetEase(Ease.InBounce);
            transform.DOScale(scaleTo, 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            GetComponentInChildren<SpriteRenderer>().DOColor(Color.red, 0.3f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);

            if (startTimer == true)
            {
                StartCoroutine(explodeTimer());
            }
        }

    }

  
    public void explode()
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(ballDeathAnim, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        GameManager.instance.ballInGame -= 1;
        GameManager.instance.ballNum -= 1;
    }

    IEnumerator explodeTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        explode();
    }
}
