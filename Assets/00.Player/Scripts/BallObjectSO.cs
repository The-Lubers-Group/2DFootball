using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName ="BallObjectSO", menuName = "ScriptableObjects/BallObjectSO")]
public class BallObjectSO : ScriptableObject
{
    [HideInInspector] public int ballId;

    [Header("Valores padrões")]
    [Label("Image da Bola")]
    public Sprite imgIcon;

    [Label("Nome da Bola")]
    public string ballName;
    
    [Label("Preço da Bola")]
    public float ballPrice;

    [Label("Bola já foi comprada")]
    //[ReadOnly] public bool WasBought;
    public bool WasBought;
    //[Label("Dano da Bola")]
    //public float ballDamage;

    [Label("Velocidade máxima da Bola")]
    public float ballVelocity;

    [Label("Peso da Bola")]
    public float ballWeight;

    [Space(5)]
    [Header("Animação")]
    public RuntimeAnimatorController animController;
}
