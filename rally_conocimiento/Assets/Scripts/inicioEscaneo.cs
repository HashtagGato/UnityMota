using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class inicioEscaneo : MonoBehaviour{
	GameObject but,mImage;
	Canvas cbut;
	private Text tPreg, tResp1, tResp2, tResp3, tResp4;
	string [] nEdificios = {"A","AC","AF","AG","CH","D","F","H","J","K","L","P","PE","R","S2","S3","T","U","Y","Z"};
    private int numPregunta;

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

        string url = string.Concat("https://artashadow.000webhostapp.com/index.php/getPregunta/", numPregunta.ToString());

        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            System.Text.Encoding utf_8 = System.Text.Encoding.BigEndianUnicode;
            string json = www.text; //json obtenido desde el WS
                                    //byte[] utf8_bytes = System.Text.Encoding.UTF8.GetBytes(json.Replace("\"",""));
                                    // byte[] utf8_bytes = System.Text.Encoding.BigEndianUnicode.GetBytes(json.Replace("\"", ""));
                                    //string jsonUTF8 = System.Text.Encoding.BigEndianUnicode.GetString(utf8_bytes);//json codificado en UTF8

            //string a = System.Text.Encoding.ASCII(json); 

            string[] json_array = json.Split(':');


            string pregunta = json_array[1].Split(',')[0];
            string op1 = json_array[2].Split(',')[0];
            string op2 = json_array[2].Split(',')[1];
            string op3 = json_array[2].Split(',')[2];
            string op4 = json_array[2].Split(',')[3];

            string correcta = json_array[3].Split('}')[0];

            string nEdificio = "Z";//Remplazar por el index del edificio que regresa el web service.
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

            Debug.Log(json_array[2]);

            
            //----------------------------------------------------------------------------------------------------------

            //Debug.Log(jsonUTF8);
        } else
        {

            //cambio a utf8, esta es una prueba, la dejo para trabajar mas tarde --------------------------------------
            System.Text.Encoding utf_8 = System.Text.Encoding.BigEndianUnicode;

            string s_unicode = "[{ \"pregunta\":\"\u00bfCual es el disco m\u00e1s vendido de la historia?\",\"respuesta\":\"Servicio de lavander\u00eda - Shakira,Thriller-Michael Jackson,Supernatural - Santana,Hotel California - Eagles \",\"respuesta_correcta\":\"1\"}]";
            byte[] utf8_bytes = System.Text.Encoding.BigEndianUnicode.GetBytes(s_unicode.Replace("\"",""));

            string dos = System.Text.Encoding.BigEndianUnicode.GetString(utf8_bytes);

            string pregunta = dos.Split(':')[1].Split(',')[0];

            Debug.Log(pregunta);

            Debug.Log(string.Concat(www.error, " aqui marca el error"));
        }
        
    }

}
