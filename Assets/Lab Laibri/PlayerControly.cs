using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControly : MonoBehaviour
{
    public float speed;
    public Rigidbody Rb;
    private float zBoundLimit=11;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput); // Create a movement vector

        Rb.AddForce(movement * speed); // Apply the force with the desired speed


        if (transform.position.z < -zBoundLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundLimit);
        }
        if(transform.position.z > zBoundLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundLimit);
        }
    }
}
