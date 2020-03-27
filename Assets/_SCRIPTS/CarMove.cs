using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour 
{
    public float MoveSpeed = 50;
    public float MaxDist = 10;
    public float MinDist = 5;
    float posY;
    Vector3 posicao;

    public bool esquerdaDireita;
	// Use this for initialization
	void Start () 
    {
        posY = transform.position.y;
        if (esquerdaDireita)
        {
            posicao = new Vector3(transform.position.x + 200, posY, transform.position.z);
        }
        else
        {
            posicao = new Vector3(transform.position.x - 200, posY, transform.position.z);
        }

	}


	
	// Update is called once per frame
	void Update () 
    {

        if (esquerdaDireita)
        {
            if (Vector3.Distance(transform.position, posicao) >= MinDist)
            {

                transform.position += transform.right * MoveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, posicao) >= MinDist)
            {

                transform.position -= transform.right * MoveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }


	}
}
