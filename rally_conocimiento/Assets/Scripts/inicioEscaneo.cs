using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class inicioEscaneo : MonoBehaviour{
	GameObject but, gameObjectScript;
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

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;

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
        numPregunta = UnityEngine.Random.Range(1, 51);//Obtenemos un numero aleatorio para la pregunta
		GameObject[] harukos = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < harukos.Length; i++)
		{
			harukos [i].SetActive (false);
		}
        string url = string.Concat("http://www.artashadow.xyz/index.php/getPregunta/", numPregunta.ToString());

        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            string json = www.text.Replace("\"", ""); //json obtenido desde el WS
                          
            string[] json_array = json.Split(':');

            gameObjectScript = GameObject.Find("script");
			sigEdif = gameObjectScript.GetComponent<sig_edificio>();
			string ed = sigEdif.obtenerEd ();
			string nEdificio = ed;//Remplazar por el index del edificio que regresa el web service.
            but = GameObject.Find("CanvasResp");
            cbut = but.GetComponent<Canvas>();
            for (int i = 0; i < nEdificios.Length; i++)
            {
				mImage = harukos[i].GetComponent<DefaultTrackableEventHandler> ();
				if (nEdificio.Equals (harukos [i].name.Replace("ImageTarget",""))) {
					harukos [i].SetActive (true);
					mImage.enabled = true;
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
		} else {
			tPreg.text = "¡Respuesta incorrecta!";
			anim.Play ("kick");
			cResps.enabled = false;
			cSig.enabled = true;
		}
	}

}
