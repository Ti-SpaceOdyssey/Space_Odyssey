using UnityEngine;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public GameObject CoinPrefab; // Prefab da moeda no Unity Inspector
    public List<Transform> Waypoints; // Lista de waypoints no Unity Inspector
    public float spawnInterval = 15f; // Intervalo de tempo entre as gerações em segundos

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
}
