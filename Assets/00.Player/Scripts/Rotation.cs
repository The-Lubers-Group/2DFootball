using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform posStart;
    [SerializeField] private Image arrowIMG;
    [SerializeField] private GameInput gameInput;

    public float zRotate;
    public bool Shoot = false;


    void Start()
    {
       OnSetBall();
       OnSetArrow();
    }

    void Update()
    {
        RotationArrow();
        Vector2 inputVector = gameInput.GetArrowRotationNormalized();

        if (inputVector.y == 1 || zRotate > 90) 
        { 
            zRotate += inputVector.y; 
        }
        else if (inputVector.y == -1 || zRotate > 0) { 
            zRotate += inputVector.y; 
            //zRotate -= inputVector.y; 
        }
        RotationLimit();
        // Debug.Log(inputVector.y);

    }
    void OnSetBall()
    {
        this.gameObject.transform.position = posStart.position;
        //Debug.Log(this.gameObject.transform.position);

    }


    void OnSetArrow() 
    {
        arrowIMG.rectTransform.localPosition = posStart.position;
        //arrowIMG.rectTransform.localPosition = this.gameObject.transform.position;
        Debug.Log(arrowIMG.rectTransform.localPosition);
    }

    
    void RotationArrow()
    {
        arrowIMG.rectTransform.eulerAngles = new Vector3(0,0, zRotate);
        //arrowIMG.rectTransform.eulerAngles = Vector3.zero;
    }

    void RotationLimit()
    {
        if (zRotate >= 90)
        {
            zRotate = 90;
        }
        if (zRotate <= 0)
        {
            zRotate = 0;
        }
    }
}
