using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scene Data/Level")]
public class Level : MonoBehaviour
{
    //Settings specific to level only
    [Header("Level specific")]
    public int enemiesCount;
}
