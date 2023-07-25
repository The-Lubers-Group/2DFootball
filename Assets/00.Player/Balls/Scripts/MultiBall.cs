using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;


public class MultiBall : BaseBall
{
    [SerializeField] private int ballPerKick = 2;
    [SerializeField] private LayerMask m_LayerMask;

    private List<BaseBall> duplicateList = new List<BaseBall>();
    public override void OnSpecialAttack()
    {
        if (ballRigdbody2D.velocity.x != 0)
        {
            for (int i = 0; i < ballPerKick; i++)
            {
                BaseBall duplicate = Instantiate(GameManager.instance.ballObject, new Vector2(ballRigdbody2D.position.x, ballRigdbody2D.position.y), Quaternion.identity, GameManager.instance.pos);
                
                duplicate.GetComponentInChildren<SpriteRenderer>().DOColor(Color.blue, 0.3f);

                duplicateList.Add(duplicate);

            }
        }
    }

    public override void SpecialUpdate()
    {
        if(duplicateList.Count > 0)
        {
            foreach (BaseBall clone in duplicateList)
            {
                if (Physics2D.OverlapBox(clone.transform.position, clone.transform.localScale , 0, m_LayerMask))
                {
                    Debug.Log("Hit !! ");
                }
            }
        }
    }
}
