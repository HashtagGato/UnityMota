using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inicioEscaneo : MonoBehaviour {
	GameObject but;
	Canvas cbut;
	// Use this for initialization
	void Start () {
		but = GameObject.Find ("CanvasResp");
		cbut = but.GetComponent<Canvas> ();
		//cbut.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("q")){
			cbut.enabled = true;
		}
		if(Input.GetKeyDown("w")){
			cbut.enabled = false;
		}
	}

}
