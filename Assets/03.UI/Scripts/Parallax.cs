using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    const string TAG_BALL = "Ball(Clone)";

    [SerializeField] private Camera cam;
    [SerializeField] private Transform subject;

    Vector2 startPosition;
    float startZ;

    private float clippingPlane;
    private float distanceFromSubject;
    private float parallaxFactor;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    //float distanceFromSubject => transform.position.z - subject.position.z;
    //float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0? cam.farClipPlane : cam.nearClipPlane));
    //float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    public void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    public void Update()
    {
        if (GameObject.Find(TAG_BALL))
        {
            subject = GameObject.Find(TAG_BALL).GetComponent<Transform>();
           
            distanceFromSubject = transform.position.z - subject.position.z;
            clippingPlane = cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane);
            parallaxFactor = Mathf.Abs(distanceFromSubject) / clippingPlane;

        }
        
        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
