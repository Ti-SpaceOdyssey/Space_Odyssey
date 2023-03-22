using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

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
    // Start is called before the first frame update
    void Start()
    {

        DesligarFlash();
    }

    // Update is called once per frame
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
    }
}
