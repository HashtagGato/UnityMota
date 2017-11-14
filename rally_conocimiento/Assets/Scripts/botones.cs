using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class botones : MonoBehaviour {


	public void BotonJugar(string scene)
    {
		
        //Application.LoadLevel("escenaCamara");

		Debug.Log ("cambio de escena: Siguiente edificio");
		SceneManager.LoadScene (scene);
    }

	public void BotonReanudar(string scene)
    {
		Debug.Log ("cambio de escena: siguiente edificio");
		SceneManager.LoadScene (scene);
    }

	public void BotonSalir(string scene)
    {
		//Dialogo de confirmacion
		//si acepta, mandar siguiente funcion
		Debug.Log ("cambio de escena: Salir");
		SceneManager.LoadScene (scene);
    }
}


