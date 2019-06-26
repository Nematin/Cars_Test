using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public float InitialAngularVelocity = -60;
    public float Force = -120;

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.angularVelocity = InitialAngularVelocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.angularVelocity += Force*Time.deltaTime;
    }
}
