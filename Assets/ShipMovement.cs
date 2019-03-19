using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public delegate void ShipMoving();
    public static event ShipMoving OnShipMoved;

    [Header("Settings")]
    public float speed = 10f;
    public float rotationSpeed = 100f;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
            OnShipMoved?.Invoke();

            GetComponent<ParticleSystem>().Play();
        }
            
        float rotation = -Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, 0, rotation);
    }
}
