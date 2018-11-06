using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleBall : MonoBehaviour {

    SpriteRenderer m_SpriteRenderer;
    Rigidbody2D rigidbody2D;

    float speed = 0;
    float deAcceleration = 0.9f;

    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_SpriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        speed *= deAcceleration;
        rigidbody2D.velocity = new Vector2(speed, 0);

    }

    public void throwTeleBall(Vector3 newPosition)
    {
        transform.position = newPosition;
        speed = 30;
    }

    public void setInvisible(bool invis)
    {
        m_SpriteRenderer.enabled = invis;
    }
}
