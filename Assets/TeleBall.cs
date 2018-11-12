using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleBall : MonoBehaviour {

    SpriteRenderer m_SpriteRenderer;
    Rigidbody2D rigidbody2D;

    public int ballSpeed;
    bool directionRight = true;

    float speed = 0;
    float deAcceleration = 0.9f;
    int layerMask;

    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_SpriteRenderer.enabled = false;

        // Bit shift the index of the layer (9) to get a bit mask
        layerMask = 1 << 9;
        // This will cast rays only against colliders in layer 9.
       
    }
	
	void Update () {
        if (directionRight && Physics2D.Raycast(transform.position, new Vector2(1, 0), 0.3f, layerMask))
        {
            speed = 0;
        }
        else if(!directionRight && Physics2D.Raycast(transform.position, new Vector2(-1,0), 0.3f, layerMask))
        {
            speed = 0;
        }

        speed *= deAcceleration;
        rigidbody2D.velocity = new Vector2(speed, 0);
        
    }

    public void throwTeleBall(Vector3 newPosition, bool throwDirectionRight)
    {
        if (throwDirectionRight)
        {
            speed = ballSpeed;
            directionRight = true;
        }
        else
        {
            speed = -ballSpeed;
            directionRight = false;
        }
        transform.position = newPosition;
    }

    //comes in inverted, ex: render enabled true = invisible state false;
    public void setInvisible(bool invisibleState)
    {
        m_SpriteRenderer.enabled = !invisibleState;
    }
}
