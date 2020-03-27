using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerChangeColor : MonoBehaviour 
{
    public float fadeOutTime = 2f;
    public Image titulo;
    public Color32 cor2;
    public int numBarra;
    public GameObject riot, riot2, riot3;
    public AudioSource somOk, somQue, somFofo, oRenan, issoehsurto, macacodoenca, mataomacaco;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (numBarra)
            {
                case 0:
                    if (!somFofo.isPlaying)
                    {
                        StartCoroutine(playConversas());
                    }
                    break;
                case 1:
                    StartCoroutine(FadeColor());
                    riot.GetComponent<CarMove>().enabled = true;
                    if (!somQue.isPlaying)
                    {
                        somQue.Play();
                        somFofo.Play();
                        StartCoroutine(playMata());

                    }
                    break;
                case 2:
                    StartCoroutine(FadeOutOther());
                    riot2.GetComponent<CarMove>().enabled = true;
                    riot3.GetComponent<CarMove>().enabled = true;
                    somOk.Stop();
                    mataomacaco.Play();
                    //StartCoroutine(playMata());
                    break;

            }
        }
    }

    IEnumerator playMata()
    {
        AudioClip som = macacodoenca.clip;
        macacodoenca.Play();
        yield return new WaitForSeconds(som.length);
        mataomacaco.Play();

    }

    IEnumerator playConversas()
    {
        AudioClip som = somFofo.clip;
        somFofo.Play();
        yield return new WaitForSeconds(som.length);
        som = oRenan.clip;
        oRenan.Play();
        yield return new WaitForSeconds(som.length);
        som = issoehsurto.clip;
        issoehsurto.Play();
    }

    private IEnumerator FadeColor()
    {

        Color originalColor = titulo.color;
        for (float t = 0.1f; t < fadeOutTime; t += Time.deltaTime)
        {
            titulo.color = Color.Lerp(originalColor, cor2, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
        somOk.Stop();
    }

    private IEnumerator FadeOutOther()
    {

        Color originalColor = titulo.color;
        for (float t = 0.1f; t < fadeOutTime; t += Time.deltaTime)
        {
            titulo.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }
}
