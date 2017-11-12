using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiodeEscena : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void CambiarEscena(string scene){
		SceneManager.LoadScene (scene);
	}
}
