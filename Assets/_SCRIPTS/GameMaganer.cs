using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaganer : MonoBehaviour 
{
    //public Text[] titulo;
    public Image[] titulo; 
    public float fadeOutTime = 2f;
    public static GameMaganer GM;
    void Awake()
    {
        GM = this;
    }
	// Use this for initialization
	void Start () 
    {
        if (titulo.Length > 0)
        {
            StartCoroutine(FadeOutRoutine());
        }

	}

    public void CallFade()
    {
        StartCoroutine(FadeOutOther());
    }

    private IEnumerator FadeOutRoutine()
    {

        Color originalColor = titulo[0].color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            titulo[0].color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }

    private IEnumerator FadeOutOther()
    {

        Color originalColor = titulo[1].color;
        for (float t = 0.1f; t < fadeOutTime; t += Time.deltaTime)
        {
            titulo[1].color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

}
