using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Force : MonoBehaviour
{
    [SerializeField] private GameInput gameinput;
    private Rigidbody2D ball;
    private float force = 1000f;
    private Rotation rotation;

    // Start is called before the first frame update
    void Start()
    {
        gameinput.OnForceActions += Gameinput_OnForceActions;
        ball = GetComponent<Rigidbody2D>();
        rotation = GetComponent<Rotation>();
    }

    private void Gameinput_OnForceActions(object sender, System.EventArgs e)
    {
        float x = force * Mathf.Cos(rotation.zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(rotation.zRotate * Mathf.Deg2Rad);

        if (ball != null)
        {
            ball.AddForce(new Vector2(x, y));
        }
    }

    // Update is called once per frame 
    void Update()
    {

        // ApplyForce();
    }

    void ApplyForce()
    {
        float x = force * Mathf.Cos(rotation.zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(rotation.zRotate * Mathf.Deg2Rad);
        
        if (ball != null)
        {
            ball.AddForce(new Vector2(x, y));
        }
    }
}
