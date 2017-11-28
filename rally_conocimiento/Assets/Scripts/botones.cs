using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;


public class botones : MonoBehaviour {

    private login_registro loginRegistro;
    private GameObject game_object;
	private Button reanudar;
	public string idUsuario;
	private string usuario;
	public string idPartida;
	private Text nUsuario;
	private int[] recorridos = new int[2];
    public string rutaRecorrer;
    private string ruta;

    public static botones boton;

    private void Awake()
    {
        if (boton == null)
        {
            boton = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("es el 1 de botones");
            //user = usuario.text;
        }
        else if (boton != this)
        {
            Destroy(gameObject);
            Debug.Log("se destruye el de botones");
        }

    }

    private void Start()
    {
		Screen.orientation = ScreenOrientation.Portrait;

        game_object = GameObject.Find("login_registro_cs");
        loginRegistro = game_object.GetComponent<login_registro>();
        usuario = loginRegistro.user;//Recuperando el usuario que hizo login

        reanudar = GameObject.Find ("btnReanudar").GetComponent<Button>();
        //Verificar si el usuario tiene una partida (sacar el id del usuario, buscar su id en el json de partidas)
        
		nUsuario = GameObject.Find("nUsuario").GetComponent<Text>();
		nUsuario.text = usuario;
        reanudar.enabled = false;
        StartCoroutine("startID");//Buscar el ID del usuario obtenido
		StartCoroutine("startPartida");//Verificar si el usuario tiene una partida activa

    }
	private IEnumerator startID(){
		WWW www = new WWW ("http://www.artashadow.xyz/index.php/usuarios");
		yield return www;
		if(www.error == null){
			//bucar el id del usuario
			string[] jsonString = www.text.Split ('[');
			jsonString =  jsonString[1].Split (']');
			jsonString = jsonString [0].Split ('}');
			for(int i =0; i<jsonString.Length; i++){
				if(jsonString[i].Contains(":\""+ usuario.ToUpper() +"\"")){
					string[] json2 = jsonString[i].Split (':');
					json2 = json2 [1].Split (',');
					json2 = json2 [0].Split ('\"');
					idUsuario = json2[1];
					Debug.Log ("Id de usuario = " + idUsuario);
				}
			}
		}
        else
        {
            Debug.Log("Error en obtener id usuario");
        }
	}
	private IEnumerator startPartida(){ //obtener el estado de la partida, 1 aun esta activa, 0 ya esta finalizada
		WWW www = new WWW ("http://www.artashadow.xyz/index.php/partidas");
		yield return www;
		if (www.error == null) {
			bool b = Processjson (www.text);
            Debug.Log(b);
			if (b == false) {
				Debug.Log("Deshabilitado");
				reanudar.enabled = false;
				//cambiar el color o deshabilitar
			} else
            {
                reanudar.enabled = true;
                Debug.Log("habilitado");
            }
		} else {
			Debug.Log ("Error: "+www.error);
		}
	}

	private bool Processjson(string json){
		//Validacion si el usuario tiene partida sin terminar en la base de datos
		bool bandera = false;
		string[] jsonString = json.Split ('[');
		jsonString =  jsonString[1].Split (']');
		jsonString = jsonString [0].Split ('}');
        Debug.Log(jsonString);
		for (int i = 0; i < jsonString.Length; i++) {
			if (jsonString [i].Contains ("\"id_usuario\":\""+idUsuario+"\"") && jsonString [i].Contains ("\"h_fin\":\"0000-00-00 00:00:00\"") ) { //no seria mejor obtenerlo con el estado de la partida? 1 cuando esta activa, 0 cuando no
				//Apenas esta buscando el id de partida porque no lo conoce
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
				//Debug.Log ("Tiene Partida "+idPartida);
			}
		}
        Debug.Log(idPartida);

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
		ruta = calcularRuta(); //Se debe generar de manera dinamica y aleatoria
		WWWForm url = new WWWForm();
		url.AddField ("ruta",ruta);
		url.AddField ("id_usuario",idUsuario);//Cifrar contraseña
		using (UnityWebRequest www = UnityWebRequest.Post("http://www.artashadow.xyz/index.php/nuevaPartida", url)){
			yield return www.SendWebRequest ();
			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			} else {
				string json = www.downloadHandler.text;
				if (json.Equals("\"ok\""))
                {
                    StartCoroutine("getIDPartida");
				} else {
					Debug.Log ("Error al crear una nueva partida");
				}
			}
		}
	}

    private IEnumerator getIDPartida()
    { //obtener el estado de la partida, 1 aun esta activa, 0 ya esta finalizada
        WWW www = new WWW("http://www.artashadow.xyz/index.php/partidas");
        yield return www;
        if (www.error == null)
        {
            string[] jsonString = www.text.Split('[');
            jsonString = jsonString[1].Split(']');
            jsonString = jsonString[0].Split('}');
            Debug.Log(jsonString);
            for (int i = 0; i < jsonString.Length; i++)
            {
                if (jsonString[i].Contains("\"id_usuario\":\"" + idUsuario + "\"") && jsonString[i].Contains("\"h_fin\":\"0000-00-00 00:00:00\""))
                { //no seria mejor obtenerlo con el estado de la partida? 1 cuando esta activa, 0 cuando no
                    string[] json2 = jsonString[i].Split(',');
                    if (i > 0)
                    {
                        json2 = json2[1].Split(':');
                        json2 = json2[1].Split('\"');
                        idPartida = json2[1];
                    }
                    else
                    {
                        json2 = json2[0].Split(':');
                        json2 = json2[1].Split('\"');
                        idPartida = json2[1];
                    }
                    Debug.Log("Tiene Partida " + idPartida);
                }
            }
            rutaRecorrer = ruta;
            Debug.Log(rutaRecorrer);
            SceneManager.LoadScene("sig_edificio");
        }
        else
        {
            Debug.Log("Error: " + www.error);
        }
    }

    public void BotonReanudar(string scene)
    {
        SceneManager.LoadScene(scene);
        //StartCoroutine("StartReanudar");
    }
    
    

	public void BotonSalir(string scene)
    {
		//Dialogo de confirmacion
		//si acepta, mandar siguiente funcion
		Debug.Log ("cambio de escena: Salir");
		SceneManager.LoadScene (scene);
    }
	private string calcularRuta(){
		int numero;
		int cont = 0;
		string ruta = "";
		while(cont < 10){
			numero = Numero();
			if(cont < 9)
			{
				ruta += numero + ",";
			} else
			{
				ruta += numero;
			}

			cont++;
		}
		return ruta;
	}
	private int Numero(){
		int num = UnityEngine.Random.Range(1, 21);
		if(num == recorridos[0] | num == recorridos[1])
		{
			num = Numero();
		} else
		{
			recorridos[1] = recorridos[0];
			recorridos[0] = num;
		}
		return num;
	}	

}


