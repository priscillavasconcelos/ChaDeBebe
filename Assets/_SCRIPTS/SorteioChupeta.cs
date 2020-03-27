using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SorteioChupeta : MonoBehaviour
{
    bool colidi = false, podeParar = false;
    public Transform retangulo;
    public float speed = 1f;
    Vector3 posInicial;
    private float startTime;
    private float journeyLength;

    bool comecarRotacao = false, horaDoAzul = true;
    public int numRotacoesTotal = 5; 
    private int numRotacoes = 0;
    public bool menino;

    public CanvasGroup panel;
    public Text texto1, texto2;
    public GameObject partMenino, partMenina;

    // Use this for initialization
    void Start ()
    {
        posInicial = transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(posInicial, retangulo.position);

        if (menino)
        {
            texto1.text = "PARABÉNS, É UM";
            texto2.text = "MENINO";
        }
        else
        {
            texto1.text = "Seja bem vinda, princesa";
            texto2.text = "Moana!";
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (comecarRotacao)
        {
            if (numRotacoes < numRotacoesTotal)
            {
                if (horaDoAzul)
                {
                    if (retangulo.GetChild(0).position.x >= -0.4f)
                    {
                        if (retangulo.GetChild(0).position.x <= 0.0f)
                        {
                            retangulo.GetChild(1).Translate(-speed * Time.deltaTime, 0f, 0f); 
                        }
                        retangulo.GetChild(0).Translate(-speed * Time.deltaTime, 0f, 0f);
                        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
                    }
                    else
                    {
                        horaDoAzul = false;
                        retangulo.GetChild(0).position = new Vector3(0.4f, retangulo.position.y, retangulo.position.z);
                        transform.GetComponent<SpriteRenderer>().enabled = false;
                        if (!menino)
                        {
                            numRotacoes++;
                        }
                        
                    }
                }
                else
                {
                    if (retangulo.GetChild(1).position.x >= -0.4f)
                    {
                        if (retangulo.GetChild(1).position.x <= 0.0f)
                        {
                            retangulo.GetChild(0).Translate(-speed * Time.deltaTime, 0f, 0f);
                        }
                        retangulo.GetChild(1).Translate(-speed * Time.deltaTime, 0f, 0f);
                    }
                    else
                    {
                        horaDoAzul = true;
                        retangulo.GetChild(1).position = new Vector3(0.4f, retangulo.position.y, retangulo.position.z);
                        if (menino)
                        {
                            numRotacoes++;
                        }
                    }
                }
                
            }
            else
            {
                panel.alpha = 1f;
                if (menino)
                {
                    partMenino.SetActive(true);
                }
                else
                {
                    partMenina.SetActive(true);
                }
            }

            
        }
        else
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(posInicial, retangulo.position, fracJourney);
            if (transform.position.y >= retangulo.position.y)
            {
                comecarRotacao = true;
                transform.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

            }
        }
        
	}


}
