using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pontuacao : MonoBehaviour
{

    private TextMeshProUGUI texto;
    private static int pontos=0;

    void Start()
    {
        // Encontre o objeto com o componente TextMeshProUGUI
        GameObject textoObjeto = GameObject.Find("Pontos");
        // Atribua o componente TextMeshProUGUI à variável 'texto'
        texto = textoObjeto.GetComponent<TextMeshProUGUI>();
        // Atribua o texto inicial
        texto.text = "Pontos: " + pontos.ToString();
    }

    void Update()
    {
        texto.text = "Pontos: " + pontos.ToString();
    }

    public static int editPontos
    {
        get
        {
            return pontos;
        }
        set
        {
            pontos = value;
            if (pontos < 0)
            {
                pontos = 0;
            }

            Debug.Log("Pontuação = " + pontos);
        }
    }
}
