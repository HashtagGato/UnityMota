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
	public Text error;
	public string user="";
	public string pass="";

	void Start(){
        Screen.orientation = ScreenOrientation.Portrait;

        //obtiene el objeto especificado por el nombre
        GameObject inputFieldU = GameObject.Find ("Usuario");
		GameObject inputFieldP = GameObject.Find ("Password");
		error = GameObject.Find ("Error").GetComponent<Text>();
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
					error.text = "Usuario no encontrado";
					error.color = Color.red;
					Debug.Log ("Usuario no encontrado");
				}
			}
		}
	}

	public void Register()
	{
		StartCoroutine ("startBusqueda");
	}
	private IEnumerator startBusqueda(){
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
					error.text = "Usuario invalido. Vuelve a ingresar tus datos";
					error.color = Color.red;
					Debug.Log ("Usuario no encontrado");
				} else {
					int tipo = 0;
					//Registro de usuario //?nombre=USER&contraPASS
					url = new WWWForm();
					url.AddField ("nombre",user);
					url.AddField ("contra",pass);//Cifrar contraseña
					url.AddField("tipo",tipo);
					using (UnityWebRequest www2 = UnityWebRequest.Post("https://artashadow.000webhostapp.com/index.php/nuevoUsuario", url)){
						yield return www2.SendWebRequest ();
						if (www2.isNetworkError || www2.isHttpError) {
							Debug.Log (www2.error);
						} else {
							 json = www2.downloadHandler.text;
							if (json.Contains("ok")) {
								Debug.Log ("Usuario Registrado correctamente");
								Debug.Log (json);
								StartCoroutine ("startPost");
							} else {
								error.text = "El usuario ya existe";
									error.color = Color.red;
							}
						}
					}
				}
			}
		}
	}

	public void CambiarEscena(string scene){
		Debug.Log ("cambio de escena");
		SceneManager.LoadScene (scene);
	}
}