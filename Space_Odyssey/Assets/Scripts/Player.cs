using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Movimentacao")]
    [Range(0f,2f)]
    [SerializeField]
    private float velocidadeMovimento = 1f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();

        areaPlayer();


    }

    private void areaPlayer()
    {

        

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
