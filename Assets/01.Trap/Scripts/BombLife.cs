using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLife : MonoBehaviour
{
    private GameObject bombRep;

    // Start is called before the first frame update
    void Start()
    {
        bombRep = GameObject.Find("Mine");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Life());
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(bombRep.gameObject);
        Destroy(this.gameObject);
    }
}
