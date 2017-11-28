using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Movs : MonoBehaviour {
	GameObject gameObjectScript;
	private sig_edificio sigEdif;
	private TrackableBehaviour mTrackableBehaviour;
	Animator anim;
	Text tA, tB, tC, tD, tPreg;
	Canvas cSig, cResps;
	string[] btnsResps = {"A","B","C","D"};
	string [] nEdificios = {"A","AC","AF","AG","CH","D","F","H","J","K","L","P","PE","R","S2","S3","T","U","Y","Z"};
	string sName, sPreg;
	int resp = 2;//sustituir por lo que regresa el ws
	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;
		gameObjectScript = GameObject.Find("script");
		sigEdif = gameObjectScript.GetComponent<sig_edificio>();
		string ed = sigEdif.obtenerEd ();
		string nEdificio = ed;//Remplazar por el index del edificio que regresa el web service.
		Debug.Log ("nEdificio: "+nEdificio);
		anim = GameObject.Find ("Haruko"+nEdificio).GetComponent<Animator> ();
		cSig = GameObject.Find ("CanvasSiguiente").GetComponent<Canvas> ();
		cResps = GameObject.Find ("CanvasResp").GetComponent<Canvas> ();
		tPreg = GameObject.Find ("preguntaText"+nEdificio).GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enviarResp(string pTextName){
		pTextName = pTextName.Replace ("Text", "");

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
