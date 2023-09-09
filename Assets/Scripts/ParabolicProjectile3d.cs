using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile3d : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float angle = 45f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Calculate initial velocity components
        float radianAngle = angle * Mathf.Deg2Rad;
        float initialVelocityX = initialSpeed * Mathf.Cos(radianAngle);
        float initialVelocityY = initialSpeed * Mathf.Sin(radianAngle);

        // Apply initial velocity
        rb.velocity = new Vector3(initialVelocityX, initialVelocityY, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        // Calculate the position based on time and the loop equation
        float t = Time.time * initialSpeed;
        float x = Mathf.Sin(t) * angle;
        float y = Mathf.Cos(t) * angle;
        Vector3 newPosition = new Vector3(x, y, 0f);

        // Set the object's position
        rb.MovePosition(newPosition);
    }
}
