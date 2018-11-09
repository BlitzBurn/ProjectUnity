using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour {

    public GameObject teleBall;
    public float blinkDistance;
    public float blinkCooldownTime;
    public float clickDelay;
    public float immunityTime;
    public float despawnTime;

    TeleBall teleBallScript;
    SpriteRenderer m_SpriteRenderer;
    Rigidbody2D rigidbody2D;


    bool immune = false;
    bool teleBallOut = false;
    bool movingRight = false;
    bool movingLeft = false;

    float blinkCooldownTimer = 0;
    float immunityTimer = 0;
    float clickTimer = 0;
    float despawnTimer = 0;


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
        if (!teleBallOut && Input.GetMouseButtonUp(1) && Time.time - clickTimer > clickDelay)
        {
            teleBallOut = true;
            teleBallScript.setInvisible(false);
            teleBallScript.throwTeleBall(transform.position, movingRight);
            clickTimer = Time.time;
            despawnTimer = Time.time;
        }
        else if (teleBallOut && Input.GetMouseButtonUp(1) && Time.time - clickTimer > clickDelay)
        {
            teleBallOut = false;
            teleportToTeleBall();
            teleBallScript.setInvisible(true);
            clickTimer = Time.time;
        }

        if (Time.time - despawnTimer > despawnTime)
        {
            teleBallOut = false;
            teleBallScript.setInvisible(true);
        }
    }

    void teleportToTeleBall()
    {
        immunityTimer = Time.time;
        immune = true;
        transform.position = teleBall.transform.position;
    }

    void handleBlink()
    {
        if (Input.GetMouseButtonDown(0) &&  Time.time - blinkCooldownTimer > blinkCooldownTime)
        {
            blink();
            blinkCooldownTimer = Time.time;
        }
    }

    void blink()
    {
        immunityTimer = Time.time;
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

    void cooldownVisuals()
    {
        if (blinkCooldownTimer != 0)
            m_SpriteRenderer.color = new Color(1 - blinkCooldownTimer/blinkCooldownTime, 0, 0);
        else
            m_SpriteRenderer.color = new Color(1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!immune)
            {
                Destroy(this.gameObject);
                Application.LoadLevel("GameOverScreen");
            }
            else if (immune)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void handleImmunity()
    {
        if (Time.time - immunityTimer > immunityTime)
            immune = false;
    }

}
