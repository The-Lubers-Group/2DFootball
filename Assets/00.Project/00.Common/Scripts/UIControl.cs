using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    const string TAG_COIN = "NCoins";
    private TMP_Text pointsUI;

    private void Awake()
    {
        pointsUI = GameObject.Find(TAG_COIN).GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("asdsdsdsdsdsdsdsdsdsdsdsdsd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
