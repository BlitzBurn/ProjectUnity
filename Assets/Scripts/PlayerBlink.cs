using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour {

    public GameObject teleBall;
    public float blinkDistance;
    public float blinkCooldownTime;
    public int clickDelay;
    public int immunityTime;

    TeleBall teleBallScript;
    SpriteRenderer m_SpriteRenderer;
    Rigidbody2D rigidbody2D;

    int clickTimer = 0;
    bool immune;
    bool teleBallOut = false;
    bool movingRight = false;
    bool movingLeft = false;
    float blinkCooldownTimer = 0;
    int immunityTimer = 0;


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
        handleImmunity();
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
            teleBallScript.throwTeleBall(transform.position, movingRight);
            clickTimer = clickDelay;
        }
        else if (teleBallOut && Input.GetMouseButtonUp(1) && clickTimer == 0)
        {
            teleBallOut = false;
            teleportToTeleBall();
            teleBallScript.setInvisible(true);
            clickTimer = clickDelay;
        }
    }

    void teleportToTeleBall()
    {
        immunityTimer = immunityTime;
        immune = true;
        transform.position = teleBall.transform.position;
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
        immunityTimer = immunityTime;
        immune = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!immune)
            {
                Destroy(this.gameObject);
                //go to gameOverScreen
            }
            else if (immune)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void handleImmunity()
    {
        if (immunityTimer != 0)
            immunityTimer--;
        else
            immune = false;
    }

}
