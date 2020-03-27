using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarro : MonoBehaviour 
{
    public bool esquerdaDireita;
    public GameObject carroEsquerda, carroDireita;
	// Use this for initialization
	void Start () 
    {
        InvokeRepeating("SpawnCar", 1f, 2f);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void SpawnCar()
    {
        if(esquerdaDireita)
        {
            Instantiate(carroEsquerda, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(carroDireita, transform.position, Quaternion.identity);
        }


    }
}
