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
    [Label("Tempo para explos�o da bomba")]
    [SerializeField] private int timeToWait = 3;


    public override void OnSpecialAttack()
    {
        Debug.Log(GameObject.FindAnyObjectByType<BaseBall>().transform);
        //Debug.Log(transform);
        //BaseBall ball = GameManager.instance.ballObject;
        BaseBall ball = GameObject.FindAnyObjectByType<BaseBall>();


        if (ball.transform != null)
        {
            //originalScale = transform.localScale;
            //originalScale = GameObject.FindAnyObjectByType<BaseBall>().transform.localScale;
            originalScale = ball.transform.localScale;
            scaleTo = originalScale * 2;

            //this.transform.DOScale(2f, 0.5f).SetEase(Ease.InBounce);
            ball.transform.DOScale(scaleTo, 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            GetComponentInChildren<SpriteRenderer>().DOColor(Color.red, 0.3f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);

            if (startTimer == true)
            {
                StartCoroutine(explodeTimer());
            }
        }

    }

  
    public void explode()
    {
        BaseBall ball = GameObject.FindAnyObjectByType<BaseBall>();

        //Instantiate(explosion, transform.position, Quaternion.identity);
        //Instantiate(ballDeathAnim, transform.position, Quaternion.identity);
        Instantiate(ballDeathAnim, ball.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        GameManager.instance.ballInGame -= 1;
        GameManager.instance.attempts -= 1;
    }

    IEnumerator explodeTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        explode();
    }
}
