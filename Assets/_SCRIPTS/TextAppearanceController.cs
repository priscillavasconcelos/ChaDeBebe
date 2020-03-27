using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextAppearanceController : MonoBehaviour
{
    public List<CanvasGroup> texts;											// Variable that will hold all the GameObjects where the history texts 

	[SerializeField]
	private float timeBtwTexts = 3.0f;									// Time between the appearance of each text

	[SerializeField]
	private float timeBeforeFadeOut = 2.0f;

    private float time = 1.0f;
    private int currentMsg = 0;
    private float timer = 3.0f;

    private float test = 0f;

    void Start ()
    {
        // Function to put the texts in order. It relies on the game objects name (it must be "Text_00")
        foreach (CanvasGroup text in texts)
        {
            text.alpha = 0f;
        }

    }

    //void FixedUpdate()
    //{
    //    test += Time.deltaTime;
    //    print(test);
    //}

	void Update ()
    {
        if (currentMsg < texts.Count)
        {
            if (timer > 0.0f)
            {
                             
                texts[currentMsg].alpha += Time.deltaTime;
                if (texts[currentMsg].alpha >= 1)
                {
                    texts[currentMsg].alpha = 1;
                    timer -= Time.deltaTime * time;
                }
            }
            else
            {
                texts[currentMsg].alpha -= Time.deltaTime;
                if (texts[currentMsg].alpha <= 0)
                {
                    texts[currentMsg].alpha = 0;
                    currentMsg++;
                    timer = timeBtwTexts;
                }
            }
        }
        else
        {
            Time.timeScale = 1.0F;
        }

    }
}
