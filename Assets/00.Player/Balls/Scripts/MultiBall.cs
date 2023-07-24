using System.Collections.Generic;
using UnityEngine;


public class MultiBall : BaseBall
{
    [SerializeField] private int ballPerKick = 2;

    private List<MultiBall> clonesBall = new List<MultiBall>();




    /*
    private List<Rigidbody2D> rg = new List<Rigidbody2D>();
    private List<BaseBall> clonesBall = new List<BaseBall>();
    private List<GameObject> a = new List<GameObject>();
    */

    public override void OnKick()
    {
        //base.OnKick();
        for (int i = 0; i < ballPerKick; i++)
        {
            //Debug.Log(" OnKick ---> Bola Multi 3");
            //Debug.Log(ballRigdbody2D.velocity.x);
            
            if (ballRigdbody2D.velocity.x != 0)
            {
                BaseBall cloneBall = GameManager.instance.ballObject;

                Instantiate(cloneBall, new Vector2(ballRigdbody2D.position.x, ballRigdbody2D.position.y), Quaternion.identity, GameManager.instance.pos);
               // Debug.Log(cloneBall.IsClone);
                cloneBall.IsClone = true;
               // Debug.Log(cloneBall.IsClone);





                //BaseBall cloneBall = GameManager.instance.ballObject;
                //Instantiate(cloneBall, new Vector2(ballRigdbody2D.position.x, ballRigdbody2D.position.y), Quaternion.identity, GameManager.instance.pos);
                //clonesBall.Add(GetComponent<BaseBall>());
                //clonesBall.Add(Instantiate(cloneBall, new Vector2(ballRigdbody2D.position.x, ballRigdbody2D.position.y), Quaternion.identity, GameManager.instance.pos));
                //cloneBall.IsClone = true;
                //Instantiate(cloneBall, new Vector2(ballRigdbody2D.position.x, ballRigdbody2D.position.y), Quaternion.identity, GameManager.instance.pos);
                //Rigidbody2D ballChildRigdbody2D = cloneBall.GetComponent<Rigidbody2D>();
                //ballChildRigdbody2D.AddForce(new Vector2(ballRigdbody2D.velocity.x, ballRigdbody2D.velocity.y));
                //cloneBall.gameObject.SetActive(false);

            }
        }
    
        if (clonesBall.Count > 0)
        {
            foreach (BaseBall cloneB in clonesBall)
            {

                Debug.Log(cloneB);
                //a.Add(Instantiate(cloneB, new Vector2(ballRigdbody2D.position.x, ballRigdbody2D.position.y), Quaternion.identity, GameManager.instance.pos));

                //Destroy(cloneB.gameObject);
            }
        }
    
    }

    public override void Status()
    {
        if (IsClone == true && ballRigdbody2D.velocity.x == 0)
        {
            //Instantiate(ballDeathAnim, transform.position, Quaternion.identity);
            Destroy(gameObject);
            OnDestroy();

        }













/*

        if (clonesBall.Count > 0)
        {
            foreach (BaseBall cloneB in clonesBall)
            {
                Debug.Log(cloneB.GetComponentInChildren<Rigidbody2D>().velocity.x);
                if (cloneB.GetComponentInChildren<Rigidbody2D>().velocity.x == 0)
                {
                    //Destroy(cloneB);
                    //cloneB.gameObject.SetActive(false);
                    //cloneB.GetComponent<BaseBall>().SetActive(false);
                }
            }
        }
*/


        /*
        Debug.Log(clonesBall.Count);
        if (clonesBall.Count > 0)
        {
            foreach (BaseBall clone in clonesBall)
            {
                Debug.Log(clone.GetComponent<Rigidbody2D>().velocity.x);
                if (clone.GetComponent<Rigidbody2D>().velocity.x == 0)
                {
                    Debug.Log(clone.gameObject);
                    clone.gameObject.SetActive(false);
                    //Destroy(clone.gameObject);
                    //clone.RemoveAt(clone.Count - 1);
                    // clone.OnDestroy();

                }
            }
        }
        */
    }
}
