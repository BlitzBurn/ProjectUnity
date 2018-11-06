using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour
{
    public bool immune;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!immune)
            {
                Destroy(this.gameObject);
            }
            else if(immune)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}