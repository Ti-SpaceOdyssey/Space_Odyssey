using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{



    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite moveUp;

    [SerializeField]
    private Sprite moveRight;

    [SerializeField]
    private Sprite moveDown;


    [Header("Movimentacao")]
    [Range(0f, 2f)]
    [SerializeField]
    private float velocidadeMovimento = 1f;



    [Header("Tiro")]
    [SerializeField]
    private Transform LugarTiro;

    [SerializeField]
    private GameObject prefabTiro;

    [SerializeField]
    private GameObject flashObject;

    [SerializeField]
    private float flashdelay = 0.1f;
    // Start e chamado antes do update do primeiro frame
    void Start()
    {

        DesligarFlash();
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        Movimentacao();
        Tiro();



    }

    private void Tiro()
    {
        if (!Input.GetButtonDown("Fire1"))
        {
            return;
        }

        Instantiate(prefabTiro, LugarTiro.position, LugarTiro.rotation);
        flashObject.SetActive(true);
        Invoke(nameof(DesligarFlash), flashdelay);
    }

    private void DesligarFlash()
    {
        flashObject.SetActive(false);
    }


    private void Movimentacao()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var mover = new Vector3(
            x * velocidadeMovimento * Time.deltaTime,
            y * velocidadeMovimento * Time.deltaTime,
            0f
        );

        transform.Translate(mover);

        var sprite = moveRight;

        if (y > 0.2f)
        {
            sprite = moveUp;
        }
        else if (y < -0.2f)
        {
            sprite = moveDown;
        }

        if (spriteRenderer.sprite != sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    private void colisaoInimigo(Collider2D other)
    {
        // diminuir vida do player quando leva tiro, atualizar barra de vida
        // quando vida == 0 -> finalizar jogo -->  abrir menu quer jogar dnv + pontuacao
        // quando colide com meteoro vida == 0 
    }
}
