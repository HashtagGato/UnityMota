using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;

public class inicioEscaneo : MonoBehaviour{
	GameObject but, gameObjectScript, haru;
	/*Probando*/
	private GameObject game_object;
	botones button;
	string rutaRecorrida;
	string idEdificio;
	private int[] pasadas = new int[5];
	/**/
	private int puntaje;
	private string idPartida;
	private string idUsuario;
	DefaultTrackableEventHandler mImage;
	Canvas cbut;
	private Text tPreg, tResp1, tResp2, tResp3, tResp4;
	string [] nEdificios = {"A","AC","AF","AG","CH","D","F","H","J","K","L","P","PE","R","S2","S3","T","U","Y","Z"};
	string[] btnsResps = {"A","B","C","D"};
    private int numPregunta;
	Animator anim;
	Canvas cSig, cResps;
	int resp;
    private sig_edificio sigEdif;
	private GameObject[] harukos;
	public AudioSource fuente;
	public AudioClip correcto, incorrecto;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		fuente = GetComponent<AudioSource> ();
        StartCoroutine("startPregunta");
	}

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("q")){
			mImage.enabled =true;
		}
		if(Input.GetKeyDown("w")){
			mImage.enabled = false;
		}
	}

    private IEnumerator startPregunta()
    {
		numPregunta = Obt_Pregunta ();//Obtenemos un numero aleatorio para la pregunta
        string url = string.Concat("http://www.artashadow.xyz/index.php/getPregunta/", numPregunta.ToString());

        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            string json = www.text.Replace("\"", ""); //json obtenido desde el WS
                          
            string[] json_array = json.Split(':');

            gameObjectScript = GameObject.Find("script");
			sigEdif = gameObjectScript.GetComponent<sig_edificio>();
			/*********recuperar el id de la partida**********/
			game_object = GameObject.Find("scripts");
			button = game_object.GetComponent<botones>();
			idPartida = button.idPartida;
			/******************/
			string ed = sigEdif.obtenerEd ();
			string nEdificio = ed;//id? del edificio que regresa el web service.
			idEdificio = sigEdif.idEdificio;
			but = GameObject.Find("CanvasResp");
            cbut = but.GetComponent<Canvas>();
			for (int i = 0; i < nEdificios.Length; i++) {
				//mImage = harukos [i].GetComponent<DefaultTrackableEventHandler> (); harukos [i].name.Replace ("ImageTarget", "")
				if (!nEdificio.Equals (nEdificios [i])) {
					GameObject.Find ("ImageTarget" + nEdificios [i]).GetComponent<DefaultTrackableEventHandler> ().enabled = false;
					GameObject.Find ("ImageTarget" + nEdificios[i]).SetActive (false);

				}
			}
            tPreg = GameObject.Find("preguntaText" + nEdificio).GetComponent<Text>();
            tResp1 = GameObject.Find("TextA").GetComponent<Text>();
            tResp2 = GameObject.Find("TextB").GetComponent<Text>();
            tResp3 = GameObject.Find("TextC").GetComponent<Text>();
            tResp4 = GameObject.Find("TextD").GetComponent<Text>();
            tResp1.text = json_array[2].Split(',')[0];//Asignar lo consumido por el webService, en cada una
            tResp2.text = json_array[2].Split(',')[1];
            tResp3.text = json_array[2].Split(',')[2];
            tResp4.text = json_array[2].Split(',')[3];
            tPreg.text = json_array[1].Split(',')[0];

			resp = int.Parse(json_array[3].Split('}')[0]);

            Debug.Log(json_array[2]);

			anim = GameObject.Find ("Haruko"+nEdificio).GetComponent<Animator> ();
			cSig = GameObject.Find ("CanvasSiguiente").GetComponent<Canvas> ();
			cResps = GameObject.Find ("CanvasResp").GetComponent<Canvas> ();
            
            //----------------------------------------------------------------------------------------------------------

            //Debug.Log(jsonUTF8);
        } else
        {
            Debug.Log(string.Concat(www.error, " aqui marca el error"));
        }
        
    }

	public void enviarResp(string pTextName){
		pTextName = pTextName.Replace ("Text", "");

		if (pTextName.Equals (btnsResps [resp])) {
			tPreg.text = "¡Respuesta correcta!";
			anim.Play ("jump");
			cResps.enabled = false;
			cSig.enabled = true;
			puntaje += 1;
			fuente.clip = correcto;
			fuente.Play ();
		} else {
			tPreg.text = "¡Respuesta incorrecta!";
			anim.Play ("kick");
			cResps.enabled = false;
			cSig.enabled = true;
			fuente.clip = incorrecto;
			fuente.Play ();

		}
		//Obtener ruta

	}
	private IEnumerator ObtainRuta()//Obtiene la ruta recorrida hasta antes de esta pregunta y le agrega el punto actual
	{
		string url = string.Concat("http://www.artashadow.xyz/index.php/getPartida/", idPartida);
		WWW www = new WWW(url);
		yield return www;
		if (www.error == null)
		{
			string[] json = www.text.Split(':');
			rutaRecorrida = json[10].Split('"')[1];
			if (rutaRecorrida.Equals ("")) {
				rutaRecorrida = idEdificio;
			} else {
				rutaRecorrida += "," + idEdificio;
			}
			string puntajeS =json[8].Split(',')[0];
			puntajeS =puntajeS.Split('"')[1];
			puntaje = int.Parse (puntajeS);
			StartCoroutine("ActualizarPartida");
		}
		else
		{
			Debug.Log(www.error);
		}
	}
	public void obtainX(){
		StartCoroutine("ObtainRuta");
	}
	private IEnumerator ActualizarPartida(){//Actualiza la partida
		string url = "http://www.artashadow.xyz/index.php/actualizarPartida?";
		string	datos = "id_partida="+idPartida+"&ruta_recorrida="+rutaRecorrida+"&puntaje="+puntaje;
		url += datos;
		byte[] data = System.Text.Encoding.UTF8.GetBytes (datos);
		using (UnityWebRequest www = UnityWebRequest.Put(url,data)){
			yield return www.SendWebRequest ();
			if (www.isNetworkError || www.isHttpError) {
				Debug.Log ("oyeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee ->" + www.error);
			} else {
				string respuesta = www.downloadHandler.text;
				if (respuesta.Contains("\"ok\"")) {
					Debug.Log ("Si se actualizo");

				} else {
					Debug.Log (respuesta);
				}
			}
		}
		SceneManager.LoadScene ("sig_edificio");
	}
	private int Obt_Pregunta(){
		int p = UnityEngine.Random.Range (1,51);
		if(p == pasadas[0] | p == pasadas[1] | p == pasadas[2] | p == pasadas[3] | p == pasadas[4]){
			p = Obt_Pregunta();
		}else{
			pasadas [4] = pasadas [4];
			pasadas [3] = pasadas [2];
			pasadas [2] = pasadas [1];
			pasadas [1] = pasadas [0];
			pasadas [0] = p;
		}
	}
	public int getPreguntaID(){
		return numPregunta;
	}

}
