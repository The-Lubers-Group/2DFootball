using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [SerializeField]private GameObject bombFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            Instantiate(bombFX, new Vector2 (this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
    }
}
