using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movs : MonoBehaviour {

	Animator anim;
	Text tA, tB, tC, tD;
	// Use this for initialization
	void Start () {
		anim = GameObject.Find ("Haruko").GetComponent<Animator> ();
		tA = GameObject.Find ("TextA").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enviarResp(){
		Debug.Log ("hola");
		tA.text = "Se hizo clic";
		anim.Play ("kick");
	}
}
