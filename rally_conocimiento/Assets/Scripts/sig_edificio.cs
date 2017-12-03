using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sig_edificio : MonoBehaviour {
	Canvas  cMapa, cSig;
	Text tAux;
	int rutaAct;
    string rA;//Ruta activa
    private string ruta; //obtenerla aqui
    private string idPartida;
    private string idUsuario;
    private string rutaRecorrer;
    public int idEdificio, numPregunta;
    public string edificio;
	private Text letra;
	private int[] pasadas = new int[5];
    // //de alguna forma se debe sacar del web service un arreglo
	//El web service regresa la ruta como el id del edificio, no la letra, ejemplo: "{6,2,1,5,4,1,7,9,8,5}"
	string [] nEdificios = {"A","CH","D","F","H","J","K","L","P","R","S2","S3","T","U","Y","Z","AC","AF","AG","PE"};
	private fin button;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.Landscape;
		cMapa = GameObject.Find ("mapa").GetComponent<Canvas> ();//marca null
		cSig = GameObject.Find ("sig_edificio").GetComponent<Canvas> ();
		letra = GameObject.Find("Edificio").GetComponent<Text>();
		cSig.enabled = true;
		cMapa.enabled = false;
		button = GameObject.Find("scriptFin").GetComponent<fin>();
		//ruta = button.rutaRecorrer;//toda la ruta obtenida cuando hace login, tenemos que ir 
		idPartida = button.getIdPartida();
		idUsuario = button.getidUsuario();
		StartCoroutine("ObtainRuta");
		//Poner los pines en el mapa
		//En la siguiente escena se debe actualizar la ruta recorrida del ws
		/*Checar la ruta recorrida, si esta ya tiene 10 puntos recorridos, no debe aparecer siguiente edificio, sino terminaste o su score
		Definir que aparecera y poner aqui*/
	}

	public void BotonSalir(string scene)
	{
		SceneManager.LoadScene (scene);
	}

	public string obtenerEd(){
		return nEdificios[idEdificio-1];
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
				rutaAct = 0;
            }
            else
            {
                rutaAct = rutaRecorrida.Length;
            }
			idEdificio = (int.Parse (rutaCompleta [rutaAct]));
			button.setidEdificio (idEdificio);
			letra.text = string.Concat ("Edificio ", obtenerEd ());
			button.setnEdificio (obtenerEd ());
			numPregunta = Obt_Pregunta ();
			button.setidPregunta (numPregunta);
			pines (rutaAct, rutaCompleta);
        }
        else
        {
            Debug.Log(www.error);
        }
    }
	private void pines(int posicion, string[] rutaTot){
		Debug.Log("rutaACt = "+posicion+" rutaCOmopleta = "+rutaTot.Length);
		if (posicion != 0) {//Pone todos si no lleva recorrido ningun punto
			for (int i = 0; i < nEdificios.Length; i++) {
				Debug.Log("rutaACt = "+posicion+" rutaCOmopleta = "+rutaTot.Length);
				Image aux = GameObject.Find (nEdificios [i]).GetComponent<Image> ();
				aux.enabled = false;
			}
		}
		if (posicion > 0){//Pinta los puntos recorridos
			for (int i = 0; i < rutaAct; i++) {
				Image aux = GameObject.Find (nEdificios[int.Parse(rutaTot[i])-1]).GetComponent<Image> ();
				tAux = GameObject.Find ("Text" + nEdificios[int.Parse(rutaTot[i])-1]).GetComponent<Text> ();
				tAux.text = (i+1).ToString ();
				aux.enabled = true;
			}
		}
	}
	private int Obt_Pregunta(){
		int p = UnityEngine.Random.Range (1,51);
		if(p == pasadas[0] | p == pasadas[1] | p == pasadas[2] | p == pasadas[3] | p == pasadas[4]){
			p = Obt_Pregunta();
		}else{
			pasadas [4] = pasadas [3];
			pasadas [3] = pasadas [2];
			pasadas [2] = pasadas [1];
			pasadas [1] = pasadas [0];
			pasadas [0] = p;
		}
		return p;
	}
}

