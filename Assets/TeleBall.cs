using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleBall : MonoBehaviour {

    SpriteRenderer m_SpriteRenderer;


    // Use this for initialization
    void Start () {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_SpriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setInvisible(bool invis)
    {
        m_SpriteRenderer.enabled = invis;
    }
}
