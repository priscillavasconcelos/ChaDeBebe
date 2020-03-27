using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleScript : MonoBehaviour 
{
    private Transform player;   

    float MoveSpeed = 10;
    float MaxDist = 10;
    float MinDist = 5;
    float posY;
    public float fadeOutTime = 2f;
    public bool angry;


    Vector3 tamanhoMaior = new Vector3(5f, 5f, 5f);

    void Awake()
    {
        // Setting up the reference.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	// Use this for initialization
	void Start () 
    {
        posY = transform.position.y;
        //StartCoroutine(Aumenta());
	}

    private IEnumerator Aumenta()
    {

        Vector3 tamanho = transform.localScale;
        for (float t = 0.1f; t < fadeOutTime; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(tamanho, tamanhoMaior, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        //transform.LookAt(player);

        if (Vector3.Distance(transform.position, player.position) >= MinDist)
        {

            transform.position += transform.right * MoveSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        }


	}
}
