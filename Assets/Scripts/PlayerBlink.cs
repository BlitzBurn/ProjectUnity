using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour {

    public GameObject teleBall;
    public float blinkDistance;
    public float blinkCooldownTime;
    public int clickDelay;

    TeleBall teleBallScript;
    SpriteRenderer m_SpriteRenderer;
    Rigidbody2D rigidbody2D;

    int clickTimer = 0;
    bool teleBallOut = false;
    bool movingRight = false;
    bool movingLeft = false;
    float blinkCooldownTimer = 0;


    // Use this for initialization
    void Start () {

        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        teleBallScript = teleBall.GetComponent<TeleBall>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        handleDirection();
        handleTeleport();
        handleBlink();
        handleClickDelay();
	}

    void handleDirection()
    {
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
    }

    void handleTeleport()
    {
        if (!teleBallOut && Input.GetMouseButtonUp(1) && clickTimer == 0)
        {
            teleBallOut = true;
            teleBallScript.setInvisible(false);
            //teleBall.Throw();
            clickTimer = clickDelay;
        }
        else if (teleBallOut && Input.GetMouseButtonUp(1) && clickTimer == 0)
        {
            teleBallOut = false;
            //teleportToTeleBall();
            teleBallScript.setInvisible(true);
            clickTimer = clickDelay;
        }


        
    }

    void handleBlink()
    {
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

    void handleClickDelay()
    {
        if (clickTimer != 0)
            clickTimer--;
    }
}
