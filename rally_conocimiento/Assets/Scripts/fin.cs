using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fin : MonoBehaviour {
	private int puntuaje, idPregunta, idEdificio;
	private string user, pass, idPartida, idUsuario, rutaRecorrer, nEdificio;
	// Use this for initialization
	public void Awake(){
		DontDestroyOnLoad (gameObject);
	}
	public void setPuntuaje(int p){
		puntuaje = p;
	}
	public string getPuntuaje(){
		return puntuaje.ToString ();
	}
	public void setUser(string usr){
		user = usr;
	}
	public string getUser(){
		return user;
	}
	public void setPass(string pss ){
		pass = pss;
	}
	public string getPass(){
		return pass;
	}
	public void setIdPartida(string idPar){
		idPartida = idPar;
	}
	public string getIdPartida(){
		return idPartida;
	}
	public void setidUsuario(string idUsr ){
		idUsuario = idUsr;
	}
	public string getidUsuario(){
		return idUsuario;
	}
	public void setrutaRecorrer(string rtaRec ){
		rutaRecorrer = rtaRec;
	}
	public string getrutaRecorrer(){
		return rutaRecorrer;
	}
	public void setidPregunta(int idPreg ){
		idPregunta = idPreg;
	}
	public int getidPregunta(){
		Debug.Log ("id pregunta en fin: " + idPregunta);
		return idPregunta;
	}
	public void setnEdificio(string nEd){
		nEdificio = nEd;
	}
	public string getnEdificio(){
		return nEdificio;
	}
	public void setidEdificio(int idEd){
		idEdificio = idEd;
	}
	public int getidEdificio(){
		return idEdificio;
	}
}
