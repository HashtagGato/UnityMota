using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class finalizar : MonoBehaviour {
	private fin fi;
	// Use this for initialization
	void Start () {
		fi = GameObject.Find ("scriptFin").GetComponent<fin> ();
		string puntuaje = fi.getPuntuaje ();
		gameObject.GetComponent<Text>().text = "Tu puntuaje es " + puntuaje + "/10";
	}
}
