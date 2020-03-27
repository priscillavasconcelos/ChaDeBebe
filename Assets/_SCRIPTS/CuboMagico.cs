using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboMagico : MonoBehaviour
{
    public bool azul;
    public GameObject chuAzul, chuRosa;
    public Sprite pedAzul, pedRosa;
    SpriteRenderer meuSprite;
    Animator anim;
    bool useless = false;

    // Use this for initialization
    void Start()
    {
        meuSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !useless)
        {
            useless = true;
            anim.enabled = false;
            if (azul)
            {
                meuSprite.sprite = pedAzul;
                Instantiate(chuAzul, transform.GetChild(0).position, Quaternion.identity);
            }
            else
            {
                meuSprite.sprite = pedRosa;
                Instantiate(chuRosa, transform.GetChild(0).position, Quaternion.identity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }


	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
