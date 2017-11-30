using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fin : MonoBehaviour {
	private int puntuaje;
	// Use this for initialization
	public fin fincito;
	public void Awake(){
		DontDestroyOnLoad (gameObject);
	}
	public void setPuntuaje(int p){
		puntuaje = p;
	}
	public string getPuntuaje(){
		return puntuaje.ToString ();
	}
}
