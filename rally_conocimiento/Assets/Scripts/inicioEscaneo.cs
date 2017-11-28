using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class inicioEscaneo : MonoBehaviour{
	GameObject but,mImage, gameObjectScript;
	Canvas cbut;
	private Text tPreg, tResp1, tResp2, tResp3, tResp4;
	string [] nEdificios = {"A","AC","AF","AG","CH","D","F","H","J","K","L","P","PE","R","S2","S3","T","U","Y","Z"};
    private int numPregunta;

    private sig_edificio sigEdif;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;

        StartCoroutine("startPregunta");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("q")){
			mImage.SetActive (true);
		}
		if(Input.GetKeyDown("w")){
			mImage.SetActive (false);
		}
	}

    private IEnumerator startPregunta()
    {
        numPregunta = UnityEngine.Random.Range(1, 51);//Obtenemos un numero aleatorio para la pregunta

        string url = string.Concat("http://www.artashadow.xyz/index.php/getPregunta/", numPregunta.ToString());

        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            string json = www.text.Replace("\"", ""); //json obtenido desde el WS
                          
            string[] json_array = json.Split(':');

            gameObjectScript = GameObject.Find("script");
			sigEdif = gameObjectScript.GetComponent<sig_edificio>();
            string ed = sigEdif.edificio;

            
			string nEdificio = ed;//Remplazar por el index del edificio que regresa el web service.
            but = GameObject.Find("CanvasResp");
            cbut = but.GetComponent<Canvas>();
            for (int i = 0; i < nEdificios.Length; i++)
            {
                mImage = GameObject.Find("ImageTarget" + nEdificios[i]);
                if (!nEdificio.Equals(nEdificios[i]))
                    mImage.SetActive(false);
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

            string correcta = json_array[3].Split('}')[0];

            Debug.Log(json_array[2]);

            
            //----------------------------------------------------------------------------------------------------------

            //Debug.Log(jsonUTF8);
        } else
        {
            Debug.Log(string.Concat(www.error, " aqui marca el error"));
        }
        
    }

}
