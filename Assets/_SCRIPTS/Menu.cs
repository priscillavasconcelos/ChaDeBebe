using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //public CanvasGroup pisca;
    public CanvasGroup telaInstrucoes, telaMenu, titulo;
    public Button btnVoltar, btnIniciar;
    private float timeLeft = 0.2f;

	private void Start()
	{
        //btnIniciar.Select();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}

	public void IniciarJogo()
    {
        SceneManager.LoadScene("02.Intro", LoadSceneMode.Single);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void Instrucoes()
    {
        btnIniciar.Select();
        titulo.alpha = 0f;
        telaMenu.alpha = 0f;
        telaMenu.interactable = false;
        telaMenu.blocksRaycasts = false;
        telaInstrucoes.alpha = 1f;
        telaInstrucoes.interactable = true;
        telaInstrucoes.blocksRaycasts = true;
        btnVoltar.Select();
    }

    public void Voltar()
    {
        titulo.alpha = 1f;
        telaInstrucoes.alpha = 0f;
        telaInstrucoes.interactable = false;
        telaInstrucoes.blocksRaycasts = false;
        telaMenu.alpha = 1f;
        telaMenu.interactable = true;
        telaMenu.blocksRaycasts = true;
        btnIniciar.Select();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        //timeLeft -= Time.deltaTime;
        //if (timeLeft < 0)
        //{
        //    timeLeft = 0.3f;
        //    sobe = !sobe;
        //}

        //if (sobe)
        //{
        //    pisca.alpha += 0.01f;
        //}
        //else
        //{
        //    pisca.alpha -= 0.01f;
        //}
    }
}
