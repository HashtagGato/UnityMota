using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login_registro : MonoBehaviour {
	//Objetos de tipo InputField para recibir datos
	public InputField usuario;
	public InputField password;
	void Start(){
        Screen.orientation = ScreenOrientation.Portrait;

        //obtiene el objeto especificado por el nombre
        GameObject inputFieldU = GameObject.Find ("Usuario");
		GameObject inputFieldP = GameObject.Find ("Password");
		//Ontiene el componente del objeto 
		usuario = inputFieldU.GetComponent<InputField> ();
		password = inputFieldP.GetComponent<InputField> ();
	}
	public void Login()
    {
		StartCoroutine ("startPost");
    }
	private IEnumerator startPost(){
		//Recupera el text de los iInput Field y los guarda en variables
		string user = "";
		string pass = "";
		user = usuario.text;
		pass = password.text;
		//Validacion de datos en el web service //?nombre=USER&contraPASS
		WWWForm url = new WWWForm();
		url.AddField ("nombre",user);
		url.AddField ("contra",pass);//Cifrar contraseña
		using (UnityWebRequest www = UnityWebRequest.Post("https://artashadow.000webhostapp.com/index.php/login", url)){
			yield return www.SendWebRequest ();
			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				string json = www.downloadHandler.text;
				if (!json.Equals("[]")) {
					CambiarEscena("menu");
				} else {
					usuario.text = "Usuario no encontrado";
				}
			}
		}
	}

    public void Register()
    {
		//StartCoroutine ("StartGet");
    }
	private IEnumerator StartGet(){
		//Recupera el text de los iInput Field y los guarda en variables
		string user = "";
		string pass = "";
		user = usuario.text;
		pass = password.text;
		//Validacion de datos en el web service //?nombre=USER&contraPASS
		WWWForm url = new WWWForm();
		url.AddField ("nombre",user);
		url.AddField ("contra",pass);//Cifrar contraseña
		using (UnityWebRequest www = UnityWebRequest.Post("https://artashadow.000webhostapp.com/index.php/nuevoUsuario", url)){
			yield return www.SendWebRequest ();
			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				string json = www.downloadHandler.text;
				if (!json.Equals("")) {
					Debug.Log ("Usuario Registrado correctamente");
					CambiarEscena("menu");
				} else {
					usuario.text = "Usuario no encontrado";
				}
			}
		}
	}
	public void CambiarEscena(string scene){
		Debug.Log ("cambio de escena");
		SceneManager.LoadScene (scene);
	}
}
