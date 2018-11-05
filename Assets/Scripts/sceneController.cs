using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour {

	public void sceneChanger(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}
