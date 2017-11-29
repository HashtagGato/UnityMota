using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

    public static User user;
    public string name_user;

    private void Awake()
    {
        if (user == null)
        {
            user = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("es el 1");
        } else if(user != this)
        {
            Destroy(gameObject);
            Debug.Log("se destruye");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
