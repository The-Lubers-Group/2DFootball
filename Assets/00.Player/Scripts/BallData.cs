using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BallObjectSO", menuName = "ScriptableObjects/BallObjectSO")]
public class BallData : ScriptableObject
{
    [Header("Default")]
    public Sprite imgIcon;

    // ANIMATION
    [Space(5)]
    [Header("Animation")]
    public RuntimeAnimatorController animController;

}
