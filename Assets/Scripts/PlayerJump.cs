using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    bool grounded = true;
    double previousYpos = 0;
    

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        grounded = Physics2D.Raycast(rigidbody2D.position, new Vector2(0, -1), 20, -1);
        print(grounded);

        

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 7);
        }

        previousYpos = System.Math.Round(rigidbody2D.position.y, 2);
    }
}
