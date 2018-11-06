using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour {

   public string level;

    private void OnTriggerEnter2D(Collider2D Colider)
    {
        if (Colider.gameObject.tag == "Player")
        {
            Application.LoadLevel(level);
        
        }
        
    }
}
