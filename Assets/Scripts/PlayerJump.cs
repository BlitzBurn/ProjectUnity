using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float sidesOffset;
    public int coyoteTiming;
    int coyoteTimer = 0;

    Rigidbody2D rigidbody2D;
    bool grounded = true;
    double previousYpos = 0;
    

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    { 
        if (isGrounded())
        {
            grounded = true;
            coyoteTimer = coyoteTiming;
        }
        else
        {
            coyoteTime();
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 7);
        }

        previousYpos = System.Math.Round(rigidbody2D.position.y, 2);
    }

    bool isGrounded()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        //testRayCastSize(); 

        return Physics2D.Raycast(transform.position - new Vector3(sidesOffset, 0, 0), transform.TransformDirection(Vector3.down), 1, layerMask) || 
            Physics2D.Raycast(transform.position + new Vector3(sidesOffset, 0, 0), transform.TransformDirection(Vector3.down), 1, layerMask);
    }

    void testRayCastSize()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        if (Physics2D.Raycast(transform.position - new Vector3(sidesOffset, 0, 0), transform.TransformDirection(Vector3.down), 1, layerMask))
        {
            Debug.DrawRay(transform.position - new Vector3(sidesOffset, 0, 0), transform.TransformDirection(Vector3.down), Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position - new Vector3(sidesOffset, 0, 0), transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        if (Physics2D.Raycast(transform.position + new Vector3(sidesOffset, 0, 0), transform.TransformDirection(Vector3.down), 1, layerMask))
        {
            Debug.DrawRay(transform.position + new Vector3(sidesOffset, 0, 0), transform.TransformDirection(Vector3.forward), Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(sidesOffset, 0, 0), transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    void coyoteTime()
    {
        coyoteTimer--;
        if (coyoteTimer <= 0)
        {
            grounded = false;
        }
    }
}
