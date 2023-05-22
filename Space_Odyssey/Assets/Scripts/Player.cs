using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int coins; 

    [SerializeField]
    private ClipWithVolume tiroFx;

    [SerializeField]
    private float delayTiro = 0.5f; // Tempo de delay em segundos

    private float tempoUltimoTiro = 0f;

    [SerializeField]
    private float healthMax = 10;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private GameObject hitPrefab;

    [SerializeField]
    private GameObject explosaoPrefab;

    [SerializeField]
    private int healthPoints = 2;

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

    private CoinManager coinManager;
    private bool canSpawnCoin = true;


    // Start e chamado antes do update do primeiro frame


    void Start()
    {
        DesligarFlash();
        coinManager = FindObjectOfType<CoinManager>();
        SpawnCoin();
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

        // Verifica se já passou tempo suficiente para atirar novamente
        if (Time.time - tempoUltimoTiro < delayTiro)
        {
            return;
        }

        // Se pode atirar, atualiza o tempo do último tiro
        tempoUltimoTiro = Time.time;

        AudioManager.Play(tiroFx);
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

    private void SpawnCoin()
    {
        if (canSpawnCoin)
        {
            coinManager.SpawnCoin();
            canSpawnCoin = false; // Impede a geração de novas moedas até que a atual seja coletada
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "coins")
        {
            Destroy(other.gameObject);
            coins++;

            canSpawnCoin = true; // Permite gerar uma nova moeda

        }
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyTiro") || other.CompareTag("Meteoro"))
        {
            // hit

            Instantiate(hitPrefab, other.transform.position, hitPrefab.transform.rotation);
            Destroy(other.gameObject);

            // Update Health Points
            healthPoints--;
            UpdateHealthBar();

            // Se saude igual ou menor que 0 -> destruir

            if (healthPoints <= 0)
            {
                Instantiate(
                    explosaoPrefab,
                    other.transform.position,
                    explosaoPrefab.transform.rotation
                );
                Destroy(other.gameObject);
                Destroy(gameObject);

                StartCoroutine(Recorde.SetMaiorValor(Pontuacao.pontos));

                TrocaTela();
            }
        }

    }

    private void TrocaTela()
    {
        //A tela de gamer over está na posição 2
        SceneManager.LoadScene(2);
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = healthPoints / healthMax;
    }
}
