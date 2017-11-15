using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movs : MonoBehaviour {

	Animator anim;
	Text tA, tB, tC, tD, tPreg;
	Canvas cSig, cResps;
	string[] btnsResps = {"A","B","C","D"};
	int resp = 2;//sustituir por lo que regresa el ws
	// Use this for initialization
	void Start () {
		anim = GameObject.Find ("Haruko").GetComponent<Animator> ();
		cSig = GameObject.Find ("CanvasSiguiente").GetComponent<Canvas> ();
		cResps = GameObject.Find ("CanvasResp").GetComponent<Canvas> ();
		tPreg = GameObject.Find ("preguntaText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enviarResp(string pTextName){
		tA = GameObject.Find (pTextName).GetComponent<Text> ();
		pTextName = pTextName.Replace ("Text", "");
		tA.text = btnsResps [resp];
		Debug.Log (name+"y "+btnsResps[resp]);
		if (pTextName.Equals (btnsResps [resp])) {
			tPreg.text = "¡Respuesta correcta!";
			anim.Play ("jump");
			cResps.enabled = false;
			cSig.enabled = true;
		} else {
			tPreg.text = "¡Respuesta incorrecta!";
			anim.Play ("kick");
			cResps.enabled = false;
			cSig.enabled = true;
		}
	}
}
