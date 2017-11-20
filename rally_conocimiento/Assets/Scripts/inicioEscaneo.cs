using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class inicioEscaneo : MonoBehaviour{
	GameObject but,mImage;
	Canvas cbut;
	private Text tPreg, tResp1, tResp2, tResp3, tResp4;
	string [] nEdificios = {"A","AC","AF","AG","CH","D","F","H","J","K","L","P","PE","R","S2","S3","T","U","Y","Z"};

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
        string nEdificio = "Z";//Remplazar por el index del edificio que regresa el web service.
		but = GameObject.Find ("CanvasResp");
		cbut = but.GetComponent<Canvas> ();
		for(int i=0; i<nEdificios.Length;i++){
			mImage = GameObject.Find ("ImageTarget" + nEdificios[i]);
			if (!nEdificio.Equals (nEdificios [i]))
				mImage.SetActive (false);
		}
		tPreg = GameObject.Find ("preguntaText"+nEdificio).GetComponent<Text> ();
		tResp1 = GameObject.Find ("TextA").GetComponent<Text> ();
		tResp2 = GameObject.Find ("TextB").GetComponent<Text> ();
		tResp3 = GameObject.Find ("TextC").GetComponent<Text> ();
		tResp4 = GameObject.Find ("TextD").GetComponent<Text> ();
		tResp1.text = "Resp A";//Asignar lo consumido por el webService, en cada una
		tResp2.text = "Resp B";
		tResp3.text = "Resp C";
		tResp4.text = "Resp D";
		tPreg.text = "Hola mi nombre es haruko y esta es tu siguiente pregunta, ¿cuántos años se toma para viajar a la luna?";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("q")){
			mImage.SetActive (true);
		}
		if(Input.GetKeyDown("w")){
			mImage.SetActive (false);
		}
	}

}
