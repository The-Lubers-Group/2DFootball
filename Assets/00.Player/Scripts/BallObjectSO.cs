using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName ="Data_BallSO", menuName = "ScriptableObjects/Unit/Info Data Ball SO", order = 1)]
public class BallObjectSO : ScriptableObject
{
    [HideInInspector] public int ballId;

    [Header("Valores padrões")]
    
    [Label("Prefab da Bola")]
    public BaseBall prefabBall;

    [Label("Image da Bola")]
    public Sprite imgIcon;

    [Label("Nome da Bola")]
    public string ballName;
    
    [Label("Preço da Bola")]
    public float ballPrice;

    [Label("Bola já foi comprada")]
    public bool WasBought;

    [Label("Velocidade máxima da Bola")]
    public float ballVelocity;

    [Label("Peso da Bola")]
    public float ballWeight;

    /*
    [Space(5)]
    [Header("Animação")]
    public RuntimeAnimatorController animController;
    */
}
