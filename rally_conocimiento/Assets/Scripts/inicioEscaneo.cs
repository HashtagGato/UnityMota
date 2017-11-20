using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class inicioEscaneo : MonoBehaviour{
	GameObject but,mImage;
	Canvas cbut;
	string [] nEdificios = {"A","AC","AF","AG","CH","D","F","H","J","K","L","P","PE","R","S2","S3","T","U","Y","Z"};

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Portrait;
        string nEdificio = "Z";//Remplazar por el index del edificio que regresa el web service.
		but = GameObject.Find ("CanvasResp");
		cbut = but.GetComponent<Canvas> ();
		for(int i=0; i<nEdificios.Length;i++){
			mImage = GameObject.Find ("ImageTarget" + nEdificios[i]);
			if (!nEdificio.Equals (nEdificios [i]))
				mImage.SetActive (false);
		}
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
