using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;


public class botones : MonoBehaviour {
	private Button reanudar;
	private string idUsuario;
	private string usuario;
	private string idPartida;
	private Text nUsuario;
	private int[] recorridos = new int[2];
    private void Start()
    {
		Screen.orientation = ScreenOrientation.Portrait;
		reanudar = GameObject.Find ("btnReanudar").GetComponent<Button>();
		//Verificar si el usuario tiene una partida (sacar el id del usuario, buscar su id en el json de partidas)
		usuario = "hola";//Debe cambiarse a variable que pase entre escenas
		nUsuario = GameObject.Find("nUsuario").GetComponent<Text>();
		nUsuario.text = usuario;
		StartCoroutine("startID");//Buscar el ID del usuario obtenido
		StartCoroutine("startPartida");//Verificar si el usuario tiene una partida activa

    }
	private IEnumerator startID(){
		WWW www = new WWW ("https://artashadow.000webhostapp.com/index.php/usuarios");
		yield return www;
		if(www.error == null){
			//bucar el id del usuario
			string[] jsonString = www.text.Split ('[');
			jsonString =  jsonString[1].Split (']');
			jsonString = jsonString [0].Split ('}');
			for(int i =0; i<jsonString.Length; i++){
				if(jsonString[i].Contains(":\""+usuario+"\"")){
					string[] json2 = jsonString[i].Split (':');
					json2 = json2 [1].Split (',');
					json2 = json2 [0].Split ('\"');
					idUsuario = json2[1];
					Debug.Log ("Id de usuario = " + idUsuario);
				}
			}
		}
	}
	private IEnumerator startPartida(){
		WWW www = new WWW ("https://artashadow.000webhostapp.com/index.php/partidas");
		yield return www;
		if (www.error == null) {
			bool b = Processjson (www.text);
			if (b == false) {
				Debug.Log("Deshabilitado");
				reanudar.enabled = false;
				//cambiar el color o deshabilitar
			}
		} else {
			Debug.Log ("Error: "+www.error);
		}
	}

	private bool Processjson(string json){
		bool bandera = false;
		string[] jsonString = json.Split ('[');
		jsonString =  jsonString[1].Split (']');
		jsonString = jsonString [0].Split ('}');
		for (int i = 0; i < jsonString.Length; i++) {
			if (jsonString [i].Contains ("\"id_usuario\":\""+idUsuario+"\"") && jsonString [i].Contains ("\"h_fin\":\"0000-00-00 00:00:00\"") ) {
				string[] json2 = jsonString [i].Split (',');
				if (i > 0) {
					json2 = json2 [1].Split (':');
					json2 = json2 [1].Split ('\"');
					idPartida = json2 [1];
				} else {
					json2 = json2 [0].Split (':');
					json2 = json2 [1].Split ('\"');
					idPartida = json2 [1];
				}
				bandera = true;
				Debug.Log ("Tiene Partida "+idPartida);
			}
		}

		return bandera;
	}
    public void BotonJugar()
    {
		//Sacar id del usuario
		//Generar nueva ruta
		//Crear nueva partida con el id del usuario y la ruta, mandar al WS
		StartCoroutine("startNuevaRuta");
    }
	private IEnumerator startNuevaRuta(){
		string ruta = "6,2,1,5,4,1,7,9,8,5"; //Ruta estatica para fines de avance, se debe generar de manera dinamica y aleatoria
		WWWForm url = new WWWForm();
		url.AddField ("ruta",ruta);
		url.AddField ("id_usuario",idUsuario);//Cifrar contraseña
		using (UnityWebRequest www = UnityWebRequest.Post("https://artashadow.000webhostapp.com/index.php/nuevaPartida", url)){
			yield return www.SendWebRequest ();
			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				string json = www.downloadHandler.text;
				if (json.Equals("\"ok\"")) {
					Debug.Log ("cambio de escena: Siguiente edificio");
					SceneManager.LoadScene ("sig_edificio");
				} else {
					Debug.Log ("Error al crear una nueva partida");
				}
			}
		}
	}
	public void BotonReanudar(string scene)
    {
		//Enviar idPartida, idUsuario
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
	/*public string calcularRuta(){
		int numero;
		int cont = 0;
		string ruta = "";
		while(cont < 20){
			numero = Numero();
			ruta+=numero+",";
			cont++;
		}
		return ruta;
	}
	public int Numero(){
		System.Random random = System.Random;
		int num = random.Next(1, 20);
		if(num == recorridos[0] | num == recorridos[1])
		{
			num = Numero();
		} else
		{
			recorridos[1] = recorridos[0];
			recorridos[0] = num;
		}
		return num;
	}*/

}


