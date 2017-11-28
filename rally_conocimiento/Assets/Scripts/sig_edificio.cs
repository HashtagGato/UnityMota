using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sig_edificio : MonoBehaviour {
	GameObject A,AC,AF,AG,CH,D,F,H,J,K,L,P,PE,R,S2,S3,T,U,Y,Z;
	Canvas  cMapa, cSig;
	Image aux;
	Text tAux;
	int rutaAct;
    string rA;//Ruta activa
    private string ruta; //obtenerla aqui
    private string idPartida;
    private string idUsuario;
    private string rutaRecorrer;
    public string idEdificio;
    public string edificio;
    private Text letra;
    //
    private string[] rutaTot; //de alguna forma se debe sacar del web service un arreglo
	//El web service regresa la ruta como el id del edificio, no la letra, ejemplo: "{6,2,1,5,4,1,7,9,8,5}"
	string [] nEdificios = {"A","AC","AF","AG","CH","D","F","H","J","K","L","P","PE","R","S2","S3","T","U","Y","Z"};
    private GameObject game_object;
    botones button;

    // Use this for initialization
    private void Awake()
    {
        game_object = GameObject.Find("scripts");
        button = game_object.GetComponent<botones>();
        //ruta = button.rutaRecorrer;//toda la ruta obtenida cuando hace login, tenemos que ir 
        idPartida = button.idPartida;
        idUsuario = button.idUsuario;
        StartCoroutine("ObtainRuta");
    }
    void Start () {
        Screen.orientation = ScreenOrientation.Landscape;
		cMapa = GameObject.Find ("mapa").GetComponent<Canvas> ();
		cSig = GameObject.Find ("sig_edificio").GetComponent<Canvas> ();
		letra = GameObject.Find("Edificio").GetComponent<Text>();
		cSig.enabled = true;
		cMapa.enabled = false;
		//Poner los pines en el mapa
		if (rutaAct != 0) {//Pone todos si no lleva recorrido ningun punto
			for (int i = 0; i < nEdificios.Length; i++) {
				aux = GameObject.Find (nEdificios [i]).GetComponent<Image> ();
				aux.enabled = false;
			}
		}
		if (rutaAct > 0){//Pinta los puntos recorridos
			for (int i = 0; i < rutaAct; i++) {
				aux = GameObject.Find (rutaTot [i]).GetComponent<Image> ();
				tAux = GameObject.Find ("Text" + rutaTot [i]).GetComponent<Text> ();
				tAux.text = (i+1).ToString ();
				aux.enabled = true;
			}
		}
		//En la siguiente escena se debe actualizar la ruta recorrida del ws
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void BotonSalir(string scene)
	{
		SceneManager.LoadScene (scene);
	}

	public string obtenerEd(){
		string url = string.Concat("http://www.artashadow.xyz/index.php/getEdificio/", idEdificio);
		WWW www = new WWW(url);
		while (!www.isDone) {
		}
		if (www.error == null) {
			edificio = www.text.Split ('"') [3];
			Debug.Log ("edificio " + edificio);
			Debug.Log (edificio);
		} else {
			Debug.Log (url);
			Debug.Log ("error " + www.error);
		}
		return edificio;
	}

    private IEnumerator ObtainRuta()
    {
        string url = string.Concat("http://www.artashadow.xyz/index.php/getPartida/", idPartida);
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            string[] json = www.text.Split(':');
            string[] rutaCompleta = json[9].Split('"')[1].Split(',');
            string[] rutaRecorrida = json[10].Split('"')[1].Split(',');
            rA = json[1].Split('"')[1];//Recupera el estado de la partida 1 si esta activa
            if (rutaRecorrida[0].Equals(""))
            {
                rutaRecorrer = json[9].Split('"')[1];
				//string sigE= rutaCompleta[0].Split('"')[0];//Al no llevar ningun edificio recorrido, 
				rutaAct = 0;
            }
            else
            {
                rutaAct = rutaRecorrida.Length;

                /*string tmp = "";
                for (int i = recorridos; i < rutaCompleta.Length; i++)
                {
                    if (i < rutaCompleta.Length - 1)
                    {
                        tmp += rutaCompleta[i] + ",";
                    }
                    else
                    {
                        tmp += rutaCompleta[i];
                    }
                }
                rutaRecorrer = tmp;*/
            }
            //rutaTot = rutaRecorrer.Split(',');
			idEdificio = rutaCompleta[rutaAct];
			Debug.Log ("unas letras "+idEdificio);
            StartCoroutine("ObtainBuild");
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    private IEnumerator ObtainBuild()
    {
        //Debug.Log(idEdificio);
        string url = string.Concat("http://www.artashadow.xyz/index.php/getEdificio/", idEdificio);
        WWW www = new WWW(url);
        yield return www;
        if(www.error == null)
        {
            edificio = www.text.Split('"')[3];
			Debug.Log ("edificio "+ edificio);
            letra.text = string.Concat("Edificio ", edificio);
            Debug.Log(edificio) ;
        } else
        {
            Debug.Log("error "+www.error);
        }
    }
}

