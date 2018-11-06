using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGompa : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float movementSpeed;
    private float direction = -1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction *= -1;
    }
    private void Update()
    {
        rigidbody2D.velocity = new Vector2(direction * movementSpeed, rigidbody2D.velocity.y);
    }
}