using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sig_edificio : MonoBehaviour {
	GameObject A,AC,AF,AG,CH,D,F,H,J,K,L,P,PE,R,S2,S3,T,U,Y,Z;
	Canvas  cMapa, cSig;
	Image aux;
	Text tAux;
	int rutaAct;
	//
	string[] rutaTot = {"A","Z","K","L","J","AC","AF","H","Y","R"};//de alguna forma se debe sacar del web service un arreglo, aunque hagamos un split de lo que manda.
	//El web service regresa la ruta como el id del edificio, no la letra, ejemplo: "{6,2,1,5,4,1,7,9,8,5}"
	string [] nEdificios = {"A","AC","AF","AG","CH","D","F","H","J","K","L","P","PE","R","S2","S3","T","U","Y","Z"};
	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Landscape;
		cMapa = GameObject.Find ("mapa").GetComponent<Canvas> ();
		cSig = GameObject.Find ("sig_edificio").GetComponent<Canvas> ();
		cSig.enabled = true;
		cMapa.enabled = false;
		rutaAct = 2;//Se debe jalar del ws
		if (rutaAct != 0) {
			for (int i = 0; i < nEdificios.Length; i++) {
				aux = GameObject.Find (nEdificios [i]).GetComponent<Image> ();
				aux.enabled = false;
			}
		}
		if (rutaAct > 0){
			for (int i = 0; i < rutaAct; i++) {
				aux = GameObject.Find (rutaTot [i]).GetComponent<Image> ();
				tAux = GameObject.Find ("Text" + rutaTot [i]).GetComponent<Text> ();
				tAux.text = (i+1).ToString ();
				aux.enabled = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void BotonSalir(string scene)
	{
		SceneManager.LoadScene (scene);
	}
}
