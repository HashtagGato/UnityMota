using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class finalizar : MonoBehaviour {
	private fin fi;
	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		fi = GameObject.Find ("scriptFin").GetComponent<fin> ();
		string puntuaje = fi.getPuntuaje ();
		GameObject.Find("score").GetComponent<Text>().text = "Tu puntuaje es " + puntuaje + "/10";
	}
	public void cambiarScene(){
		SceneManager.LoadScene ("menu");
	}
}
