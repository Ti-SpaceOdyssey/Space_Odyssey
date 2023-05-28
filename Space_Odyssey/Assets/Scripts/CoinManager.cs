using UnityEngine;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{   
    public static bool bossCriado = false;
    public GameObject CoinPrefab; // Prefab da moeda no Unity Inspector
    public List<Transform> Waypoints; // Lista de waypoints no Unity Inspector
    public float spawnInterval = 15f; // Intervalo de tempo entre as gerações em segundos

    public List<Transform> BossWaypoints; // Lista de waypoints específicos para o boss
    private bool geracaoAtiva = true; // Variável para controlar a geração de coin

    private GameObject currentCoin;

    private void Start()
    {
        InvokeRepeating("SpawnCoin", spawnInterval, spawnInterval);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(currentCoin);
            SpawnCoin();
        }
    }

    public void SpawnCoin()
    {

        // Verifica se a geração está desativada ou se o boss já foi criado
        if (!geracaoAtiva || Gerar.bossCriado)
        {
            if (CoinPrefab != null && BossWaypoints.Count > 0)
            {
                int randomIndex = Random.Range(0, BossWaypoints.Count);
                Transform randomWaypoint = BossWaypoints[randomIndex];
                currentCoin = Instantiate(CoinPrefab, randomWaypoint.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("O prefab da moeda ou a lista de waypoints do boss não foi atribuída no editor da Unity.");
            }
            return;
        }

        if (CoinPrefab != null && Waypoints.Count > 0)
        {
            int randomIndex = Random.Range(0, Waypoints.Count);
            Transform randomWaypoint = Waypoints[randomIndex];
            currentCoin = Instantiate(CoinPrefab, randomWaypoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("O prefab da moeda ou a lista de waypoints não foi atribuída no editor da Unity.");
        }
    }

    // Função para ativar/desativar a geração de inimigos
    public void SetGeracaoAtiva(bool ativa)
    {
        geracaoAtiva = ativa;
    }

    void Update(){
        if (CriarBoss.bossMorto == true){
            bossCriado = false;
        }
    }

}