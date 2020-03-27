using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cegonha : MonoBehaviour
{
    public Transform soltaDaqui;
    public GameObject scroll;
    public float velocidade;
    bool agoraPode = false;
    public CanvasGroup[] panels;
    int numSlide = 0;
    public SpriteRenderer[] coracoes;
    public Animator[] nuvens;
	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Pisca", 0f, 0.2f);
        Invoke("Espera", 5f);
        foreach(CanvasGroup a in panels)
        {
            a.alpha = 0f;
        }
        panels[numSlide].alpha = 1f;
        foreach (Animator nuvem in nuvens)
        {
            nuvem.enabled = false;
        }
    }

    void Pisca()
    {
        //int alteraEsse = Random.Range(0, coracoes.Length);
        coracoes[0].color = new Color(1f, 1f, 1f, Random.Range(0.6f, 1f));
        coracoes[1].color = new Color(1f, 1f, 1f, Random.Range(0.6f, 1f));
        coracoes[2].color = new Color(1f, 1f, 1f, Random.Range(0.6f, 1f));
    }

    void PassaSlide()
    {
        //panels[numSlide].alpha = 0f;
        if (numSlide < panels.Length-1)
        {
            numSlide++;
            panels[numSlide].alpha = 1f;
            if (numSlide == 4)
            {
                foreach(Animator nuvem in nuvens)
                {
                    nuvem.enabled = true;
                }
            }
        }
        else
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }


    }

    void Espera()
    {
        agoraPode = true;
        panels[numSlide].alpha = 0f;
        numSlide++;
        panels[numSlide].alpha = 1f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (agoraPode)
        {
            transform.Translate(new Vector3(velocidade * Time.deltaTime, 0f, 0f));
        }
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "SoltaOCoisa")
        {
            Instantiate(scroll, soltaDaqui.position, Quaternion.identity);
        }
        else if (collision.transform.name == "TrocaImagem")
        {
            InvokeRepeating("PassaSlide", 0f, 5f);

        }
    }
}
