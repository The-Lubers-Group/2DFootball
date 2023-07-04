using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform posStart;
    [SerializeField] private Image arrowIMG;
    [SerializeField] private GameObject arrowGo;
    [SerializeField] private GameInput gameInput;

    public float zRotate;
    public bool Shoot = false;


    void Start()
    {
       OnSetBall();
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
        }
     
        RotationLimit();
        OnSetArrow();
    }
    void OnSetBall()
    {
        this.gameObject.transform.position = posStart.position;
    }

    // Posicionar a Img da Seta
    void OnSetArrow() 
    {
        arrowIMG.rectTransform.localPosition = transform.position;
    }

    // Controla a rotação usando comandos do teclado
    void RotationArrow()
    {
        arrowIMG.rectTransform.eulerAngles = new Vector3(0,0, zRotate);
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

    void OnMouseDown()
    {
        arrowGo.SetActive(true);
    }

}
