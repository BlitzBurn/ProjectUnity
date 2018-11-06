using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour {

    public float blinkDistance;
    public float blinkCooldownTime;

    SpriteRenderer m_SpriteRenderer;
    //The Color to be assigned to the Renderer’s Material

    Rigidbody2D rigidbody2D;
    bool movingRight = false;
    bool movingLeft = false;
    float blinkCooldownTimer = 0;


    // Use this for initialization
    void Start () {

        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (rigidbody2D.velocity.x < 0)
        {
            movingLeft = true;
            movingRight = false;
            m_SpriteRenderer.flipX = false;
        }
        else if (rigidbody2D.velocity.x > 0)
        {
            movingLeft = false;
            movingRight = true;
            m_SpriteRenderer.flipX = true;
        }

        if (Input.GetMouseButtonDown(0) && blinkCooldownTimer == 0)
        {
            blink();
            blinkCooldownTimer = blinkCooldownTime;
        }
        cooldownVisuals();
        blinkCooldown();
	}

    void blink()
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

    void blinkCooldown()
    {
        if (blinkCooldownTimer != 0)
        {
            blinkCooldownTimer--;
        }
    }

    void cooldownVisuals()
    {
        if (blinkCooldownTimer != 0)
            m_SpriteRenderer.color = new Color(1 - blinkCooldownTimer/blinkCooldownTime, 0, 0);
        else
            m_SpriteRenderer.color = new Color(1, 1, 1);

    }
}
