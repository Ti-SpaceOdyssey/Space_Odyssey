using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gerar : MonoBehaviour
{
    
    [Header("Prefab")]
    [SerializeField]
    private GameObject gerarPrefab;

    [Header("Delay")]
    [SerializeField]
    [Range(0f,10f)]
    private float inicialDelay = 1f;

    [SerializeField]
    [Range(0f,10f)]
    private float gerarDelay = 1f;

    [Header("Limite")]
    [SerializeField]
    private Limite limiteX;

    [SerializeField]
    private Limite limiteY;

    private  void Awake() {
        InvokeRepeating(nameof(Gera), inicialDelay,gerarDelay);
    }

    private void Gera(){
        
        
        var randomX = Random.Range(limiteX.min, limiteX.max);
        var randomY = Random.Range(limiteY.min, limiteY.max);

        var position = new Vector3(
            transform.position.x + randomX,
            transform.position.y + randomY,
            transform.position.z
        );

        Instantiate(gerarPrefab, position, transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
