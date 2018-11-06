using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour {
    public float blinkDistance;


    Rigidbody2D rigidbody2D;
    bool movingRight = false;
    bool movingLeft = false;


    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (rigidbody2D.velocity.x < 0)
        {
            movingLeft = true;
            movingRight = false;
        }
        else if (rigidbody2D.velocity.x > 0)
        {
            movingLeft = false;
            movingRight = true;
        }

		if (Input.GetMouseButtonDown(0))
        {
            if (movingLeft)
            {
                transform.position -= new Vector3(blinkDistance, 0, 0);
            }
            else if (movingRight)
            {
                transform.position += new Vector3(blinkDistance, 0, 0);
            }
        }
	}
}
