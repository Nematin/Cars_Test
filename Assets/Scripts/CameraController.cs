using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float velocity = 9.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertival = Input.GetAxis("Vertical");
        Vector3 position = transform.position;
        position.x = position.x + velocity * horizontal * Time.deltaTime;
        position.y = position.y + velocity * vertival * Time.deltaTime;
        transform.position = position;
    }
}
