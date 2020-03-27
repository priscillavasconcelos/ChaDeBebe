using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    Vector2 minX, maxX;
    float direction = 1;
    public float speed = 5;
    public bool voador = false;
    public float range = 1f;

    public Transform saiTiro;
    public GameObject fralda;

    private float timeLeft = 0.2f;
    float dirY = 0;
    // Use this for initialization
    void Start ()
    {
        minX.x = transform.position.x - range;
        maxX.x = transform.position.x + range;
        if (saiTiro)
        {
            InvokeRepeating("Shoot", 0.0f, 2.0f);
        }
        else
        {
            if (!voador)
            {
                InvokeRepeating("Pula", 0.0f, 2.0f);
            }
            else
            {
                dirY = 0.3f;
            }
            
        }
        
    }

    void Pula()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 3.0f), ForceMode2D.Impulse);
    }
	
    void Shoot()
    {
        float velX = 3.0f;
        float velY = 1.0f;
        GameObject tiro = Instantiate(fralda, saiTiro.position, Quaternion.identity);
        if (transform.localScale.x > 0)
        {
            tiro.GetComponent<Rigidbody2D>().AddForce(new Vector2(-velX, velY), ForceMode2D.Impulse);
        }
        else
        {
            tiro.GetComponent<Rigidbody2D>().AddForce(new Vector2(velX, velY), ForceMode2D.Impulse);
        }

        Destroy(tiro, 0.5f);
    }
	// Update is called once per frame
	void Update ()
    {
        
        if (voador)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0.3f;
                dirY = dirY * -1;
            }
            
        }
        if (transform.localScale.x > 0)
        {
            if (minX.x < transform.position.x)
            {
                transform.Translate(-direction * speed * Time.deltaTime, dirY * Time.deltaTime, 0f);
            }
            else
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            
        }
        else
        {   
            if (transform.position.x > maxX.x)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);  
            }
            else
            {
                transform.Translate(direction * speed * Time.deltaTime, dirY * Time.deltaTime, 0f);
            }
        }

    }
}
