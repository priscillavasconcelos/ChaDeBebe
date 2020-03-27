using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour 
{
    public Transform criaAqui;
    public float velocidade;
    public bool primeiro = false;
	// Use this for initialization
	void Start () 
    {
        if (primeiro)
        {
            GameObject outro = Instantiate(gameObject, criaAqui.position, Quaternion.identity);
            outro.GetComponent<BackGroundMove>().primeiro = false;
        }

        Destroy(gameObject, 20f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(new Vector3 (-velocidade * Time.deltaTime, 0f, 0f));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        GameObject outro = Instantiate(gameObject, criaAqui.position, Quaternion.identity);
        outro.GetComponent<BackGroundMove>().primeiro = false;
	}
}
