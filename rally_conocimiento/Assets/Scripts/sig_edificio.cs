using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sig_edificio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Landscape;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void BotonSalir(string scene)
	{
		//Dialogo de confirmacion
		//si acepta, mandar siguiente funcion
		Debug.Log ("cambio de escena: Salir");
		SceneManager.LoadScene (scene);
	}
}
