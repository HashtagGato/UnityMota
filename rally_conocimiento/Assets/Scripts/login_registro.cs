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
			yield return www.Send ();
			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				Debug.Log ("No regresa error pero aun no se como sacar el valor...rashos");
			}
		}
		//Cambio a Bienvenida/Menu
		//CambiarEscena ("menu");
	}
    public void Register()
    {

    }

	public void CambiarEscena(string scene){
		Debug.Log ("cambio de escena");
		SceneManager.LoadScene (scene);
	}
}
