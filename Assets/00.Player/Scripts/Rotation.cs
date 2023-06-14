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


    void Start()
    {
        OnSetBall();
        OnSetArrow();
    }

    void Update()
    {
        RotationArrow();
        Vector2 inputVector = gameInput.GetArrowRotationNormalized();

        if (inputVector.y == 1) 
        { 
            zRotate += inputVector.y; 
        } 
        else if (inputVector.y == -1) { 
            zRotate -= inputVector.y; 
        }
        Debug.Log(inputVector.y);
    }

    void OnSetArrow() 
    { 
        arrowIMG.rectTransform.localPosition = posStart.position;
    }

    void OnSetBall()
    {
        this.gameObject.transform.position = posStart.position;
    }

    void RotationArrow()
    {
        arrowIMG.rectTransform.eulerAngles = new Vector3(0,0, zRotate);
        //arrowIMG.rectTransform.eulerAngles = Vector3.zero;
    }
}
