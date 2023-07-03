using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    public Text txt;
    public InputField boxText;
    private float testF;

    // Start is called before the first frame update
    void Start()
    {
        txt.text = PlayerPrefs.GetFloat("ponts").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveFloat()
    {
        testF = float.Parse(boxText.text);
        PlayerPrefs.SetFloat("ponts", testF);
        //txt.text = testF.ToString();
    }
}
